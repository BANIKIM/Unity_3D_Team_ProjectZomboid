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

    [SerializeField]
    private Text actionText;
    [SerializeField]
    private GameObject go_DropBase;
    [SerializeField]
    private Inventory theinventory;

    private void Update()
    {
        CheckItem();
        TryAction();
    }
    public void ToggleDropBase()
    {
        go_DropBase.SetActive(!go_DropBase.activeSelf);
    }

    private void TryAction()
    {
        if (Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư (1) �Է� Ȯ��
        {
            if (pickupActivated)
            {
                PickupItem();
            }
        }
    }

    private void PickupItem()
    {
        if (hitCollider != null)
        {
            ItemPickup itemPickup = hitCollider.GetComponent<ItemPickup>();
            if (itemPickup != null)
            {
                Debug.Log(itemPickup.item.itemName + "ȹ��");
                theinventory.AcquireItem(itemPickup.item);
                Destroy(hitCollider.gameObject);
                infoDisAppear();
            }
        }
    }

    private void CheckItem()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag(itemTag))
            {
                pickupActivated = true;
                actionText.gameObject.SetActive(true);
                go_DropBase.gameObject.SetActive(true);
                hitCollider = col;
                actionText.text = "ȹ��" + "<color=yellow>" + "(���콺 ������ ��ư)" + "</color>";
                return; // ù ��° �����۸� ó���ϰ� �������� ����
            }
        }

        infoDisAppear();
    }
   

    private void infoDisAppear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
       
    }
   
}
