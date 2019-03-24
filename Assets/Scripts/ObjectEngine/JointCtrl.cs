using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointCtrl : MonoBehaviour {

	public myJoint myJoint;

	void Update()
	{
		this.transform.position = ((mySingleJoint)myJoint).GetPos();
	}
	void OnDestroy()
	{
		myJoint.Delete();
	}
}
