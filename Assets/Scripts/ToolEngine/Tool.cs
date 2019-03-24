using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Tool
{
    public enum updType { Update, FixedUpdate, LateUpdate }
    protected static camera pCamera = GameObject.Find("Main Camera").GetComponent<camera>();
    public GameObject props = GameObject.Find("Properties");
    public int id;
    public string name;
    public int CurrToolType;
    public Image ToolTypeIco;
    public Text ToolTypeName;
    public int ToolTypeCount;
    public Sprite[] ToolTypeIcons;
    public string[] ToolTypeNames;
    public Sprite icon;
    protected delegate void ToolTypeFunk();
    protected ToolTypeFunk[] ttf;
    public Button pButton;

    public updType _updType;


    public Tool()
    {
        ToolTypeIco = GameObject.Find("ToolTypeIco").GetComponent<Image>();
        ToolTypeName = GameObject.Find("ToolTypeText").GetComponent<Text>();
        _updType = updType.Update;
    }
    public void Sel()
    {
        if(ToolEngine.currentTool != null) ToolEngine.currentTool.Exit();
        ToolEngine.currentTool = this;
        GameObject.Find("ToolName").GetComponent<Text>().text = name + "(id" + id + ")";
        Debug.Log("Выбран инструмент id" + id);
    }
    public void Run()
    {
        GameObject ai = GameObject.Find("Canvas/position");
        ai.GetComponent<Text>().text = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).ToString();
        if(Input.GetAxis("Mouse ScrollWheel")!=0) Exit();
        CurrToolType += (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
        if (CurrToolType < 0) CurrToolType = ToolTypeCount - 1;
        if (CurrToolType > ToolTypeCount - 1) CurrToolType = 0;
        ToolTypeIco.sprite = ToolTypeIcons[CurrToolType];
        ToolTypeName.text = ToolTypeNames[CurrToolType];
        ttf[CurrToolType]();
    }

    public virtual void Exit() { }
    protected void InitToolTypeIcons()
    {
        ToolTypeIcons = new Sprite[ToolTypeCount];
        for (int i = 0; i < ToolTypeCount; i++)
        {
            ToolTypeIcons[i] = (Sprite)Resources.Load("Tools/" + name + "/" + (i + 1), typeof(Sprite));
        }
    }

    protected abstract void InitToolTypeNames();
    protected abstract void InitToolTypeFunk();

    protected void ActivateProp(int id)
    {
        GameObject ai = GameObject.Find("Canvas/Properties/p" + id);
        ai.SetActive(true);
    }
    protected void SetProp(int id, string Prop)
    {
        GameObject ai = GameObject.Find("Canvas/Properties/p" + id);
        ai.GetComponent<Text>().text = Prop;
    }
    protected void ClearProp()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject ai = GameObject.Find("Canvas/Properties/p" + i);
            ai.SetActive(false);
            ai.GetComponent<Text>().text = "";
        }
    }
}
