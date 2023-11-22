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
    [Header("���� ���ӿ�����Ʈ")]
    public GameObject Sound_Gun;//���ӿ�����Ʈ
    private bool IsMovement = false;

    [Header("���콺 Ŀ��")]
    public Game_Cursor game_Cursor;

    [Header("��Ƽ���� ��Ʈ��")]
    //� �ִ� ��Ʈ
    [SerializeField] private GameObject Bat_Spine;
    private bool Bat_In = true;
    //�տ� �ִ� ��Ʈ
    [SerializeField] private GameObject Bat_Hand;
    private bool Bat_Out;

    [Header("calf������Ʈ �־��ּ���")]
    [SerializeField] private GameObject calf_l;
    [SerializeField] private GameObject calf_r;

    //� �ִ� ��
    [SerializeField] private GameObject Gun_Spine;
    private bool Gun_In = true;
    //�տ� �ִ� ��
    [SerializeField] private GameObject Gun_Hand;

    [Header("�κ��丮")]
    [SerializeField] private Inventory inventory;
    private bool Gun_Out;
    private int bullet = 0;//�κ��丮 �Ѿ� ��
    private int magazine = 0;//źâ �� 


    [Header("Level_Up")]
    [SerializeField] private Player_Level_Up level_up;
    [SerializeField] private GameObject[] health;//ü��
    [SerializeField] private GameObject[] strength;//��
    [SerializeField] private GameObject[] mace;//�� �б�
    [SerializeField] private GameObject[] aiming;//����
    //�������� ���� Ƚ��
    private int health_exe = 0; //ü��
    private int strength_exe = 0; // ��
    private int mace_exe = 0; //��б�
    private int aiming_exe = 0; //����

    [Header("�׽�Ʈ������")]
    public bool Bat_Get;//��Ʈ�� ������ �ֳ�?    
    public bool Gun_Get;
    public bool Melee_weapon;
    public bool Range_weapon;


    

    [SerializeField] private RightClickMenu rightClickMenu;
    float keydown = 0f;
    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gun_Shot = GetComponent<Gun_Shot>();


    }

    private void Update()
    {

        //������ Ȯ��
        if(health_exe >=10) // ������
        {
            Debug.Log("������");//����� �ٲܰ�
            level_up.Level_up(health);
            health_exe = 0;//����ġ �ʱ�ȭ
        }
        else if(strength_exe >= 10)
        {
            Debug.Log("������");//����� �ٲܰ�
            level_up.Level_up(strength);
            strength_exe = 0;//����ġ �ʱ�ȭ
        }
        else if(mace_exe >= 5)
        {
            Debug.Log("������");//����� �ٲܰ�
            level_up.Level_up(mace);
            mace_exe = 0;//����ġ �ʱ�ȭ
        }
        else if(aiming_exe >= 5)
        {
            Debug.Log("������");//����� �ٲܰ�
            level_up.Level_up(aiming);
            aiming_exe = 0;//����ġ �ʱ�ȭ
        }

        for (int i = 0; i < inventory.slots.Length; i++)//�������� ã�Ҵµ� ��� �ؾ��ұ� 
        {
            if (inventory.slots[i].itemName == "Bullet")
            {
                bullet = inventory.slots[i].itemCount;
            }
        }
        Debug.Log("�Ѿ˰���" + bullet);

        if (Melee_weapon)
        {
            anim.SetBool("isWeapon", true);
        }
        else if (!Melee_weapon)
        {
            anim.SetBool("isWeapon", false);
        }
        if (Range_weapon && !IsMovement)
        {
            anim.SetBool("isGun", true);

            if (Input.GetKeyDown(KeyCode.R) && !IsMovement)//������
            {
                IsMovement = true; //�ൿ����
                Debug.Log("��������Ĭ��Ĭ");
                Invoke("isMovement", 1.5f);

                if (bullet >= 20)
                {
                    bullet -= 20;//20�߰���
                    for (int i = 0; i < inventory.slots.Length; i++)
                    {
                        if (inventory.slots[i].itemName == "Bullet")
                        {
                            inventory.slots[i].SetSlotCount(-20);
                        }                       
                    }
                    magazine += 20;//20������
                }
                else if (bullet > 0)
                {
                   
                    magazine += bullet;
                    for (int i = 0; i < inventory.slots.Length; i++)
                    {
                        if (inventory.slots[i].itemName == "Bullet")
                        {
                            inventory.slots[i].SetSlotCount(-bullet);
                        }
                    }
                    bullet = 0;
                }
                else
                {
                    
                }
            }

            anim.SetLayerWeight(1, 1);//��ü �ִϸ��̼� ���

        }
        else if (!Range_weapon)
        {

            anim.SetBool("isGun", false);
            anim.SetLayerWeight(1, 1);//��ü �ִϸ��̼� ���

        }


        if (Input.GetButtonDown("Jump") && !IsMovement)//�����ϱ� �ٶ���...Todo �ʿ���ٸ� ���� �ϱ�...
        {
            health_exe += 1;//����ġ�߰�
            anim.SetLayerWeight(1, 0);
            anim.SetTrigger("isKickig");
            IsMovement = true;
            Invoke("isKick", 0.5f);
            Invoke("isMovement", 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !IsMovement)
        {
            anim.SetLayerWeight(1, 1);//��ü �ִϸ��̼� ���
            Debug.Log("��������~~~");

            if (Bat_Get)
            {
                Bat_take_out(Bat_Get);
            }
            else if (Gun_Get)
            {
                IsMovement = true;
                Gun_take_out(Gun_Get);
                Invoke("isMovement", 1f);
            }


        }

        if (Input.GetMouseButton(1))//��Ŭ����
        {
            keydown += Time.deltaTime;

            if(keydown>0.8f)
            {
                rightClickMenu.isAim = true;
                game_Cursor.OnMouseOver();
                anim.SetLayerWeight(1, 1);//��ü �ִϸ��̼� ���
                anim.SetBool("isRight_click", true);

                if (Melee_weapon)//�и��� �ִ��� �Ѵ� ������
                {
                    if (Input.GetMouseButtonDown(0) && !IsMovement)//��Ŭ���� �ϸ�
                    {
                        anim.SetTrigger("isSwing");//���������� �Ѵ�
                        isAttack = true;
                        Invoke("BatSWingClip", 0.3f);
                        IsMovement = true;
                        Invoke("isMovement", 1f);
                        mace_exe += 1; //���̽� ����ġ �߰�
                        strength_exe += 1;//����ġ�߰�
                    }
                }
                if (!Melee_weapon && !Range_weapon)//�Ѵ� ������
                {
                    if (Input.GetMouseButtonDown(0) && !IsMovement)//��Ŭ���� �ϸ�
                    {
                        anim.SetTrigger("isSwing");//��´�.
                        Invoke("isStomp", 0.5f);
                        isAttack = true;
                        IsMovement = true;
                        Invoke("isMovement", 1.1f);
                        strength_exe += 1;//����ġ�߰�
                    }
                }

                else if (Range_weapon)
                {
                    anim.SetBool("isAiming", true);//������

                    if (Input.GetMouseButton(0) && !IsMovement && magazine > 0)//�ѽ��
                    {

                        gun_Shot.ShotEvent();
                        anim.SetTrigger("isFiring");
                        Sound_Gun.SetActive(true);
                        IsMovement = true;
                        Invoke("isMovement", 0.3f);
                        magazine -= 1;
                        aiming_exe += 1; // �ѱ����ġ
                        StartCoroutine(Sound_Gun_false_co());
                    }
                    else if (Input.GetMouseButton(0) && !IsMovement && magazine == 0)
                    {
                        IsMovement = true;
                        Invoke("isMovement", 0.3f);
                        //�Ѿ� ���¼Ҹ� ��Ĭ��Ĭ
                        Debug.Log("��Ĭ��Ĭ");
                    }

                }
            }


        }
        if (Input.GetMouseButtonUp(1))
        {
            keydown = 0f;
            rightClickMenu.isAim = false;
            game_Cursor.OnMouseExit();
            anim.SetBool("isRight_click", false);
            anim.SetBool("isAiming", false);
            anim.SetLayerWeight(1, 0);
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

            if (Bat_In)
            {

                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Bat_Spine, Bat_Hand, false, true));
                Melee_weapon = true;
                Gun_Hand.SetActive(false);
                Range_weapon = false;
            }
            else if (Bat_Out)
            {
                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Bat_Hand, Bat_Spine, true, false));
                Melee_weapon = false;

            }
        }
    }

    private void Gun_take_out(bool Gun)// ���� �̴� �ִϸ��̼�
    {
        if (Gun)
        {

            if (Gun_In)
            {

                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Gun_Spine, Gun_Hand, false, true));
                Range_weapon = true;
                Gun_In = false;
                Gun_Out = true;
            }
            else if (Gun_Out)
            {
                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Gun_Hand, Gun_Spine, true, false));
                Range_weapon = false;
                Gun_In = true;
                Gun_Out = false;
                Bat_Hand.SetActive(false);
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
    private IEnumerator Sound_Gun_false_co()
    {
        yield return new WaitForSeconds(1f);
        Sound_Gun.SetActive(false);
    }
    private void isMovement()//�ൿ���ɿ���
    {
        IsMovement = false;
        if (calf_l == true)
        {
            calf_l.SetActive(false);
        }
        if (calf_r == true)
        {
            calf_r.SetActive(false);
        }

    }

    private void isKick()
    {
        calf_l.SetActive(true);
    }

    private void isStomp()
    {
        calf_r.SetActive(true);
    }

}
