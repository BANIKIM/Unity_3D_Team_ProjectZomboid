using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitColl : MonoBehaviour
{
    [Header("B_pos, hit������Ʈ")]
    public Transform Hit_pos;//��Ƣ�°�
    public GameObject hit;//�ǿ�����Ʈ����

    [Header("�ǰ�(����Ŭ��)")]
    private AudioSource audioSource;
    public AudioClip Hit_Sound;
    public AudioClip Die_Sound;

    [Header("�÷��̾� �־��ּ���")]
    public Player_Attack player;

    [Header("������ ����")]
    public GameObject[] BodyDmg;



    public Player_Fog player_Fog;//���� �� ���̰� �ϱ�
    public GameObject Bleeding;
    

    public HP hp;
    private float Player_HP;
    public bool isDie = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponentInParent<Player_Attack>();
        hp = GetComponentInParent<HP>();
        Player_HP=hp.Start_HP(Player_HP);
        Debug.Log(Player_HP);
        player_Fog = GetComponentInParent<Player_Fog>();

    }

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ZombieAttack") && !isDie)
        {
           
            StartCoroutine(Hit_co(Hit_pos));
          
            Debug.Log(Player_HP);

            if (Player_HP <= 0 && !isDie)
            {
                
                player.GetComponent<Player_Move>().enabled = false;
                player.GetComponent<Player_Attack>().enabled = false;
                player.anim.SetLayerWeight(1, 0);//��ü �ִϸ��̼� �������
                player.anim.SetTrigger("isDie");
                Debug.Log("���ƾƾƾƾƾƾƝ�!");
                audioSource.PlayOneShot(Die_Sound);
                player_Fog.viewAngle = 360f;
                player_Fog.ViewRadius = 50f;
                isDie = true;
                //StartCoroutine(Die_Zombie_co());
               

            }
        }
       
    }

    //����� ��Ƣ�°�

    public void Zombie_Hit(Transform Hit_pos)
    {
        StartCoroutine(Hit_co(Hit_pos));
    }

/*    private IEnumerator Die_Zombie_co()
    {
        yield return new WaitForSeconds(3f);
       
        isDie = true;
    }*/

    public IEnumerator Hit_co(Transform Hit_pos)
    {
        Instantiate(hit, Hit_pos.transform.position, Hit_pos.transform.rotation);
        player.anim.SetTrigger("isHit");
        audioSource.PlayOneShot(Hit_Sound);
        Player_HP = hp.Damage(10f, Player_HP);
        bodyDmg();
        yield return new WaitForSeconds(1f);
    }

    //�������� ��ó�� ��
    private void bodyDmg()
    {
        int a = Random.Range(0, BodyDmg.Length);
        Bleeding.SetActive(true);
        BodyDmg[a].SetActive(true);
    }
}
