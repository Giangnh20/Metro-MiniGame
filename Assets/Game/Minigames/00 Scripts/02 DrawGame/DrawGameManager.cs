using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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

    [SerializeField] private MouseInputArea mouseInputArea;
    [SerializeField] private Button btnZoomIn;
    [SerializeField] private Button btnZoomOut;
    [SerializeField] private Slider sliderZoom;

    private Transform objective;
    private RectTransform objectiveRectTransform;
    private Vector2 objectiveSize;
    private const float ZOOM_MIN = 1f;
    private const float ZOOM_MAX = 2f;
    private const float ZOOM_STEP_MOUSE = 0.05f;
    private const float ZOOM_STEP_BUTTON = 0.1f;
    
    
    
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
        objective = level.Objective;
        objectiveRectTransform = objective.GetComponent<RectTransform>();
        objectiveSize = objectiveRectTransform.rect.size;
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
        
        // Add listeners
        btnZoomIn.onClick.AddListener(BtnZoomInClicked);
        btnZoomOut.onClick.AddListener(BtnZoomOutClicked);
        sliderZoom.onValueChanged.AddListener(OnSlideValueChanged);
        mouseInputArea.OnMouseWheelScroll += OnMouseWheelScroll;
        mouseInputArea.OnMouseDrag += OnMouseDrag;
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

    private void OnSlideValueChanged(float value)
    {
        var newScale = ZOOM_MIN + value;
        newScale = Mathf.Clamp(newScale, ZOOM_MIN, ZOOM_MAX);
        objective.DOScale(newScale * Vector3.one, 0.1f).SetEase(Ease.Flash);
    }
    

    private void BtnZoomInClicked()
    {
        ZoomIn(ZOOM_STEP_BUTTON);
    }
    
    private void BtnZoomOutClicked()
    {
        ZoomOut(ZOOM_STEP_BUTTON);
    }

    private void ZoomIn(float step)
    {
        var currentScale = objective.localScale;
        var newScale = currentScale.x + step;
        newScale = Mathf.Clamp(newScale, ZOOM_MIN, ZOOM_MAX);
        objective.DOScale(newScale * Vector3.one, 0.1f).SetEase(Ease.Flash);
        sliderZoom.value = newScale - ZOOM_MIN;
    }

    private void ZoomOut(float step)
    {
        var currentScale = objective.localScale;
        var newScale = currentScale.x - step;
        newScale = Mathf.Clamp(newScale, ZOOM_MIN, ZOOM_MAX);
        objective.DOScale(newScale * Vector3.one, 0.1f).SetEase(Ease.Flash);
        sliderZoom.value = newScale - ZOOM_MIN;
    }
    
    private void OnMouseWheelScroll(float scrollValue)
    {
        if (scrollValue > 0)
            ZoomIn(ZOOM_STEP_MOUSE);
        else
            ZoomOut(ZOOM_STEP_MOUSE);
    }
    
    private void OnMouseDrag(Vector3 dragValue)
    {
        dragValue.z = 0f;
        var currentPos = objective.position;
        var newPos = currentPos + dragValue;
        objective.position = newPos;

//        var localPosition = objective.localPosition;
//        var xValue = Mathf.Abs(objectiveSize.x / 2 * objective.localScale.x);
//        var yValue = Mathf.Abs(objectiveSize.y / 2 * objective.localScale.y);
//        var newX = Mathf.Clamp(localPosition.x, - xValue, xValue);
//        var newY = Mathf.Clamp(localPosition.x, - yValue, yValue);
//        objective.localPosition = new Vector3(newX, newY, 0f);
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
