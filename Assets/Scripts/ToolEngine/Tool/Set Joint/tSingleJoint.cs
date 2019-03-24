using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tSingleJoint : Tool
{


    GameObject temp;
    public GameObject SelectedObject;
    Vector2 pos1;
    public bool isset = false;

    public tSingleJoint()
    {
        name = "SetSingleJoint";
        ToolTypeCount = 1;
        InitToolTypeIcons();
        InitToolTypeNames();
        InitToolTypeFunk();
        CurrToolType = 0;
        icon = (Sprite)Resources.Load("singlej-ico", typeof(Sprite));
    }

    protected override void InitToolTypeNames()
    {
        ToolTypeNames = new string[ToolTypeCount];
        ToolTypeNames[0] = "Target Joint";
    }

    protected override void InitToolTypeFunk()
    {
        ttf = new Tool.ToolTypeFunk[ToolTypeCount];
        ttf[0] = fTarget;
    }

    void fTarget()
    {
        ActivateProp(0);
        if (SelectedObject != null) SetProp(0, "Body:" + SelectedObject.name);
        else ClearProp();
        if (myTime.pause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isset)
                {
                    SelectedObject = pCamera.GetBody();
                    if (SelectedObject != null)
                    {
                        isset = true;
                        SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.yellow);
                    }
                }
                else
                {
                    if (SelectedObject == pCamera.GetBody())
                    {
                        pos1 = SelectedObject.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                        new mySingleJoint(SelectedObject.gameObject, pos1);
                    }
                    isset = false;
                }
            }
        }
    }

    public override void Exit()
    {
        isset = false;
        ClearProp();
    }
}
