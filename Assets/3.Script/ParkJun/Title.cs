using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "PJunYeong";
    public string loadName = "GameLoad";



    public void ClickStart()
    {
        Debug.Log("�ε� ");
        
        SceneManager.LoadScene(sceneName);
       
    }
    public void ClickLoad()
    {
        Debug.Log("�ε� ");


        SceneManager.LoadScene(loadName);
       
      
    }

    public void ClickExit()
    {
        Debug.Log("����");
        Application.Quit();
    }
   
}
