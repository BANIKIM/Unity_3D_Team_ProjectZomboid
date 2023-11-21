using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bleeding : MonoBehaviour
{
    [Header("Hitcoll �־��ֱ�")]
    [SerializeField] private HitColl hitColl;//���� ������ ������ ���� ���� ����
    [SerializeField] private Player_Banding[] Point; //����Ʈ���� ����� �ߴ��� �Ұ��� ������ �������� ��

  
    public StatusController statusController;//ü���� ���ҽ�Ű�� ���� 
    private bool[] hit_part; //���� ������ ���� �޼���

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
        for (int i = 0; i < hit_part.Length; i++)//��Ƽ�� �Ǿ����� Ȯ���ϰ� ������ Point�� Ȯ���Ѵ�.
        {
            if (hitColl.BodyDmg[i].activeSelf == true) //��ó��Ʈ�� ��Ƽ���Ѵ�.
            {
                hit_part[i] = true;
            }            
        }

        //���� �ش븦 ������ �������� ���� ���� ���� ������.
        StartCoroutine(player_Bleeding_co());
       


    }


    private IEnumerator player_Bleeding_co()
    {
        for (int i = 0; i < hit_part.Length; i++)
        {
            // hit_part[i]==true�� Ʈ��� ������ false�� �Ǿ��ִٸ�
            if (hit_part[i] == true && !Point[i].isBanding)
            {
                statusController.DecreaseHP(0.05f);//�������� 1�ش�.
                hitColl.Bleeding.SetActive(true);
            }
            else if(hit_part[i] == true && Point[i].isBanding)
            {
                hitColl.Bleeding.SetActive(false);
            }
            
        }


        yield return null;
    }


    //�ʿ��ϴ� �ش� ������ ���� �ش����� ���� �� ������ ���� bool ���� ���� �����...
    //HitColl���� ���� ������ �ͼ� ��Ȱ��ȭ ��ų���� �ߴµ� ��� �������̶� ������ ����Ű���..
    //�ش� ����Ʈ�� is������ Ʈ�簡 �ȴٸ� �� ���� Ʈ���ϸ� �ɲ�������??������??
}
