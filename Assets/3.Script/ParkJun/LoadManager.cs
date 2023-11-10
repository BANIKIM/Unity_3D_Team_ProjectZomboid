using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public string sceneName = "PJunYeong";
    public Text text1;
    public Text text2;
    public Button continueButton;
    private AsyncOperation operation;
    private SaveAndLoad thesaveAndLoad;

    public static LoadManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(ShowText1());
    }

    IEnumerator ShowText1()
    {
        text1.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f); // �ؽ�Ʈ 1�� 3�� ���� ������

        text1.gameObject.SetActive(false); // �ؽ�Ʈ 1�� ��Ȱ��ȭ
        StartCoroutine(ShowText2());
    }

    IEnumerator ShowText2()
    {
        text2.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f); // �ؽ�Ʈ 2�� 3�� ���� ������

        text2.gameObject.SetActive(false); // �ؽ�Ʈ 2�� ��Ȱ��ȭ
        continueButton.gameObject.SetActive(true); // ��ư Ȱ��ȭ
    }

    // ��ư�� �����Ͽ� ȣ��� �Լ�
    public void ContinueToNextScene()
    {
        StartCoroutine(LoadCoroutine());
    }
  

    IEnumerator LoadCoroutine()
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            if (operation.progress < 0.9f)
            {
                timer = 0f;
            }
            else
            {
                operation.allowSceneActivation = true;
            }
        }

        thesaveAndLoad = FindObjectOfType<SaveAndLoad>(); // ���� ���� SaveAndLoad
        thesaveAndLoad.LoadData();
        gameObject.SetActive(false);
     
    }
}
