using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Status_Btn : MonoBehaviour
{
    [Header("UI �־��ּ���")]
    public GameObject Player_Btn_On;


    [Header("UI �־��ּ���")]
    public GameObject Status;



    public void Btn_On()
    {
        Player_Btn_On.SetActive(true);
        Status.SetActive(true);
       
    }

    public void Btn_Off()
    {
        Player_Btn_On.SetActive(false);
        Status.SetActive(false);
        
    }
}
