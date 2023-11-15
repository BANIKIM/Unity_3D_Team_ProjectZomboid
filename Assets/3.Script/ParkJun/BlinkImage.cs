using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    public Image ImageToBlink;
    public float blinkInterval = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlinkImageRoutine());
    }

    // �����Ÿ��� ��ƾ
    IEnumerator BlinkImageRoutine()
    {
        while (true)
        {
            // �̹����� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�մϴ�.
            ImageToBlink.enabled = !ImageToBlink.enabled;

            // ������ ���ݸ�ŭ ��ٸ��ϴ�.
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
