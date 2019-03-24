using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tMove : Tool
{
    public GameObject SelectedObject;

    Vector3 mp;
    public tMove()
    {
        name = "Move";
        ToolTypeCount = 1;
        InitToolTypeIcons();
        InitToolTypeNames();
        InitToolTypeFunk();
        CurrToolType = 0;
        icon = (Sprite)Resources.Load("move-ico", typeof(Sprite));
    }

    protected override void InitToolTypeNames()
    {
        ToolTypeNames = new string[ToolTypeCount];
        ToolTypeNames[0] = "Move";
    }

    protected override void InitToolTypeFunk()
    {
        ttf = new Tool.ToolTypeFunk[ToolTypeCount];
        ttf[0] = fMove;
    }

    void fMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectedObject = pCamera.GetBody();
            if (SelectedObject != null)
            {
                SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.green);
                mp = Camera.main.ScreenToWorldPoint(Input.mousePosition) - SelectedObject.transform.position;
                ActivateProp(0);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (SelectedObject != null)
            {
                Vector3 v = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mp)/10;
                SelectedObject.GetComponent<BodyCtrl>().myBody.SetPosition(new Vector3(Mathf.Floor(v.x)*10,Mathf.Floor(v.y)*10,Mathf.Floor(v.z)*10));
                SetProp(0, "Position: " + SelectedObject.transform.position.ToString());
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedObject != null)
            {
                if (SelectedObject != null) SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.white);
                SelectedObject = null;
                ClearProp();
            }
        }
    }

    public override void Exit()
    {
        if (SelectedObject != null) SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.white);
        SelectedObject = null;
        ClearProp();
    }
}
