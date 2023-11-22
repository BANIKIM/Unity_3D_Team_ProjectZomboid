using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    private string sceneName = "MainGame_Fake";
    private GameObject canvas;
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
            instance.Find_teset();
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (canvas == null && SceneManager.GetActiveScene().name.Equals("GameLoad"))
        {
            canvas = GameObject.Find("Canvas");
        }
        StartCoroutine(ShowText1());
    }

    public void Find_teset() {
        if (canvas == null && SceneManager.GetActiveScene().name.Equals("GameLoad"))
        {
            canvas = GameObject.Find("Canvas");
        }
        StartCoroutine(ShowText1());
    }
    private void FindObject()
    {
        // Button Event add
        if (canvas != null)
        {
            continueButton = canvas.transform.GetChild(3).gameObject.transform.GetComponent<Button>();
            // button addListener
            continueButton.onClick.AddListener(ContinueToNextScene);
        }
    }

    IEnumerator ShowText1()
    {
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(3f); // �ؽ�Ʈ 1�� 3�� ���� ������

        canvas.transform.GetChild(1).gameObject.SetActive(false); // �ؽ�Ʈ 1�� ��Ȱ��ȭ
        StartCoroutine(ShowText2());
    }

    IEnumerator ShowText2()
    {
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(3f); // �ؽ�Ʈ 2�� 3�� ���� ������

        canvas.transform.GetChild(2).gameObject.SetActive(false); // �ؽ�Ʈ 2�� ��Ȱ��ȭ
        canvas.transform.GetChild(3).gameObject.SetActive(true); // ��ư Ȱ��ȭ
        FindObject();
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
    }
}
