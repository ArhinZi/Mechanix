using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myPrimitive : myBody
{
    Sprite[] sprites;
    public enum BodyType { Circle, Rectangle }
    public BodyType bt;
    protected static int count = 0;
    public ERigidbody thisErigid;
    public myPrimitive(BodyType type, Vector3 scale, Vector3 pos)
    {
        sprites = new Sprite[2];
        sprites[0] = (Sprite)Resources.Load("Circle", typeof(Sprite));
        sprites[1] = (Sprite)Resources.Load("Rectangle", typeof(Sprite));
        switch (type)
        {
            case BodyType.Circle:
                thisGO = CreateCircle(scale, pos);
                bt = BodyType.Circle;
                break;
            case BodyType.Rectangle:
                thisGO = CreateRectangle(scale, pos);
                bt = BodyType.Rectangle;
                break;
            default:
                break;
        }
        thisErigid = thisGO.GetComponent<ERigidbody>();
    }
    protected GameObject CreateCircle(Vector3 scale, Vector3 pos)
    {
        GameObject go = new GameObject("Circle_GOid-" + count, typeof(BodyCtrl), typeof(SpriteRenderer), typeof(ERigidbody));
        go.GetComponent<BodyCtrl>().myBody = this;
        go.GetComponent<SpriteRenderer>().sprite = sprites[0];
        go.transform.localScale = scale;
        go.transform.position = pos;
        go.AddComponent<CircleCollider2D>();
        go.tag = "Body";
        count++;
        return go;
    }
    protected GameObject CreateRectangle(Vector3 scale, Vector3 pos)
    {
        GameObject go = new GameObject("Rectangle_GOid-" + count, typeof(BodyCtrl), typeof(SpriteRenderer), typeof(ERigidbody));
        go.GetComponent<BodyCtrl>().myBody = this;
        go.GetComponent<SpriteRenderer>().sprite = sprites[1];
        go.transform.localScale = scale;
        go.transform.position = pos;
        go.AddComponent<BoxCollider2D>();
        go.tag = "Body";
        count++;
        return go;
    }

    public override void SetColor(Color clr)
    {
        thisGO.GetComponent<SpriteRenderer>().color = clr;
    }
    public override void Delete()
    {
        Debug.Log(thisGO.name + " deleted");
    }

    public override void SetPosition(Vector3 p)
    {
        thisErigid.Velocity = Vector3.zero;
        thisErigid.AngVelocity = 0;
        thisGO.transform.position = p;
    }

    public override void SetRotate(Quaternion r)
    {
        thisErigid.Velocity = Vector3.zero;
        thisErigid.AngVelocity = 0;
        thisGO.transform.rotation = r;
    }

    public override void SetScale(Vector3 s)
    {
        thisErigid.Velocity = Vector3.zero;
        thisErigid.AngVelocity = 0;
        thisGO.transform.localScale = s;
    }


    //BodySettCtrl bsc = GameObject.Find("SettingsBody").GetComponent<BodySettCtrl>();
    public override void ShowSettings()
    {
        /*if (!bsc.gameObject.activeSelf)
        {
            bsc.gameObject.SetActive(true);
            bsc.cerb = thisErigid;
        }
        else
        {
            bsc.cerb = thisErigid;
        }*/
    }

    void Init()
    {
        
    }
}
