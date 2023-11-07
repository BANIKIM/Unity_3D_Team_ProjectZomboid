using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [Header("�ִϸ��̼� ��Ʈ�ѷ�")]
    public Animator anim;
    [Header("��������")]
    public bool isAttack;//������ ���� bool��
    [Header("�����Ҹ�")]
    public AudioClip BatSwing;
    private AudioSource audioSource;
    [Header("�÷��̾������Ʈ")]
    [SerializeField] private Gun_Shot gun_Shot;
    [Header("�ǻ��������Ʈ")]
    public GameObject Sound_Gun;
    private bool IsMovement=false;


    [Header("��Ƽ���� ��Ʈ��")]
    //� �ִ� ��Ʈ
    [SerializeField] private GameObject Bat_Spine;
    private bool Bat_In=true;    
    //�տ� �ִ� ��Ʈ
    [SerializeField] private GameObject Bat_Hand;
    private bool Bat_Out;


    [Header("�׽�Ʈ������")]
    public bool Bat_Get;//��Ʈ�� ������ �ֳ�?    
    public bool Melee_weapon;
    public bool Range_weapon;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gun_Shot = GetComponent<Gun_Shot>();

    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")&&!IsMovement)//�����ϱ� �ٶ���...Todo �ʿ���ٸ� ���� �ϱ�...
        {
            anim.SetLayerWeight(1, 0);
            anim.SetTrigger("isKickig");
            IsMovement = true;
            Invoke("isMovement", 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !IsMovement)
        {
            anim.SetLayerWeight(1, 1);//��ü �ִϸ��̼� ���
            Debug.Log("��������~~~");
            Bat_take_out(Bat_Get);

        }

        if (Input.GetMouseButton(1))//��Ŭ����
        {

            anim.SetLayerWeight(1, 1);//��ü �ִϸ��̼� ���
            if (Melee_weapon)//�������⸦ ��� �ִٸ�
            {
                anim.SetBool("isWeapon", true);//�������� ����ڼ��� ����ϰ�
                if (Input.GetMouseButtonDown(0) && !IsMovement)//��Ŭ���� �ϸ�
                {
                    anim.SetTrigger("isSwing");//���������� �Ѵ�
                    isAttack = true;
                    Invoke("BatSWingClip", 0.3f);
                    IsMovement = true;
                    Invoke("isMovement", 1f);
                }
                
            }
            else if (Range_weapon)
            {
                anim.SetBool("isAiming", true);
                if (Input.GetMouseButton(0) && !IsMovement)//�ѽ��
                {

                    gun_Shot.ShotEvent();
                    anim.SetTrigger("isFiring");
                    Sound_Gun.SetActive(false);
                    IsMovement = true;
                    Invoke("isMovement", 0.3f);
                }
            }
   
        }
        else if(Input.GetMouseButtonUp(1))
        {
            anim.SetBool("isWeapon", false);
            anim.SetBool("isAiming", false);
            anim.SetLayerWeight(1, 0);
        }
    }

    private void FixedUpdate()
    {
        if (Range_weapon==true)
        {
            anim.SetLayerWeight(1, 1);//��ü �ִϸ��̼� ���
            anim.SetBool("isGun", true);
        }
    }

    private void BatSWingClip()
    {
        audioSource.PlayOneShot(BatSwing);

    }

    private void Bat_take_out(bool Bat)// ��Ʈ�� �̴� �ִϸ��̼�
    {
        if (Bat)
        {
            Debug.Log("��Ʈ���־�");
            if (Bat_In)
            {
                Debug.Log("��ڿ� �־�");
                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Bat_Spine, Bat_Hand, false, true));
            }
            else if (Bat_Out)
            {
                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Bat_Hand, Bat_Spine, true, false));
            }
        }
    }

    private IEnumerator ActivateWithDelay(GameObject objectToDisable, GameObject objectToEnable, bool newBatIn, bool newBatOut)
    {
        yield return new WaitForSeconds(0.8f); // ��� �ð��� 1�ʷ� �����߽��ϴ�. ���ϴ� �ð����� ���� �����մϴ�.
        objectToDisable.SetActive(false);
        objectToEnable.SetActive(true);
        Bat_In = newBatIn;
        Bat_Out = newBatOut;
    }

    private void isMovement()//�ൿ���ɿ���
    {
        IsMovement = false;
    }


}
