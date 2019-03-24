using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tDelete : Tool
{

    GameObject SelectedObject;
    public tDelete()
    {
        name = "Delete";
        ToolTypeCount = 1;
        InitToolTypeIcons();
        InitToolTypeNames();
        InitToolTypeFunk();
        CurrToolType = 0;
        icon = (Sprite)Resources.Load("del-ico", typeof(Sprite));
    }

    protected override void InitToolTypeNames()
    {
        ToolTypeNames = new string[ToolTypeCount];
        ToolTypeNames[0] = "Delete";
    }

    protected override void InitToolTypeFunk()
    {
        ttf = new Tool.ToolTypeFunk[ToolTypeCount];
        ttf[0] = fDelete;
    }

    void fDelete()
    {
        if (SelectedObject != null) SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.white);
        SelectedObject = pCamera.GetBody();
        if (SelectedObject != null) SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.red);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject.Destroy(SelectedObject);
        }
    }

    public override void Exit()
    {
        SelectedObject = null;
    }
}
