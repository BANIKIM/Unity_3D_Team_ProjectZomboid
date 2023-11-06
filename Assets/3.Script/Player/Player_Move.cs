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
            animator.SetTrigger("isKickig");
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

        else if (moveHorizontal == 0 && moveVertical == 0) // �÷��̾� �ӵ��� 0�� �Ǿ�����
        {
            Sound_Run.SetActive(false);
            Sound_Walk.SetActive(false);
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", false);
        }
    }

    private void Rotate()
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
    /*void OnCollisionEnter(Collision collision)//����� �ٲ� ���� ������...
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            // �浹�� ������Ʈ�� �����ɴϴ�
            var otherObject = collision.gameObject;

            // ���� ������Ʈ�κ��� �ٸ� ������Ʈ�� ���ϴ� ���͸� �����ɴϴ�
            var direction = otherObject.transform.position - transform.position;

            // ���͸� ������ �ٸ� ������Ʈ�� �ڷ� �̴� ������ �����մϴ�
            direction = -direction;

            // �̵��� �Ÿ��Դϴ�
            float pushBackDistance = 3f;

            // �ٸ� ������Ʈ�� �ڷ� �̸��ϴ�
            otherObject.transform.position += direction.normalized * pushBackDistance;
            Push.SetActive(false);
        }
     
    }*/
}