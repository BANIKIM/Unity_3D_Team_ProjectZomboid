using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; //���� ������ �ִ� �Ÿ� 

    private bool pickupActivated = false; //���� ������ �� true

    private RaycastHit hitinfo; //�浹ü ���� ����.

    //������ ���̾�� �����ϵ��� ���̾� ����ũ ���� 
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theinventory;

    private void Update()
    {
        CheckItem();
        TryAction();
    }
    private void TryAction()
    {
        if (Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư (1) �Է� Ȯ��
        {
            CheckItem();
            CanPickUp();
        }
    }
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitinfo.transform !=null)
            {
                Debug.Log(hitinfo.transform.GetComponent<ItemPickup>().item.itemName + "�׵�");
                theinventory.AcquireItem(hitinfo.transform.GetComponent<ItemPickup>().item);
                Destroy(hitinfo.transform.gameObject);
                infoDisAppear();
            }
        }
    }
    private void CheckItem()
    {
        Vector3 rayStartPos = transform.position + new Vector3(0, 0.1f, 0);
        if (Physics.Raycast(rayStartPos, transform.TransformDirection(Vector3.forward), out hitinfo, range, layerMask))
        {
            if (hitinfo.transform.tag=="Item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            infoDisAppear();
        }
    }
    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitinfo.transform.GetComponent<ItemPickup>().item.itemName + "�׵�";
    }
    private void infoDisAppear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        // ������ ���� �ð�ȭ
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range);
    }
}

