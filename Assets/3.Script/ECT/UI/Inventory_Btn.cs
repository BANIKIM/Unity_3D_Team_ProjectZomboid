using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Btn : MonoBehaviour
{
    [Header("UI �־��ּ���")]
    public GameObject Inventory_on;
    

    [Header("UI �־��ּ���")]
    public GameObject inventoryBtn;
    public GameObject DropBtn;



    public void Btn_On()
    {
        Inventory_on.SetActive(true);
        inventoryBtn.SetActive(true);
        DropBtn.SetActive(true);
    }

    public void Btn_Off()
    {
        Inventory_on.SetActive(false);
        inventoryBtn.SetActive(false);
        DropBtn.SetActive(false);
    }
}
