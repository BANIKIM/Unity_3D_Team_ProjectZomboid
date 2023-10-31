using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed = 4f;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);//�ش� �������� ĳ���Ͱ� �ٶ� 
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);//ĳ���� ����

            if(Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRun", true);
                transform.position += movement * speed*2f * Time.deltaTime;
            }
            else if(!Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRun", false);
                animator.SetBool("isWalik", true);
                transform.position += movement * speed * Time.deltaTime;
            }
        }
        else if (moveHorizontal == 0 && moveVertical == 0)
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isWalik", false);
            
        }

        
        transform.position += movement * speed * Time.deltaTime;
    }
}