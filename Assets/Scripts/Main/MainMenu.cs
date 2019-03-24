using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject LevelMain;
    public GameObject LevelNewSim;

    public void GoMainMenu()
    {
        LevelNewSim.SetActive(false);
        LevelMain.SetActive(true);
    }

    public void NewSim()
    {
        LevelMain.SetActive(false);
        LevelNewSim.SetActive(true);
    }

    public void Load()
    {
        
    }

    public void Settings()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void Run()
    {
        Application.LoadLevel("DesktopScene");
    }
}
