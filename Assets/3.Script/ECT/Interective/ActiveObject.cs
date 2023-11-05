using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour
{ 
    
  

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 'Window' �±׸� ���� ��� ������Ʈ�� ã���ϴ�.
            GameObject[] windows = GameObject.FindGameObjectsWithTag("Window");

            // ã�� ��� 'Window' ������Ʈ�� Ȱ��ȭ�մϴ�.
            foreach (GameObject window in windows)
            {
                Debug.Log(window);
                window.SetActive(false);
            }
        }
    }
}
