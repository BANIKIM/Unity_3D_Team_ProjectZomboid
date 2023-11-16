using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drop : MonoBehaviour
{
    public static bool dropActiveated = false;
    private float range = 0.5f;

    //�ʿ��� ������Ʈ 
    [SerializeField]
    private GameObject go_dropBase;
    [SerializeField]
    private GameObject go_Base;
    [SerializeField]
    private GameObject go_SlotsParent;

    [SerializeField]
    private Text text_weight;

    public  float dropmaxweight = 20f;





    private Slot[] slots;
    


    [SerializeField] private Item[] items;

    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
      
    }
    void Update()
    {
        TryOpenInventory();
    }
   private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I)) // ���콺 ���� ��ư (1) �Է� Ȯ��
        {
            dropActiveated = !dropActiveated;
            if (dropActiveated)
            {
                OpenDrop();
            }
            else
            {
                CloseDrop();
            }
        }
    }
    public void OpenDrop()
    {
        go_dropBase.SetActive(true);
    }
    public void CloseDrop()
    {

        go_dropBase.SetActive(false);
       // go_Base.SetActive(false); // ���� CloseInventory�� �߰��ϱ�
     
    }
    public void ToggleinventoryBase()
    {
        dropActiveated = !dropActiveated;
        go_dropBase.SetActive(dropActiveated);
    }

   
    public void AcquireItem(Item _item,float _weight,int _count = 1)
    {
        if (Item.ItemType.Equipment!=_item.itemType)
        {
           
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].item != null)
                    {
                        if (slots[i].item.itemName == _item.itemName)
                        {
                            slots[i].SetSlotCount(_count);
                            UpdateTotalWeight();


                            return;
                        }
                    }

                }
           
        }  
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item ==null)
            {
                slots[i].AddItem(_item, _item.itemName,_weight,_count);
                UpdateTotalWeight();
                return;
            }
           
        }

    }

    public void RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length && slots[slotIndex].item != null)
        {
            float removedWeight = slots[slotIndex].itemweight * slots[slotIndex].itemCount;
            slots[slotIndex].ClearSlot();  // ������ ���ϴ�.
            UpdateTotalWeight();  // ���� ������Ʈ (���� ������ �����Ͽ� ���Ҹ� ��Ÿ��)
        }
    }

    public void UpdateTotalWeight()
    {
        float totalWeight = 0;


        // ��� ������ Ȯ���ϸ� �������� ���Ը� �ջ�
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                totalWeight += slots[i].itemweight * slots[i].itemCount;
            }
        }

        // �ؽ�Ʈ ������Ʈ ���� �߰� �۾� ����
        text_weight.text = $"{totalWeight.ToString()}/{dropmaxweight}";

        if (totalWeight >= dropmaxweight)
        {
            Debug.Log("�Ѿ���?");
            // TODO: ó���� ���� �߰�
        }
    }
  

}
