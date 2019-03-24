using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Timer : MonoBehaviour
{

    bool visible;
    public myTime t;
    float TimeInSec;
    public GameObject part0;
    public GameObject part1;
    public Text timeField;
    public Dropdown DropDown1;
    public Dropdown DropDown2;
    public Text tdd1;
    public Text tdd2;
    bool isPlay;

    GameObject[] gos;
    public GameObject obj1;
    public GameObject obj2;
    // Use this for initialization
    void Start()
    {
        isPlay = false;
        TimeInSec = 0;
        timeField.text = "0" + " s.";
        s_h(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            s_h(!visible);
        }

        if (isPlay)
        {
            TimeInSec += Time.deltaTime;
            timeField.text = TimeInSec.ToString("#.##") + " s.";
        }
        else
        {
            DropDown1.ClearOptions();
            DropDown2.ClearOptions();
            gos = GameObject.FindGameObjectsWithTag("Body");
            foreach (GameObject item in gos)
            {
                Dropdown.OptionData k = new Dropdown.OptionData();
                k.text = item.name;
                DropDown1.options.Add(k);
                DropDown2.options.Add(k);
            }
            if (obj1 == null && obj2 == null)
            {
                try
                {
                    Set1Obj(0);
                    Set2Obj(0);
                }
                catch { }
            }
        }
        try
        {
            tdd1.text = obj1.name;
            tdd2.text = obj2.name;
        }
        catch { }
    }

    public void B_Stop()
    {
        isPlay = false;
        TimeInSec = 0;
        timeField.text = "0" + " s.";
    }
    public void B_Play()
    {
        Debug.Log("Timer-Start");
        isPlay = !isPlay;
    }

    public void s_h(bool a)
    {
        visible = a;
        part0.SetActive(a);
        part1.SetActive(a);

    }

    public void Set1Obj(int k)
    {
        if (obj1 != null) obj1.GetComponent<ERigidbody>().timerColl = null;
        obj1 = gos[k];
        if (obj2 != null) obj1.GetComponent<ERigidbody>().timerColl = obj2;
    }
    public void Set2Obj(int k)
    {
        obj2 = gos[k];
        if (obj1 != null) obj1.GetComponent<ERigidbody>().timerColl = obj2;
    }
}
