using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    // ü��
    [SerializeField]
    private int hp;  // �ִ� ü��. ����Ƽ ������ ���Կ��� ������ ��.
    private float currentHp;

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

    private bool isSP = false;
    private Coroutine recoverStaminaCoroutine;

    // ���¹̳� ���� ����
    private bool spUsed;

    // ����
    [SerializeField]
    private int dp;  // �ִ� ����. ����Ƽ ������ ���Կ��� ������ ��.
    private int currentDp;

    //�ٷ� 
    [SerializeField]
    private int att;
    private int currentAtt;

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
    private Image image_Gauge;
    [SerializeField]
    private GameObject image_Thirsty;
    [SerializeField]
    private GameObject image_Hungry;

    //��������
    [SerializeField] private HitColl hitColl;


    private const int  DP = 0, SP = 1,ATT=2, HUNGRY = 3, THIRSTY = 4;


    //���̽� ���� �ҷ������ ���� 
    public float GetcurrentHP() { return currentHp; }
    public int GetcurrentDP() { return currentDp; }
    public int GetcurrentSP() { return currentSp; }
    public int GetcurrentAtt() { return currentAtt; }
    public int GetcurrentHungry() { return currentHungry;}
    public int GetcurrentThirsty() { return currentThirsty; }
    public void SetcurrentHP(float LoadHp)
    {
        currentHp = LoadHp;
    }
    public void SetcurrentDP(int LoadDp)
    {
        currentDp = LoadDp;
    }
    public void SetcurrentSP(int LoadSp)
    {
        currentSp = LoadSp;
    }
    public void SetcurrentAtt(int LoadAtt)
    {
        currentAtt = LoadAtt;
    }
    public void SetcurrentHungry(int LoadHungry)
    {
        currentHungry = LoadHungry;
    }
    public void SetcurrentThirsty(int LoadThirsty)
    {
        currentThirsty = LoadThirsty;
    }

    private void Start()
    {
        currentHp = hp;
        currentDp = dp;
        currentSp = sp;
        currentAtt = att;
        currentHungry = hungry;
        currentThirsty = thirsty;

       // image_Thirsty.enabled = true;
    }
    private void Update()
    {
        if (!GameManager.isPause)
        {
            Hungry();
            Thirsty();
            SPRechargeTime();
           
            GagueUpdate();
        }
          
            
    }
    
    public void increaseHP(float _count)
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
    public bool DecreaseHP(float _count)
    {
       
        currentHp -= _count;
        if (currentHp <= 0 && !hitColl.isDie)
        {
            hitColl.Player_Die();
            hitColl.isDie = true;
            return true;
        }
        return false;
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
           
        }
    }

    public void increaseATT(int _count)
    {
        if (currentAtt + _count > att)
        {
            currentAtt += _count;
        }
        else
        {
            currentAtt = att;
        }
    }

    //���� 
    public void SPRecover()
    {
        //V�� ������ ���鼭 ���׹̳ʰ� ä������.
        if (true)
        {
            if (!isSP)
            {
                recoverStaminaCoroutine = StartCoroutine(RecoverStamina());
                isSP = true;
            }
            else
            {
                if (currentSp < sp)
                {
                    StopCoroutine(recoverStaminaCoroutine);
                }
                isSP = false;
            }
        }
    }

    IEnumerator RecoverStamina()
    {
        isSP = true;
        while (currentSp < sp)
        {
            yield return new WaitForSeconds(0.1f);
            currentSp += spIncreaseSpeed;
        }
        isSP = false;
    }

    private void SPRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime<spRechargeTime)
            {
                currentSpRechargeTime++;
            }
            else
            {
                spUsed = false;
            }
        }
    }

    public void increaseSP(int _count)
    {
        if (currentSp < sp)
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
        spUsed = true;
        currentSpRechargeTime = 0;
        if (currentSp > 0)
        {
            currentSp -= _count;
        }
        else
        {
            currentSp = 0;
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
            if (currentHungry<30)
            {
                image_Hungry.gameObject.SetActive(true);
            }
            if (currentHungry>70)
            {
                image_Hungry.gameObject.SetActive(false);
            }
        }
      
          
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
           
            // currentThirsty�� 30���� ������ �̹����� Ȱ��ȭ
            if (currentThirsty < 30)
            {
                image_Thirsty.gameObject.SetActive(true);
                //image_Thirsty.enabled = false;
            }
            if (currentThirsty>70)
            {
                image_Thirsty.gameObject.SetActive(false);
                //image_Thirsty.enabled = true;
            }
        }
      
           
    }
    private void GagueUpdate()
    {
        image_Gauge.fillAmount=(float)currentHp / hp;
    }





}
