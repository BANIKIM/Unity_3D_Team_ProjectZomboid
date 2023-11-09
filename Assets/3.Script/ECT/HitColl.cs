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

    [Header("�÷��̾� �־��ּ���")]
    public Player_Attack player;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponentInParent<Player_Attack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ZombieAttack"))
        {
            player.anim.SetTrigger("isHit");
            audioSource.PlayOneShot(Hit_Sound);
            StartCoroutine(Hit_co());
            Debug.Log("���� ������");
        }
    }

    public IEnumerator Hit_co()
    {
        Instantiate(hit, Hit_pos.transform.position, Hit_pos.transform.rotation);
        yield return new WaitForSeconds(0.3f);
    }
}
