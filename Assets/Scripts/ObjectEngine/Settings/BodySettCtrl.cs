using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySettCtrl : MonoBehaviour
{


    public ERigidbody cerb = null;
    public float PosX
    {
        set
        {
            cerb.transform.position = new Vector3(value, cerb.transform.position.y, cerb.transform.position.z);
        }
        get
        {
            return cerb.transform.position.x;
        }
    }
    public float PosY
    {
        set
        {
            cerb.transform.position = new Vector3(cerb.transform.position.x, value, cerb.transform.position.z);
        }
        get
        {
            return cerb.transform.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cerb != null)
        {

        }
    }
}
