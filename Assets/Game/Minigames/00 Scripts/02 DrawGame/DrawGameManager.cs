using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class DrawGameManager : Singleton<DrawGameManager>
{
    public string levelKey = "DrawLevel_0";
    public Color CurrentColor;
    [SerializeField] private Transform pickerHolder;
    [SerializeField] private ColorPicker prefabPicker;
    [SerializeField] private Transform objectiveHolder;

        public Shader ShaderGUItext => Shader.Find("GUI/Text Shader");
    public Shader ShaderSpritesDefault => Shader.Find("Sprites/Default");
    private List<ColorPicker> pickers;
    public bool InitDone;
    
    // Start is called before the first frame update
    async void Start()
    {
        pickers = new List<ColorPicker>();
        InitDone = false;
        var loadLevel = await Addressables.LoadAssetAsync<ScriptableObject>(levelKey).Task;
        if (loadLevel == null)
        {
            Debug.LogError($"Load level {levelKey} error!");
            return;
        }

        var levelConfig = loadLevel as DrawGameLevelConfig;
        var premiumColors = levelConfig.GetPremiumColors();
        Instantiate(levelConfig.LevelPrefab, objectiveHolder);

        await Task.Delay(TimeSpan.FromSeconds(0.5f));
        
        var allCells = objectiveHolder.GetComponentsInChildren<ColorImage>().ToList();
        var groupByColor = allCells.GroupBy(x => x.Color).ToList();

        List<IDisposable> disposablesCounters = new List<IDisposable>();
        
        int colorIndex = -1;
        foreach (var group in groupByColor)
        {
            int index = ++colorIndex;
            var picker = Instantiate(prefabPicker, pickerHolder);
            int total = group.Count();
            Debug.LogError($"index: {colorIndex}, total: {total} -- {ColorUtility.ToHtmlStringRGBA(group.Key)}");
            picker.SetColor(group.Key, colorIndex);
            if (premiumColors.Contains(group.Key))
                picker.SetLockedState(true);
            
            picker.button.onClick.AddListener(()=> OnPickerSelected(index));
            pickers.Add(picker);

            foreach (var colorImage in @group)
            {
                colorImage.SetColorNumber(colorIndex);
            }
            
            // Subcribe to count opened cells
            group.Select(x => x.IsOpen).CombineLatest().Subscribe(value =>
            {
//                Debug.Log("Some cell open");
                int opended = value.Count(val => val);
                picker.UpdateProgress((float) opended/ total);
                if (opended == total)
                    picker.ShowCompletedState();
            }).AddTo(disposablesCounters);
        }

        pickers.Select(p => p.IsCompleted).CombineLatest().Subscribe(value =>
        {
            if (value.All(done => done))
            {
                Debug.LogError("Level Completed");
            }
        });
        
        
//        yield return null;
        InitDone = true;
    }

    private void OnPickerSelected(int index)
    {
        CurrentColor = pickers[index].Color;
        foreach (var picker in pickers)
        {
            picker.ShowSelectedState(picker.Color == CurrentColor);
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
