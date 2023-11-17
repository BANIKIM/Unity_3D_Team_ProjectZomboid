using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_isCar : MonoBehaviour
{
    public bool iscar = false;
    [SerializeField] private GameObject CarInfo;//��������â


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("CarDoor"))
        {
            iscar = true;
            Debug.Log("�������");
        }
        if (other.gameObject.CompareTag("CarHood"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("�ĵ�������");
                CarInfo.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CarDoor"))
        {
            iscar = false;
        }

        if (other.gameObject.CompareTag("CarHood"))
        {
            CarInfo.SetActive(false);
        }
    }
}
