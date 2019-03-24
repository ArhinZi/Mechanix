using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force {

	public enum FT{Local,World}
	public string name;
	public bool isfict;
	public GameObject parent;
	public Vector2 position;
	public Vector2 force_value;
	public FT ForceType;
	public Color color;

	public Force(string n, GameObject par, Vector2 pos, Vector2 f_v, bool is_fict = false, Color c = default(Color))
	{
		name = n;
		isfict = is_fict;
		parent = par;
		position = pos;
		force_value = f_v;
		color = c;
	}


}
