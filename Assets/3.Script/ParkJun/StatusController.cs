using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    // 체력
    [SerializeField]
    private int hp;  // 최대 체력. 유니티 에디터 슬롯에서 지정할 것.
    private float currentHp;

    // 스태미나
    [SerializeField]
    private int sp;  // 최대 스태미나. 유니티 에디터 슬롯에서 지정할 것.
    private int currentSp;

    // 스태미나 증가량
    [SerializeField]
    private int spIncreaseSpeed;

    // 스태미나 재회복 딜레이 시간
    [SerializeField]
    private int spRechargeTime;
    private int currentSpRechargeTime;

    private bool isSP = false;
    private Coroutine recoverStaminaCoroutine;

    // 스태미나 감소 여부
    private bool spUsed;

    // 방어력
    [SerializeField]
    private int dp;  // 최대 방어력. 유니티 에디터 슬롯에서 지정할 것.
    private int currentDp;

    //근력 
    [SerializeField]
    private int att;
    private int currentAtt;

    // 배고픔
    [SerializeField]
    private int hungry;  // 최대 배고픔. 유니티 에디터 슬롯에서 지정할 것.
    private int currentHungry;

    // 배고픔이 줄어드는 속도
    [SerializeField]
    private int hungryDecreaseTime;
    private int currentHungryDecreaseTime;

    // 목마름
    [SerializeField]
    private int thirsty;  // 최대 목마름. 유니티 에디터 슬롯에서 지정할 것.
    private int currentThirsty;

    // 목마름이 줄어드는 속도
    [SerializeField]
    private int thirstyDecreaseTime;
    private int currentThirstyDecreaseTime;


    [SerializeField]
    private Image image_Gauge;
    [SerializeField]
    private GameObject image_Thirsty;
    [SerializeField]
    private GameObject image_Hungry;

    private const int  DP = 0, SP = 1,ATT=2, HUNGRY = 3, THIRSTY = 4;


    //제이슨 저장 불러오기용 스탯 
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
            Hungry();
            Thirsty();
            SPRechargeTime();
            SPRecover();
            GagueUpdate();
            
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
    public bool DecreaseHP(float _count)
    {
       
        currentHp -= _count;
        if (currentHp <= 0)
        {
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
            Debug.Log("캐릭터의 dp가 0입니다.");
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

    //쉬기 
    private void SPRecover()
    {
        //V를 누르면 쉬면서 스테미너가 채워진다.
        if (Input.GetKeyDown(KeyCode.V))
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
        if (currentSp + _count < sp)
        {
            currentSp += _count;
        }
        else
        {
            currentSp = sp;
        }
        Debug.Log("sp"+_count);

    }

    public void DecreaseSP(int _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;
        if (currentSp - _count>0)
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
        Debug.Log("배고픔"+_count);
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
        Debug.Log("목마름"+_count);
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
        else
            Debug.Log("배고픔 수치가 0이 되었습니다.");
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
           
            // currentThirsty가 30보다 작으면 이미지를 활성화
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
        else
            Debug.Log("목마름 수치가 0이 되었습니다.");
    }
    private void GagueUpdate()
    {
        image_Gauge.fillAmount=(float)currentHp / hp;
    }





}
