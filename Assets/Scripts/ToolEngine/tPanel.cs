using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tPanel {

	public GameObject _panel;


	public void s_h()
	{
		_panel.SetActive(!_panel.activeSelf);
	}
	public void s_h(bool a)
	{
		_panel.SetActive(a);
	}
}
