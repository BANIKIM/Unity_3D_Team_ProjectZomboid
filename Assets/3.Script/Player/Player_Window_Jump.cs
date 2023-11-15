using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Window_Jump : MonoBehaviour
{
    [Header("�÷��̾ ��������")]
    [SerializeField] private Player_Move player;

    private void Start()
    {
        player = GetComponentInParent<Player_Move>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Window"))
        {
            if (Input.GetKeyDown(KeyCode.E))//â���� �����ִ��� Ȯ���� �ʿ�
            {
                player.animator.SetTrigger("isClimbing");
                if (player.transform.rotation.y < 0)//WD����
                {
                    player.transform.position = other.transform.position + new Vector3(-1f, 1f, 1f);
                    Debug.Log("WD����");
                    Debug.Log(player.transform.rotation.y);
                }
                else if (player.transform.rotation.y > 0)//S����
                {
                    player.transform.position = other.transform.position + new Vector3(-0.5f, 0, -0.5f);
                    Debug.Log("S����");
                    Debug.Log(player.transform.rotation.y);
                }
                

            }

        }
    }
}
