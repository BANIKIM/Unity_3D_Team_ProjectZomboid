using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string NewName = "GameNew";
    public string loadName = "GameLoad";



    public void ClickStart()
    {
        Debug.Log("�ε� ");
        
        SceneManager.LoadScene(NewName);
       
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
