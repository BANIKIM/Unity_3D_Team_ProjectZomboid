using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI;
    [SerializeField] private SaveAndLoad theSaveAndLoad;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!GameManager.isPause)
            {
                CallMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }
    private void CallMenu()
    {
        GameManager.isPause = true;
        go_BaseUI.SetActive(true);
        Time.timeScale = 0f;
    }
    private void CloseMenu()
    {
        GameManager.isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = 1f;

    }
    public void ClickSave()
    {
        Debug.Log("세이브 ");
        theSaveAndLoad.SaveData();
    }
    public void ClicKLoad()
    {
        Debug.Log("로드 ");
        theSaveAndLoad.LoadData();
    }
    public void ClickExit()
    {
        Debug.Log("종료 ");
        Application.Quit();
    }
}

