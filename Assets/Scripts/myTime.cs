using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myTime : MonoBehaviour {

	public delegate void Pause();
	public static event Pause PauseOn;
	public static event Pause PauseOff;
	public static bool pause;
	public GameObject pauseicon;

	// Use this for initialization
	void Start () {
		SetPause(true);
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
		{
			SetPause(!pause);
		}
	}
	public void SetPause(bool p)
	{
		pause = p;
		if(p)
		{
			Debug.Log("Pause");
			Time.timeScale = 0;
			pauseicon.SetActive(true);
			if(PauseOn != null) PauseOn();
		} 
		else
		{
			Time.timeScale = 1;
			pauseicon.SetActive(false);
			if(PauseOff != null) PauseOff();
		}
	}
}
