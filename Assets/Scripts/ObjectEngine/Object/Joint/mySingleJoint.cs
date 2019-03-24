using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mySingleJoint : myJoint
{
    GameObject pref;
    GameObject JObj;
    public HingeJoint2D comp;
    public mySingleJoint(GameObject bd, Vector2 pos)
    {
        pref = (GameObject)Resources.Load("Prefs/TJoint", typeof(GameObject));
        GameObject temp = GameObject.Instantiate(pref);
        temp.AddComponent<JointCtrl>();
        temp.GetComponent<JointCtrl>().myJoint = this;

        JObj = GameObject.Find("Main Camera").GetComponent<camera>().GetBody();
        temp.transform.position = JObj.transform.TransformPoint(pos);
        comp = JObj.AddComponent<HingeJoint2D>();
        comp.anchor = pos;
    }

    public Vector2 GetPos()
    {
        return (comp.connectedAnchor);
    }

    public override void Delete()
    {
        //Debug.Log(JObj.name + " deleted");
        GameObject.Destroy(comp);

    }

    public override void ShowSettings()
    {
        throw new System.NotImplementedException();
    }
}
