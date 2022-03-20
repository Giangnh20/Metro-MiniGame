using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class DrawGameManager : Singleton<DrawGameManager>
{
    public bool playInLocalMode = false;

    [ShowIf("playInLocalMode")] [SerializeField] private ScriptableObject localLevelAsset;
    [HideIf("playInLocalMode")] [SerializeField] public string levelKey = "DrawLevel_0";
    public Color CurrentColor;
    [SerializeField] private Transform objectiveHolder;
    private List<ColorPicker> pickers;
    [SerializeField] private GameObject goLevelCompleted;
    [HideInInspector] public bool InitDone;
    
    // Start is called before the first frame update
    async void Start()
    {
//        pickers = new List<ColorPicker>();
        InitDone = false;
        DrawGameLevelConfig levelConfig;
        if (playInLocalMode)
        {
            if (localLevelAsset == null)
            {
                Debug.LogError($"Load Local LevelAsset error!");
                return;
            }
            levelConfig = localLevelAsset as DrawGameLevelConfig;
        }
        else
        {
            var loadLevel = await Addressables.LoadAssetAsync<ScriptableObject>(levelKey).Task;
            if (loadLevel == null)
            {
                Debug.LogError($"Load level {levelKey} error!");
                return;
            }
            levelConfig = loadLevel as DrawGameLevelConfig;
        }

        var premiumColors = levelConfig.PremiumColors;
        var level = Instantiate(levelConfig.LevelPrefab, objectiveHolder).GetComponent<DrawGameLevel>();
        pickers = level.Pickers;

        for (int i = 0; i < pickers.Count; i++)
        {
            int index = i;
            var picker = pickers[i];
            bool isPremium = IsPremium(premiumColors, picker.Color);
            picker.Populate(isPremium);
            picker.button.onClick.AddListener(()=> OnPickerSelected(index));
        }

        pickers.Select(p => p.IsCompleted).CombineLatest().Subscribe(value =>
        {
            if (value.All(done => done))
            {
                Debug.LogError("Level Completed");
                goLevelCompleted.SetActive(true);
                level.ShowFullColorObject();
            }
        });
        
        
        await UniTask.DelayFrame(1);
        InitDone = true;
    }

    private bool IsPremium(List<Color> premiums, Color tmpColor)
    {
        for (int i = 0; i < premiums.Count; i++)
        {
            if (tmpColor.CompareWithNoAlpha(premiums[i])) 
                return true;
        }
        return false;
    }

    private void OnPickerSelected(int index)
    {
        if (pickers[index].IsLocked.Value)
        {
            pickers[index].ShowBuyUnlock(true);
        }
        else
        {
            CurrentColor = pickers[index].Color;
            foreach (var picker in pickers)
            {
                picker.ShowSelectedState(picker.Color == CurrentColor);
                picker.ShowBuyUnlock(false);
            }
        }    
        
    }


    
//    // Update is called once per frame
//    void Update()
//    {
//
//        if (Input.GetMouseButtonDown(0))
//        {
//            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            RaycastHit2D hit = Physics2D.Raycast(ray, Vector3.forward);
//            if (hit.collider != null)
//            {
//                var colorSprite = hit.collider.GetComponent<ColorSprite>();
//                Debug.LogError(hit.collider.transform.parent.name + $", colorSprite: {ColorUtility.ToHtmlStringRGBA(colorSprite.Color)}, CurrentColor: {ColorUtility.ToHtmlStringRGBA(CurrentColor)}, ");
//                if (colorSprite != null && colorSprite.Color == CurrentColor)
//                {
//                    colorSprite.ToNormalColor();
//                }
//            }
//        }
//            
//    }


}
