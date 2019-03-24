using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCtrl : MonoBehaviour {

	public myBody myBody;

	void OnDestroy()
	{
		myBody.Delete();
	}
}
