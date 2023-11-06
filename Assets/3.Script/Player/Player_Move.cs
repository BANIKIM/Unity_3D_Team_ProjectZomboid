using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed = 4f;
    Animator animator;
    public GameObject Sound_Walk;
    public GameObject Sound_Run;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            Turn(movement);
            

            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetMouseButton(1))
            {
                Sound_Walk.SetActive(false);
                Sound_Run.SetActive(true);
                animator.SetBool("isRun", true);
                transform.position += movement * speed * 2f * Time.deltaTime;
            }
            else if (!Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1))
            {
                Sound_Walk.SetActive(false);
                Sound_Walk.SetActive(true);
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", true);
                transform.position += movement * speed * Time.deltaTime;
            }
        }
        else if (moveHorizontal == 0 && moveVertical == 0)
        {
            Sound_Run.SetActive(false);
            Sound_Walk.SetActive(false);
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", false);

        }


        transform.position += movement * speed * Time.deltaTime;
    }
    private void Turn(Vector3 movement)
    {
        Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);//�ش� �������� ĳ���Ͱ� �ٶ� 
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);//ĳ���� ����
    }

}