using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "PJunYeong";

    public static Title instance;

    private SaveAndLoad theSaveAndLoad;

    //�̱��� �� �̵����� �ı����� �ʰ� 
    private void Awake()
    {
       

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void ClickStart()
    {
        Debug.Log("�ε� ");
        gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
        // "GameTitle"�� Canvas�� ��� ��Ȱ��ȭ
    }
    public void ClickLoad()
    {
        Debug.Log("�ε� ");

       
        StartCoroutine(LoadCor());
      
    }
    IEnumerator LoadCor()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone) //�ε��� ������ �ʴ´ٸ� 
        {
            yield return null;
        }
        
        theSaveAndLoad = FindObjectOfType<SaveAndLoad>();
        theSaveAndLoad.LoadData();
        gameObject.SetActive(false);  // "GameTitle"�� Canvas�� ��� ��Ȱ��ȭ

    }
    public void ClickExit()
    {
        Debug.Log("����");
        Application.Quit();
    }
   
}
