using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tScale : Tool
{

    public GameObject SelectedObject;
    public Vector2 pos1 = Vector2.zero;
    public Vector2 pos2 = Vector2.zero;
    public float d1;
    Vector3 a = Vector3.zero;
    public float Dist;

    public tScale()
    {
        name = "Scale";
        ToolTypeCount = 2;
        InitToolTypeIcons();
        InitToolTypeNames();
        InitToolTypeFunk();
        CurrToolType = 0;
        icon = (Sprite)Resources.Load("scale-ico", typeof(Sprite));
    }

    protected override void InitToolTypeNames()
    {
        ToolTypeNames = new string[ToolTypeCount];
        ToolTypeNames[0] = "ScaleP";
        ToolTypeNames[1] = "ScaleM";
    }

    protected override void InitToolTypeFunk()
    {
        ttf = new Tool.ToolTypeFunk[ToolTypeCount];
        ttf[0] = fScaleP;
        ttf[1] = fScaleM;
    }

    void fScaleP()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectedObject = pCamera.GetBody();
            if (SelectedObject != null)
            {
                SelectedObject.GetComponent<SpriteRenderer>().color = Color.green;
                pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                d1 = Vector3.Distance(pos1, SelectedObject.transform.position);
                a = SelectedObject.transform.localScale;
                ERigidbody irigid = SelectedObject.GetComponent<ERigidbody>();
                irigid.GravScale = 0;
                irigid.Velocity = Vector3.zero;
                irigid.AngVelocity = 0;
                ActivateProp(0);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (SelectedObject != null)
            {
                pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Dist = Vector2.Distance(SelectedObject.transform.position, pos2) - d1;
                SelectedObject.transform.localScale = a * (1 + Dist / 1000);
                SetProp(0, "Scale: " + SelectedObject.transform.localScale);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedObject != null)
            {
                ERigidbody irigid = SelectedObject.GetComponent<ERigidbody>();
                irigid.GravScale = 1;
                if (SelectedObject != null) SelectedObject.GetComponent<SpriteRenderer>().color = Color.white;
                SelectedObject = null;
                ClearProp();
            }
        }
    }

    void fScaleM()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectedObject = pCamera.GetBody();
            if (SelectedObject != null)
            {
                SelectedObject.GetComponent<SpriteRenderer>().color = Color.green;
                pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                d1 = Vector3.Distance(pos1, SelectedObject.transform.position);
                a = SelectedObject.transform.localScale;
                ERigidbody irigid = SelectedObject.GetComponent<ERigidbody>();
                irigid.GravScale = 0;
                irigid.Velocity = Vector3.zero;
                irigid.AngVelocity = 0;
                ActivateProp(0);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (SelectedObject != null)
            {
                pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Dist = Vector2.Distance(SelectedObject.transform.position, pos2) - d1;
                SelectedObject.transform.localScale = a / (1 + Dist / 1000);
                SetProp(0, "Scale: " + SelectedObject.transform.localScale);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedObject != null)
            {
                ERigidbody irigid = SelectedObject.GetComponent<ERigidbody>();
                irigid.GravScale = 1;
                if (SelectedObject != null) SelectedObject.GetComponent<SpriteRenderer>().color = Color.white;
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
