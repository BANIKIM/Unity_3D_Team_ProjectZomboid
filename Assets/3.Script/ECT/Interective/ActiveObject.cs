using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour
{ 
    
  

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 'Window' 태그를 가진 모든 오브젝트를 찾습니다.
            GameObject[] windows = GameObject.FindGameObjectsWithTag("Window");

            // 찾은 모든 'Window' 오브젝트를 활성화합니다.
            foreach (GameObject window in windows)
            {
                Debug.Log(window);
                window.SetActive(false);
            }
        }
    }
}
