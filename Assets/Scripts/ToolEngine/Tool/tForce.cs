using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tForce : Tool
{
    public Vector2 pos1 = Vector2.zero;
    public Vector2 pos2 = Vector2.zero;
    public float Dist = 0;
    public GameObject SelectedObject;
    public tForce()
    {
        name = "Force";
        ToolTypeCount = 1;
        InitToolTypeIcons();
        InitToolTypeNames();
        InitToolTypeFunk();
        CurrToolType = 0;
        icon = (Sprite)Resources.Load("force-ico", typeof(Sprite));
    }

    protected override void InitToolTypeNames()
    {
        ToolTypeNames = new string[ToolTypeCount];
        ToolTypeNames[0] = "Force";
    }

    protected override void InitToolTypeFunk()
    {
        ttf = new Tool.ToolTypeFunk[ToolTypeCount];
        ttf[0] = fForce;
    }

    void fForce()
    {
        _updType = updType.FixedUpdate;
        if (SelectedObject != null)
        {
            SelectedObject.GetComponent<ERigidbody>().SetDispForces();
            List<string> ss = SelectedObject.GetComponent<ERigidbody>().DispForces();
            ClearProp();
            int i = 1;
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
                SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.grey);
                SelectedObject.GetComponent<ERigidbody>().DisplayForces = true;
                pos1 = SelectedObject.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                ActivateProp(0);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (SelectedObject != null)
            {
                Vector2 _pos1 = SelectedObject.transform.TransformPoint(pos1);
                pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 force = (pos2 - _pos1) * 10 * SelectedObject.GetComponent<ERigidbody>().mass;
                SelectedObject.GetComponent<ERigidbody>().AddForceAtPosition(force, _pos1, ForceMode2D.Force);
                Debug.DrawLine(_pos1, pos2, Color.red, 0.01f, true);
                SelectedObject.GetComponent<ERigidbody>().Velocity = SelectedObject.GetComponent<ERigidbody>().Velocity * 0.9999f;
                ActivateProp(0);
                SetProp(0, "Force: " + force / PhizSettings.scale + "H;");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedObject != null)
            {
                if (SelectedObject != null) SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.white);
                SelectedObject.GetComponent<ERigidbody>().DisplayForces = false;
                SelectedObject = null;
                ClearProp();
            }
        }
    }
    /*
        void fconstForce()
        {
            _updType = updType.FixedUpdate;
            if (Input.GetMouseButtonDown(0))
            {
                SelectedObject = pCamera.GetObj();
                if (SelectedObject != null)
                {
                    SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.blue);
                    pos1 = SelectedObject.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (SelectedObject != null)
                {
                    pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (SelectedObject != null)
                {
                    Vector2 _pos1 = SelectedObject.transform.TransformPoint(pos1);
                    SelectedObject.GetComponent<ERigidbody>().AddConstForceAtPosition((pos2 - _pos1) * 10, _pos1, ForceMode2D.Force);
                    Debug.DrawLine(_pos1, pos2, Color.red, 100, true);
                    if (SelectedObject != null) SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.white);
                    SelectedObject = null;
                }
            }
        }
    */

    public override void Exit()
    {
        if (SelectedObject != null)
        {
            SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.white);
            SelectedObject.GetComponent<ERigidbody>().DisplayForces = false;
        }
        if (SelectedObject != null) SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.white);
        SelectedObject = null;
        _updType = updType.Update;
    }


}
