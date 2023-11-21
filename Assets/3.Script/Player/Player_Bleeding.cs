using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bleeding : MonoBehaviour
{
    [Header("Hitcoll 넣어주기")]
    [SerializeField] private HitColl hitColl;//맞은 부위를 가지고 오기 위한 참조
    [SerializeField] private Player_Banding[] Point; //포인트에서 밴딩을 했는지 불값을 가지고 오기위한 값

  
    public StatusController statusController;//체력을 감소시키기 위한 
    private bool[] hit_part; //출혈 부위를 담을 메서드

    private void Start()
    {
        hit_part = new bool[9];
        for (int i = 0; i < hit_part.Length; i++)
        {
            hit_part[i] = false;
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < hit_part.Length; i++)//엑티브 되었는지 확인하고 이제는 Point를 확인한다.
        {
            if (hitColl.BodyDmg[i].activeSelf == true) //상처파트를 엑티브한다.
            {
                hit_part[i] = true;
            }            
        }

        //이제 붕대를 감으면 데미지를 주지 않은 것을 만들자.
        StartCoroutine(player_Bleeding_co());
       


    }


    private IEnumerator player_Bleeding_co()
    {
        for (int i = 0; i < hit_part.Length; i++)
        {
            // hit_part[i]==true가 트루고 벤딩이 false가 되어있다면
            if (hit_part[i] == true && !Point[i].isBanding)
            {
                statusController.DecreaseHP(0.05f);//데미지를 1준다.
                hitColl.Bleeding.SetActive(true);
            }
            else if(hit_part[i] == true && Point[i].isBanding)
            {
                hitColl.Bleeding.SetActive(false);
            }
            
        }


        yield return null;
    }


    //필요하다 해당 부위에 대한 붕대질을 했을 때 출혈에 대한 bool 값을 꺼줄 방법이...
    //HitColl에서 값을 가지고 와서 비활성화 시킬려고 했는데 계속 랜덤값이라 문제가 생길거같다..
    //해당 포인트에 is벤딩이 트루가 된다면 그 값을 트루하면 될꺼같은데??맞을까??
}
