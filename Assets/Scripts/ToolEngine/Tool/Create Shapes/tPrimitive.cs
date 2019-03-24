using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tPrimitive : Tool
{

    public Vector2 pos1 = Vector2.zero;
    public Vector2 pos2 = Vector2.zero;
    public float Rad = 0;
    GameObject temp;
    public int numCircle = 0;
    public int numRect = 0;

    public bool isset;

    public tPrimitive()
    {
        name = "Create Primitive";
        ToolTypeCount = 2;
        InitToolTypeIcons();
        InitToolTypeNames();
        InitToolTypeFunk();
        CurrToolType = 0;
        icon = (Sprite)Resources.Load("prim-ico", typeof(Sprite));
    }

    protected override void InitToolTypeNames()
    {
        ToolTypeNames = new string[ToolTypeCount];
        ToolTypeNames[0] = "Circle";
        ToolTypeNames[1] = "Rectangle";
    }

    protected override void InitToolTypeFunk()
    {
        ttf = new Tool.ToolTypeFunk[ToolTypeCount];
        ttf[0] = fCircle;
        ttf[1] = fRectangle;
    }

    void fCircle()
    {
        if (myTime.pause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isset = true;
                pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                temp = new GameObject("circle");
                temp.AddComponent<SpriteRenderer>();
                temp.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Circle", typeof(Sprite));
                temp.transform.position = pos1;
                ActivateProp(0);
            }
            if (Input.GetMouseButton(0))
            {
                pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Rad = Vector2.Distance(pos1, pos2);
                Rad /= 10;
                Rad = Mathf.Floor(Rad);
                //Debug.Log(Rad);
                temp.transform.localScale = new Vector3(Rad, Rad, 0);
                SetProp(0, "Radius: " + Rad);
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isset)
                {
                    if (Rad > 0.5)
                    {
                        new myPrimitive(myPrimitive.BodyType.Circle, temp.transform.localScale, temp.transform.position);
                    }
                    GameObject.Destroy(temp);
                    isset = !isset;
                    temp = null;
                    ClearProp();
                }
            }
        }
    }
    void fRectangle()
    {
        if (myTime.pause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isset = true;
                pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                temp = new GameObject("rectangle");
                temp.AddComponent<SpriteRenderer>();
                temp.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Rectangle", typeof(Sprite));
                ActivateProp(0);
            }
            if (Input.GetMouseButton(0))
            {
                pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                temp.transform.position = new Vector3(((pos1 + pos2) / 2).x, ((pos1 + pos2) / 2).y, 0);
                temp.transform.localScale = new Vector3(Mathf.Floor(GetRect(pos1, pos2).width / 10), Mathf.Floor(GetRect(pos1, pos2).height / 10), 0);
                SetProp(0, "Scale: " + temp.transform.localScale);
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isset)
                {
                    if (Mathf.Abs(pos1.x - pos2.x) > 5 && Mathf.Abs(pos1.y - pos2.y) > 5)
                    {
                        try{
                            new myPrimitive(myPrimitive.BodyType.Rectangle, temp.transform.localScale, temp.transform.position);
                        }
                        catch{}
                    }
                    GameObject.Destroy(temp);
                    isset = !isset;
                    ClearProp();
                }
            }
        }
    }

    Rect GetRect(Vector3 p1, Vector3 p2)
    {
        p1.y = Screen.height - p1.y;
        p2.y = Screen.height - p2.y;

        Vector3 TopLeft = Vector3.Min(p1, p2);
        Vector3 BottomRight = Vector3.Max(p1, p2);
        return Rect.MinMaxRect(TopLeft.x, TopLeft.y, BottomRight.x, BottomRight.y);
    }

    public override void Exit()
    {
        GameObject.Destroy(temp);
        isset = !isset;
        temp = null;
        ClearProp();
    }
}
