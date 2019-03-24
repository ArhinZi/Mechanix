using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tLock : Tool
{
    public GameObject SelectedObject;

    Vector3 mp;
    public tLock()
    {
        name = "Lock";
        ToolTypeCount = 1;
        InitToolTypeIcons();
        InitToolTypeNames();
        InitToolTypeFunk();
        CurrToolType = 0;
        icon = (Sprite)Resources.Load("lock-ico", typeof(Sprite));
    }

    protected override void InitToolTypeNames()
    {
        ToolTypeNames = new string[ToolTypeCount];
        ToolTypeNames[0] = "Lock";
    }

    protected override void InitToolTypeFunk()
    {
        ttf = new Tool.ToolTypeFunk[ToolTypeCount];
        ttf[0] = fLock;
    }

    void fLock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectedObject = pCamera.GetBody();
            if (SelectedObject != null)
            {
                SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.blue);
                ERigidbody erb = SelectedObject.GetComponent<ERigidbody>();
                if (erb.freezeX && erb.freezeY && erb.freezeZ)
                {
                    erb.freezeX = false;
                    erb.freezeY = false;
                    erb.freezeZ = false;
                }
                else
                {
                    erb.freezeX = true;
                    erb.freezeY = true;
                    erb.freezeZ = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedObject != null)
            {
                if (SelectedObject != null) SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.white);
                SelectedObject = null;
            }
        }
    }

    public override void Exit()
    {
        if (SelectedObject != null) SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.white);
        SelectedObject = null;
    }
}
