using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSimMenu : MonoBehaviour
{


    public GameObject LevelMain;
    // Use this for initialization
    public void Resume()
    {
        this.gameObject.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
