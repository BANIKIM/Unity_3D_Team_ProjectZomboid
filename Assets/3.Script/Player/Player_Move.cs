using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed = 4f;
    Animator animator;
    public GameObject Sound_Walk;
    public GameObject Sound_Run;
    public GameObject Push;
    [SerializeField] private Camera followCamera;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if(Input.GetButtonDown("Jump"))//�����̽� �����ÿ� ������ �δ�.
        {
            animator.SetTrigger("isKickig");//�ִϸ��̼� ���
            Push.SetActive(true);
        }


        if (moveHorizontal != 0 || moveVertical != 0)
        {

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
        if (Input.GetMouseButton(1))//���콺 ��Ŭ��
        {
            Rotate(); 
        }
        else if (movement != Vector3.zero) // ���콺�� �ٶ��� �ʴ� ��Ȳ���� �̵� ���̶��
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        }

        if (moveHorizontal == 0 && moveVertical == 0) // �÷��̾� �ӵ��� 0�� �Ǿ�����
        {
            Sound_Run.SetActive(false);
            Sound_Walk.SetActive(false);
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", false);
        }
    }

    private void Rotate()//ȸ�� �޼���
    {

        //���콺 ȸ��
        Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayhit;
        if (Physics.Raycast(ray, out rayhit, 100))
        {
            Vector3 nextVec = rayhit.point - transform.position;
            // nextVec.x = 0;
            nextVec.y = 0;
            // nextVec.z = 0;
            transform.LookAt(transform.position + nextVec);
        }
    }

/*    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            Push.SetActive(false);

        }

    }*/

}