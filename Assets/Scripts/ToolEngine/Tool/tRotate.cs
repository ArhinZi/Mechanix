using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tRotate : Tool
{

    public GameObject SelectedObject;
    public Vector2 pos1 = Vector2.zero;
    public Vector2 pos2 = Vector2.zero;
    public float d1;
    Quaternion a = new Quaternion();
    public float Dist;

    public tRotate()
    {
        name = "Rotate";
        ToolTypeCount = 1;
        InitToolTypeIcons();
        InitToolTypeNames();
        InitToolTypeFunk();
        CurrToolType = 0;
        icon = (Sprite)Resources.Load("rot-ico", typeof(Sprite));
    }

    protected override void InitToolTypeNames()
    {
        ToolTypeNames = new string[ToolTypeCount];
        ToolTypeNames[0] = "Rotate";
    }

    protected override void InitToolTypeFunk()
    {
        ttf = new Tool.ToolTypeFunk[ToolTypeCount];
        ttf[0] = fRotate;
    }

    void fRotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectedObject = pCamera.GetBody();
            if (SelectedObject != null)
            {
                SelectedObject.GetComponent<BodyCtrl>().myBody.SetColor(Color.green);
                pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                d1 = Vector3.Distance(pos1, SelectedObject.transform.position);
                a = SelectedObject.transform.rotation;
                ActivateProp(0);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (SelectedObject != null)
            {
                //if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Dist = Vector2.Distance(SelectedObject.transform.position, pos2) - d1;
                    Quaternion ai = a * Quaternion.Euler(0, 0, Mathf.Floor(Dist / 15));
                    SelectedObject.GetComponent<BodyCtrl>().myBody.SetRotate(ai);
                    SetProp(0, "Angle: " + SelectedObject.transform.localRotation.eulerAngles.z + " deg");
                }
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
