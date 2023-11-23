using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Window_Jump : MonoBehaviour
{
    [Header("�÷��̾ ��������")]
    [SerializeField] private Player_Move player;

    float keydown = 0f;
    private void Start()
    {
        player = GetComponentInParent<Player_Move>();

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Window"))
        {
            if (Input.GetKey(KeyCode.E)) // â���� �����ִ��� Ȯ���� �ʿ�
            {
                keydown += Time.deltaTime;
                WIndow_bool door = other.GetComponent<WIndow_bool>();

                if (door != null) // Door_bool ������Ʈ�� �����Ѵٴ� �� Ȯ��
                {

                    if (keydown >= 0.8f && door.isOpen)
                    {
                        player.animator.SetTrigger("isClimbing");

                        if (player.transform.rotation.y < 0) //WD����
                        {
                            player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y - 4f, 0);


                        }
                        else if (player.transform.rotation.y > 0) //S����
                        {
                            player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y - 4f, 0);


                        }
                        keydown = 0f;
                    }
                }
                else
                {

                }
            }
            else
            {
                keydown = 0f;
            }
        }
        else if (other.gameObject.CompareTag("Fence"))
        {
            if (Input.GetKey(KeyCode.E)) // â���� �����ִ��� Ȯ���� �ʿ�
            {
                player.animator.SetTrigger("isClimbing");

                if (player.transform.rotation.y < 0) //WD����
                {
                    player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y+2f, 0);


                }
                else if (player.transform.rotation.y > 0) //S����
                {
                    player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y + 2f, 0);


                }
            }
            else if(Input.GetKey(KeyCode.LeftShift))
            {
                player.animator.SetTrigger("isFence");

                if (player.transform.rotation.y < 0) //WD����
                {
                    player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y, 0);


                }
                else if (player.transform.rotation.y > 0) //S����
                {
                    player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y, 0);


                }
            }
        }
    }
}

