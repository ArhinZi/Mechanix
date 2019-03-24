using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolEngine : MonoBehaviour {


    Tool[,] tools;//массив классов Инструмент
    string[] toolgroup;//массив групп Инструментов
    public Sprite[] toolgroup_icon;//Массив спрайтов для групп Инструментов
    GameObject[] toolgroup_panel;//Массив панелей для групп Инструментов
    public GameObject Panelpref;//Префаб панели для групп Инструментов
    tPanel[] tpanel;//Массив классов Панель
    public Transform pTool;//Трансформ панели инструментов
    public Sprite sButton_act;//Спрайт активной кнопки
    public Sprite sButton_nact;//Спрайт неактивной кнопки
    public Sprite sButton_click;//Спрайт нажатой кнопки
    //public Sprite sPanel;//??????????????
    public GameObject[] Buttons;//Массив кнопок панели инструментов
    public Text ToolName;//Поле для вывода названия текущего инструмента
    public static Tool currentTool;//Текущий инструмент
    
    const int toolmassI = 9;
    const int toolmassJ = 5;
    void Start()
    {
        tools = new Tool[toolmassI, toolmassJ];

            //null
            tools[0,0] = new tSelect();
            //null
            tools[1,0] = new tDelete();
            //null
            tools[2,0] = new tMove();

            tools[3,0] = new tRotate();

            tools[4,0] = new tScale();

            tools[5,0] = new tLock();

            tools[6,0] = new tPrimitive();

            //tools[6,0] = new tComposite();

            tools[7,0] = new tForce();
            tools[8,0] = new tSingleJoint();


        toolgroup = new string[toolmassI];

            toolgroup[0] = null;
            /*toolgroup[1] = null;
            toolgroup[2] = null;
            /*toolgroup[3] = "Builder";
            toolgroup[4] = "AddForce";
            toolgroup[5] = "Joint";*/

        Buttons = new GameObject[toolmassI*toolmassJ];
        toolgroup_panel = new GameObject[toolmassI];
        tpanel = new tPanel[toolmassI];
        int iter = 0;
        for(int i = 0; i<toolmassI; i++)
        {
            if(toolgroup[i] == null)
            {
                tools[i,0].id = i+1;
                Buttons[iter] = CreateButton(tools[i,0].id, tools[i,0].icon);
                tools[i,0].pButton = Buttons[iter].GetComponent<Button>();
                Buttons[iter].GetComponent<Button>().onClick.AddListener( tools[i,0].Sel );
                Buttons[iter].transform.SetParent(pTool);
                iter++;
            }
            else
            {               
                Buttons[iter] = CreateButton(0, toolgroup_icon[i]);
                toolgroup_panel[i] = Instantiate(Panelpref);
                toolgroup_panel[i].transform.SetParent(Buttons[iter].transform);
                tpanel[i] = new tPanel();
                tpanel[i]._panel = toolgroup_panel[i];
                Buttons[iter].GetComponent<Button>().onClick.AddListener( tpanel[i].s_h ); 
                tpanel[i].s_h();
                Buttons[iter].transform.SetParent(pTool);
                iter++;
                for (int j = 0; j<toolmassJ; j++)
                {
                    if(tools[i,j] != null)
                    {
                        tools[i,j].id = i*10 + j;
                        Buttons[iter] = CreateButton(tools[i,j].id, tools[i,j].icon);
                        tools[i,0].pButton = Buttons[iter].GetComponent<Button>();
                        Buttons[iter].GetComponent<Button>().onClick.AddListener( tools[i,j].Sel );
                        Buttons[iter].transform.SetParent(toolgroup_panel[i].transform);
                        iter++;
                    }
                }
            }
        }
        tools[0,0].Sel(); //default
    }

    GameObject CreateButton(int id, Sprite icon)
    {
        GameObject _B = null;
            _B = new GameObject("Button " + id.ToString(), typeof(Image), typeof(Button));
            _B.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
            _B.GetComponent<Image>().sprite = sButton_act;
            //_B.GetComponent<Image>().raycastTarget = false;

            Button bb = _B.GetComponent<Button>();
            bb.transition = Selectable.Transition.SpriteSwap;

            SpriteState ss = new SpriteState();
            ss.highlightedSprite = sButton_act;
            ss.pressedSprite = sButton_click;
            ss.disabledSprite = sButton_nact;
            bb.spriteState = ss;

            GameObject ico = new GameObject("ico", typeof(Image));
            ico.GetComponent<Image>().sprite = icon;
            ico.GetComponent<Image>().raycastTarget = false;
            ico.transform.SetParent(_B.transform);
            ico.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);

        return _B;
    }

    void Update () {
        if(currentTool != null && currentTool._updType == Tool.updType.Update) currentTool.Run();
    }
    void FixedUpdate() {
        if(currentTool != null && currentTool._updType == Tool.updType.FixedUpdate) currentTool.Run();
    }
  

}
