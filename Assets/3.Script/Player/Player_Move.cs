using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [Header("�ȱ�ӵ� (�޸����*2��)")]
    public float speed = 3f;
    Animator animator;

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

    [Header("���� ���ӿ�����Ʈ")]
    public SphereCollider Sound;//���ӿ�����Ʈ

    public CapsuleCollider Man;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Sound =GetComponentInChildren<SphereCollider>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if(Input.GetButtonDown("Jump"))//�����̽� �����ÿ� ������ �δ�.
        {
            //animator.SetTrigger("isKickig");//�ִϸ��̼� ���
            //Push.SetActive(true);
        }

        //ĳ���� ȸ��
        if (Input.GetMouseButton(1))//���콺 ��Ŭ��
        {
            Rotate();
        }
        else if (movement != Vector3.zero) // ���콺�� �ٶ��� �ʴ� ��Ȳ���� �̵� ���̶��
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        }



        if (moveHorizontal != 0 || moveVertical != 0)
        {

            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetMouseButton(1))
            {

                animator.SetBool("isRun", true);
                Sound.radius = 15f;
                transform.position += movement * speed * 2f * Time.deltaTime;
                StartCoroutine(run_Sound());
                ;
            }
            else if (!Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1))
            {
  
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", true);
                Sound.radius = 10f;
                transform.position += movement * speed * Time.deltaTime;
                StartCoroutine(walking_Sound());
            }
        }

        if (moveHorizontal == 0 && moveVertical == 0) // �÷��̾� �ӵ��� 0�� �Ǿ�����
        {
            Sound.radius = 5f;
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", false);
        }
    }



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





}