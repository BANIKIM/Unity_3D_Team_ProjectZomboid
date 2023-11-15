using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Die : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Zombie;
    [SerializeField] private GameObject ppp;
    public GameObject Zombie_icon;
    public GameObject Carmer;
    public HitColl hitColl;
    private bool isDie=false;
    private bool Die = false;
    //private bool NonTarget = false;


    void Update()
    {
        
        if (hitColl.isDie && !Die)//Die�� ����ó�� 
        {
            Zombie.transform.position = Player.transform.position;//���� ������Ű�� �ɼ��̶� ����ó���� �ʿ���
            StartCoroutine(Die_Zombie_co());
            Die = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        
        if(other.gameObject.CompareTag("Zombie")&& isDie)
        {
            Debug.Log("����");
            other.gameObject.TryGetComponent(out ZombieController zombie);
            zombie.nonTarget = true;
        }
    }

    private IEnumerator Die_Zombie_co()
    {
        isDie = true;
        yield return new WaitForSeconds(5f);
        Player.SetActive(false);
        Carmer.GetComponent<Camera_Controller>().enabled = false;
        Zombie.SetActive(true);
        Carmer.GetComponent<Camera_Controller_Zomdie>().enabled = true;
        Zombie_icon.SetActive(true);


    }

}