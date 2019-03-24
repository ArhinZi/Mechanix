using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class myBody : myObject {

    protected GameObject thisGO;
    public abstract void SetColor(Color clr);
    public abstract void SetPosition(Vector3 p);
    public abstract void SetRotate(Quaternion r);
    public abstract void SetScale(Vector3 s);
}
