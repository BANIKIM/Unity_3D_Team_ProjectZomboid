using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; // ���� ������ �ִ� �Ÿ�

    private bool pickupActivated = false; // ���� ������ �� true
    

    private Collider hitCollider; // �浹�� �ݶ��̴� ���� ����

    [SerializeField]
    private string itemTag = "Item"; // �����ۿ� �Ҵ�� �±�

   /* [SerializeField]
    private Text actionText;*/
    [SerializeField]
    private GameObject go_DropBase;
    [SerializeField]
    private Drop theDrop;
    [SerializeField]
    private Inventory theInventory;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    Player_Attack player_Attack;


 

    private void Update()
    {
       // CheckItem();
        //TryAction();
    }
  
    public void ToggleDropBase()
    {
        go_DropBase.SetActive(true);

        // ��Ӻ��̽��� ���� �� ������ ���� �ʱ�ȭ �����ϵ��� ����
        //opClear.SetCanClearSlots(go_DropBase.activeSelf);
    }

    private void TryAction()
    {
        if (Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư (1) �Է� Ȯ��
        {
            if (pickupActivated)
            {
                //PickupItem();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(itemTag))
        {
            pickupActivated = true;
            go_DropBase.gameObject.SetActive(true);
            theInventory.OpenInventory();

            ItemPickup itemPickup = other.GetComponent<ItemPickup>();
            if (itemPickup != null && !itemPickup.hasBeenPickedUp)
            {
                theDrop.AcquireItem(itemPickup.item, itemPickup.item.itemweight);
                //PickupItem();
                itemPickup.hasBeenPickedUp = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(itemTag))
        {
            ItemPickup itemPickup = other.GetComponent<ItemPickup>();
            itemPickup.hasBeenPickedUp = false;

            slider.gameObject.SetActive(false);
            player_Attack.anim.SetBool("isDrinking", false);
            infoDisAppear();

        }
    }
    



    private void infoDisAppear()
    {
        
        // actionText.gameObject.SetActive(false);
        pickupActivated = false;
       // theInventory.CloseInventory();
        go_DropBase.gameObject.SetActive(false);
        theInventory.CloseInventory();
        
        
        for (int i = 0; i < 20; i++)
        {
            theDrop.RemoveItem(i);
        }
      
    }
    private void infoAppear()
    {

        //actionText.gameObject.SetActive(true);
        go_DropBase.gameObject.SetActive(true);
        theInventory.OpenInventory();
    }
    public void OffDropBase()
    {
        go_DropBase.gameObject.SetActive(false);
    }
   
}

