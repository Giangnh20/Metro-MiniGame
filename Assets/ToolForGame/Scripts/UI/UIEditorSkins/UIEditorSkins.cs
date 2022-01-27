using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEditorSkins : Dialog<UIEditorSkins>
{
    public static void Show()
    {
        Open();
    }

    public static void Hide()
    {
        Close();
    }

    private UIEditorsSkinCanvas _uiEditorsSkinCanvas;

    public UIEditorsSkinCanvas UIEditorsSkinCanvas
    {
        get
        {
            if (this._uiEditorsSkinCanvas == null)
            {
                this._uiEditorsSkinCanvas = this.GetComponentInChildren<UIEditorsSkinCanvas>();
            }
            return this._uiEditorsSkinCanvas;
        }
    }

    public override void Populate(IScreenData screenData)
    {
        
    }
}