using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    // ü��
    [SerializeField]
    private int hp;  // �ִ� ü��. ����Ƽ ������ ���Կ��� ������ ��.
    private int currentHp;

    // ���¹̳�
    [SerializeField]
    private int sp;  // �ִ� ���¹̳�. ����Ƽ ������ ���Կ��� ������ ��.
    private int currentSp;

    // ���¹̳� ������
    [SerializeField]
    private int spIncreaseSpeed;

    // ���¹̳� ��ȸ�� ������ �ð�
    [SerializeField]
    private int spRechargeTime;
    private int currentSpRechargeTime;

    // ���¹̳� ���� ����
    private bool spUsed;

    // ����
    [SerializeField]
    private int dp;  // �ִ� ����. ����Ƽ ������ ���Կ��� ������ ��.
    private int currentDp;

    // �����
    [SerializeField]
    private int hungry;  // �ִ� �����. ����Ƽ ������ ���Կ��� ������ ��.
    private int currentHungry;

    // ������� �پ��� �ӵ�
    [SerializeField]
    private int hungryDecreaseTime;
    private int currentHungryDecreaseTime;

    // �񸶸�
    [SerializeField]
    private int thirsty;  // �ִ� �񸶸�. ����Ƽ ������ ���Կ��� ������ ��.
    private int currentThirsty;

    // �񸶸��� �پ��� �ӵ�
    [SerializeField]
    private int thirstyDecreaseTime;
    private int currentThirstyDecreaseTime;

    [SerializeField]
    private GameObject openStatus;

    [SerializeField]
    private Image image_Gauge;
    [SerializeField]
    private Text[] text_Update;
    private const int  DP = 0, SP = 1, HUNGRY = 2, THIRSTY = 3;

    private void Start()
    {
        currentHp = hp;
        currentDp = dp;
        currentSp = sp;
        currentHungry = hungry;
        currentThirsty = thirsty;
    }
    private void Update()
    {
   
            Hungry();
            Thirsty();
            GagueUpdate();
            TextUdate();
      
     
    }
  
    public void increaseHP(int _count)
    {
        if (currentHp + _count < hp)
        {
            currentHp += _count;
        }
        else
        {
            currentHp = hp;
        }

    }
    public void DecreaseHP(int _count)
    {
        //������ ��� ���� ����
        if (currentDp > 0)
        {
            DecreaseDP(_count);
            return;
        }
        currentHp -= _count;
        if (currentHp <= 0)
        {
            Debug.Log("ĳ������ hp�� 0�Դϴ�.");
        }
    }
    public void increaseDP(int _count)
    {
        if (currentDp + _count < dp)
        {
            currentDp += _count;
        }
        else
        {
            currentDp = dp;
        }

    }

    public void DecreaseDP(int _count)
    {

        currentDp -= _count;
        if (currentDp <= 0)
        {
            Debug.Log("ĳ������ dp�� 0�Դϴ�.");
        }
    }
    public void increaseSP(int _count)
    {
        if (currentSp + _count < sp)
        {
            currentSp += _count;
        }
        else
        {
            currentSp = sp;
        }

    }

    public void DecreaseSP(int _count)
    {

        currentSp -= _count;
        if (currentSp <= 0)
        {
            Debug.Log("ĳ������ dp�� 0�Դϴ�.");
        }
    }


    public void increaseHungry(int _count)
    {

        if (currentHungry + _count < hungry)
        {
            currentHungry += _count;
        }
        else currentHungry = hungry;
    }
    public void DecreaseHungry(int _count)
    {
        if (currentHungry - _count < 0)
        {
            currentHungry = 0;
        }
        else
        {
            currentHungry -= _count;
        }


    }


    public void increaseThirsty(int _count)
    {

        if (currentThirsty + _count < thirsty)
        {
            currentThirsty += _count;
        }
        else currentThirsty = thirsty;
    }
    public void DecreaseThirsty(int _count)
    {
        if (currentThirsty - _count < 0)
        {
            currentThirsty = 0;
        }
        else
        {
            currentThirsty -= _count;
        }


    }


    private void Hungry()
    {
        if (currentHungry > 0)
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)
            {
                currentHungryDecreaseTime++;
            }
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else
            Debug.Log("����� ��ġ�� 0�� �Ǿ����ϴ�.");
    }
    private void Thirsty()
    {
        if (currentThirsty >0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
            {
                currentThirstyDecreaseTime++;
            }
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
            Debug.Log("�񸶸� ��ġ�� 0�� �Ǿ����ϴ�.");
    }
    private void GagueUpdate()
    {
        image_Gauge.fillAmount=(float)currentHp / hp;
    }
    private void TextUdate()
    {

        text_Update[DP].text = "���� " + currentDp + " / " + dp;
        text_Update[SP].text = "���¹̳�: " + currentSp + " / " + sp;
        text_Update[HUNGRY].text = "�����: " + currentHungry + " / " + hungry;
        text_Update[THIRSTY].text = "�񸶸�: " + currentThirsty + " / " + thirsty;
        
    }
    public void ToggleStat()
    {

        openStatus.SetActive(!openStatus.activeSelf);

    }



}
