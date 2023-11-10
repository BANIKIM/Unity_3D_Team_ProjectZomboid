using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    public Text textToBlink;
    public float blinkInterval = 0.5f;

    void Start()
    {
        // �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(Blink_Text());
    }

    IEnumerator Blink_Text()
    {
        while (true)
        {
            // �ؽ�Ʈ�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�մϴ�.
            textToBlink.enabled = !textToBlink.enabled;

            // ������ ���ݸ�ŭ ��ٸ��ϴ�.
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
