using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewLoad : MonoBehaviour
{
    private string sceneName = "MainGame_Fake";
    public Text text1;
    public Text text2;
    public Button continueButton;
 

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

    public void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
