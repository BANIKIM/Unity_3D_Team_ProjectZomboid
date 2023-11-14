using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_isCar : MonoBehaviour
{
    public bool iscar = false;
    [SerializeField] private GameObject CarInfo;
/*    private void OnCollisionStay(Collision collision)
    {

        if(collision.gameObject.CompareTag("Car"))
        {
                       
            Debug.Log("iscar");
        }
   
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("CarDoor"))
        {
            iscar = false;
            Debug.Log("iscar");
        }
    }*/

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("CarDoor"))
        {
            iscar = true;
            Debug.Log("도어어어어");
        }
        if (other.gameObject.CompareTag("CarHood"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
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
