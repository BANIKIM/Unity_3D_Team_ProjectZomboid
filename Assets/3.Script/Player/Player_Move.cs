using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [Header("걷기속도 (달리기는*2임)")]
    public float speed = 4f;
    Animator animator;
    [Header("게임오브젝트")]
    public GameObject Sound_Walk;
    public GameObject Sound_Run;
    public GameObject Push;
    [Header("메인카메라")]
    [SerializeField] private Camera followCamera;

    [Header("발소리(사운드클립)")]
    public AudioClip FootSteps;
    private AudioSource audioSource;
    public AudioClip RunSteps;
    [Header("죽음_PZ_MaleBeingEaten_Death사운드 넣기")]
    public AudioClip Death;

    [Header("B_pos, hit오브젝트")]
    public Transform Hit_pos;//피튀는곳
    public GameObject hit;//피오브젝트선언
    private Plane plane;//바닦선언

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
        if(Input.GetButtonDown("Jump"))//스페이스 누를시에 상대방을 민다.
        {
            //animator.SetTrigger("isKickig");//애니메이션 재생
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
        if (Input.GetMouseButton(1))//마우스 우클릭
        {
            Rotate(); 
        }
        else if (movement != Vector3.zero) // 마우스를 바라보지 않는 상황에서 이동 중이라면
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        }

        if (moveHorizontal == 0 && moveVertical == 0) // 플레이어 속도가 0이 되었을때
        {
            Sound_Run.SetActive(false);
            Sound_Walk.SetActive(false);
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", false);
        }
    }

    /*    private void Rotate()//회전 메서드
        {

            //마우스 회전
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


    private void Rotate()//오차범위 조준테스트
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 카메라의 월드포지션을 가져온다

        plane = new Plane(Vector3.up, transform.position); // Vector.up 방향으로 point에 평면이 만들어진다.
        float rayDistance;// 변수선언

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

    public void Hit()//피뿜는 메서드
    {
        Instantiate(hit, Hit_pos.transform.position, Hit_pos.transform.rotation);
    }

}