using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [Header("�ȱ�ӵ� (�޸����*2��)")]
    public float speed = 4f;
    Animator animator;
    [Header("���ӿ�����Ʈ")]
    public GameObject Sound_Walk;
    public GameObject Sound_Run;
    public GameObject Push;
    [Header("����ī�޶�")]
    [SerializeField] private Camera followCamera;

    [Header("�߼Ҹ�(����Ŭ��)")]
    public AudioClip FootSteps;
    private AudioSource audioSource;
    public AudioClip RunSteps;
    [Header("����_PZ_MaleBeingEaten_Death���� �ֱ�")]
    public AudioClip Death;

    [Header("B_pos, hit������Ʈ")]
    public Transform Hit_pos;//��Ƣ�°�
    public GameObject hit;//�ǿ�����Ʈ����
    private Plane plane;//�ٴۼ���

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if(Input.GetButtonDown("Jump"))//�����̽� �����ÿ� ������ �δ�.
        {
            //animator.SetTrigger("isKickig");//�ִϸ��̼� ���
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
                StartCoroutine(run_Sound());
                ;
            }
            else if (!Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1))
            {
                Sound_Walk.SetActive(false);
                Sound_Walk.SetActive(true);
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", true);
                transform.position += movement * speed * Time.deltaTime;
                StartCoroutine(walking_Sound());
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

    /*    private void Rotate()//ȸ�� �޼���
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
        }*/


    private void Rotate()//�������� �����׽�Ʈ
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ī�޶��� ������������ �����´�

        plane = new Plane(Vector3.up, transform.position); // Vector.up �������� point�� ����� ���������.
        float rayDistance;// ��������

        if (plane.Raycast(ray, out rayDistance))
        {
            Vector3 pointToLook = ray.GetPoint(rayDistance);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

           /* Quaternion toRotation = Quaternion.LookRotation(pointToLook, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);*/
        }
    }

    private bool isWalkingSoundPlaying = false;

    private IEnumerator walking_Sound()
    {
        if (isWalkingSoundPlaying)
        {
            yield break;
        }

        isWalkingSoundPlaying = true;
        audioSource.PlayOneShot(FootSteps);
        yield return new WaitForSeconds(0.5f);

        isWalkingSoundPlaying = false;
    }

    private IEnumerator run_Sound()
    {
        if (isWalkingSoundPlaying)
        {
            yield break;
        }

        isWalkingSoundPlaying = true;
        audioSource.PlayOneShot(FootSteps);
        yield return new WaitForSeconds(0.35f);

        isWalkingSoundPlaying = false;
    }


    private void Die()
    {
        audioSource.PlayOneShot(Death);
    }

    public void Hit()//�ǻմ� �޼���
    {
        Instantiate(hit, Hit_pos.transform.position, Hit_pos.transform.rotation);
    }

}