using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp; //����ü��
    [SerializeField]
    private float destroyTime; //�ĸ� ���� �ð�
    [SerializeField]
    private SphereCollider COL; //��ü �ݶ��̴� 

    //�ʿ��� ���� ������Ʈ 
    [SerializeField]
    private GameObject go_rock; //�Ϲ� ����
    [SerializeField]
    private GameObject go_debris; //���� ���� 
    [SerializeField]
    private GameObject go_effect_Prefabs; //ä�� ����Ʈ 

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip effectClip1;
    [SerializeField]
    private AudioClip effectClip2;

    public void Mining()
    {
        audioSource.clip = effectClip1;
        audioSource.Play();
        var clone = Instantiate(go_effect_Prefabs, COL.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);


        hp--;
        if (hp<=0)
        {
            Destruction();
        }
    }
    private void Destruction()
    {
        audioSource.clip = effectClip2;
        audioSource.Play();

        COL.enabled = false;
        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            Debug.Log("������ �¾Ҵ�!");

            Mining();

        }

        
    }
}
