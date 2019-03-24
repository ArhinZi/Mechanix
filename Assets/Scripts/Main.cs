using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    bool visible_help;
    public GameObject help;

	bool visible_esc;
    public GameObject esc;
    // Use this for initialization
    void Start()
    {
        s_h_help(false);
		s_h_esc(false);
        Physics2D.gravity = new Vector2(0, (-1) * PhizSettings.WorldGrav * PhizSettings.scale);
        Time.fixedDeltaTime = PhizSettings.FixedTimestep;
        myPrimitive mp = new myPrimitive(myPrimitive.BodyType.Rectangle, new Vector2(300,10), new Vector2(0,-50));
        mp.thisErigid.freezeX = true;
        mp.thisErigid.freezeY = true;
        mp.thisErigid.freezeZ = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            s_h_help(!visible_help);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            s_h_esc(!visible_esc);
        }
    }

    void s_h_help(bool a)
    {
        visible_help = a;
        help.SetActive(a);
    }
	void s_h_esc(bool a)
    {
        visible_esc = a;
        esc.SetActive(a);
    }
}
