using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Animator anim;
    public bool isAttack;//������ ���� bool��
    public AudioClip BatSwing;
    private AudioSource audioSource;

    public bool Melee_weapon;
    public bool Range_weapon;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (Range_weapon)
        {
            anim.SetBool("isGun", true);
        }

            if (Input.GetMouseButton(1))//��Ŭ����
        {
            anim.SetLayerWeight(1, 1);//��ü �ִϸ��̼� ���
            if (Melee_weapon)//�������⸦ ��� �ִٸ�
            {
                anim.SetBool("isWeapon",true);//�������� ����ڼ��� ����ϰ�
                if (Input.GetMouseButtonDown(0))//��Ŭ���� �ϸ�
                {
                    anim.SetTrigger("isSwing");//���������� �Ѵ�
                    isAttack = true;
                    Invoke("BatSWingClip", 0.3f);
                }              
            }
            else if (Range_weapon)
            {
                anim.SetBool("isGun", true);
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("��");
                }
            }
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }

    private void BatSWingClip()
    {
        audioSource.PlayOneShot(BatSwing);

    }
}
