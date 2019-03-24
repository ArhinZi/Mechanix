using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class tSelect : Tool
{

    public GameObject SelectedObject;
    public GameObject SelectedObject2;
    private float time;

    public tSelect()
    {
        name = "Select";
        ToolTypeCount = 2;
        InitToolTypeIcons();
        InitToolTypeNames();
        InitToolTypeFunk();
        CurrToolType = 0;
        icon = (Sprite)Resources.Load("sel-ico", typeof(Sprite));
    }

    protected override void InitToolTypeNames()
    {
        ToolTypeNames = new string[ToolTypeCount];
        ToolTypeNames[0] = "Select";
        ToolTypeNames[1] = "ForceInfo";
    }

    protected override void InitToolTypeFunk()
    {
        ttf = new Tool.ToolTypeFunk[ToolTypeCount];
        ttf[0] = fSelect;
        ttf[1] = fForceInfo;
    }

    void fSelect()
    {

        if (Input.GetMouseButtonDown(0))
        {
            SelectedObject = pCamera.GetBody();
            if (SelectedObject != null)
            {
                ((myPrimitive)SelectedObject.GetComponent<BodyCtrl>().myBody).ShowSettings();
            }

        }
    }

    void fForceInfo()
    {
        if (SelectedObject != null)
        {
            SelectedObject.GetComponent<ERigidbody>().SetDispForces();
            List<string> ss = SelectedObject.GetComponent<ERigidbody>().DispForces();
            ClearProp();
            int i = 0;
            foreach (string item in ss)
            {
                ActivateProp(i);
                SetProp(i, item);
                i++;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectedObject != null)
            {
                SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.white);
                SelectedObject.GetComponent<ERigidbody>().DisplayForces = false;
            }
            ClearProp();
            SelectedObject = pCamera.GetBody();
            if (SelectedObject != null)
            {
                SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.yellow);
                SelectedObject.GetComponent<ERigidbody>().DisplayForces = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ClearProp();
        }
    }

    public override void Exit()
    {
        if (SelectedObject != null)
        {
            SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.white);
            SelectedObject.GetComponent<ERigidbody>().DisplayForces = false;
        }
        SelectedObject = null;
        ClearProp();
    }

}
