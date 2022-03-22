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

    private RectTransform mouseAreaRect;
    private Transform objective;
    private Vector2 objectiveSize;
    private float zoomMin = 1f;
    private float zoomMax = 2f;
    private float zoomStepMouse = 0.05f;
    private float zoomStepButton = 0.1f;

    protected override void Awake()
    {
        base.Awake();
        mouseAreaRect = mouseInputArea.RectTransform;
    }

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

        zoomMin = levelConfig.ZoomMin;
        zoomMax = levelConfig.ZoomMax;
        zoomStepMouse = levelConfig.ZoomStepMouse;
        zoomStepButton = levelConfig.ZoomStepButton;
        var premiumColors = levelConfig.PremiumColors;
        var level = Instantiate(levelConfig.LevelPrefab, objectiveHolder).GetComponent<DrawGameLevel>();
        objective = level.Objective;
        objectiveSize = objective.GetComponent<RectTransform>().rect.size;
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
        
        //
        sliderZoom.minValue = zoomMin;
        sliderZoom.maxValue = zoomMax;
        
        // Add listeners
        btnZoomIn.onClick.AddListener(BtnZoomInClicked);
        btnZoomOut.onClick.AddListener(BtnZoomOutClicked);
        sliderZoom.onValueChanged.AddListener(OnSlideValueChanged);
        mouseInputArea.OnMouseWheelScroll += OnMouseWheelScroll;
        mouseInputArea.OnMouseUpdate += OnMouseUpdate;
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
        var newScale = value;
        newScale = Mathf.Clamp(newScale, zoomMin, zoomMax);
        objective.DOScale(newScale * Vector3.one, 0.1f).SetEase(Ease.Flash);
    }
    

    private void BtnZoomInClicked()
    {
        ZoomIn(zoomStepButton);
    }
    
    private void BtnZoomOutClicked()
    {
        ZoomOut(zoomStepButton);
    }

    private void ZoomIn(float step)
    {
        var currentScale = objective.localScale;
        var newScale = currentScale.x + step;
        newScale = Mathf.Clamp(newScale, zoomMin, zoomMax);
        sliderZoom.value = newScale;
    }

    private void ZoomOut(float step)
    {
        var currentScale = objective.localScale;
        var newScale = currentScale.x - step;
        newScale = Mathf.Clamp(newScale, zoomMin, zoomMax);
        sliderZoom.value = newScale;
    }
    
    private void OnMouseWheelScroll(float scrollValue)
    {
        if (scrollValue > 0)
            ZoomIn(zoomStepMouse);
        else
            ZoomOut(zoomStepMouse);
    }
    

    private bool isDragging = false;
    private Vector3 dragOrigin;
    private Vector3 distanceToObjective;
    private void OnMouseUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragOrigin = Input.mousePosition;
            distanceToObjective = objective.transform.position - dragOrigin;
        }
        
        if (Input.GetMouseButton(0))
        {
            var current = Input.mousePosition;
            Vector3 diff = current + distanceToObjective;
            diff.z = 0f;
            objective.position = diff;
            
            var localPosition = objective.localPosition;
            var xValue = Mathf.Abs((mouseAreaRect.rect.width - objectiveSize.x * objective.localScale.x) / (2 ));
            var yValue = Mathf.Abs((mouseAreaRect.rect.height - objectiveSize.y * objective.localScale.y) / (2 ));
            var newX = Mathf.Clamp(localPosition.x, - xValue, xValue);
            var newY = Mathf.Clamp(localPosition.y, - yValue, yValue);
            objective.localPosition = new Vector3(newX, newY, 0f);

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
