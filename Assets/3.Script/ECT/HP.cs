using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float hP = 100f;
    //public float Current_HP;


    //�ʱ�ü�� ����
    public float Start_HP(float Current_HP)
    {
        Current_HP = hP;

        return Current_HP;
    }


    //������
    public float Damage(float damage, float current_HP) 
    {
        current_HP = current_HP - damage;

        return current_HP;
    }

    //ȸ��
    public float Recovery(float Current_HP, float recovery_HP)
    {
        Current_HP += recovery_HP;
        if(Current_HP> hP)
        {
            Current_HP = hP;
        }
        return Current_HP;
    }

}
