using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitColl : MonoBehaviour
{
    [Header("B_pos, hit������Ʈ")]
    public Transform Hit_pos;//��Ƣ�°�
    public GameObject hit;//�ǿ�����Ʈ����


    public void Hit()//�ǻմ� �޼���
    {
        Instantiate(hit, Hit_pos.transform.position, Hit_pos.transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ZombieAttack") && other.gameObject.CompareTag("ZombieAttack"))
        {
            Hit();
            Debug.Log("���� ������");
        }
    }
}
