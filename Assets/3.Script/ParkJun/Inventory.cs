using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActiveated = false;

    //�ʿ��� ������Ʈ 
    [SerializeField]
    private GameObject go_inventotyBase; //�κ��丮 ���� 
    [SerializeField]
    private GameObject go_ToolTipBase;
    [SerializeField]
    private GameObject go_QuickSlotParent;  // ������ ����
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Player_Move player_move;

    public  float invenmaxweight = 20f;


    public Text text_inventoryweight;

    public GameObject Bag;

    private Slot[] slots;
    private Slot[] quickSlots; // �������� ���Ե�
    [SerializeField]
    private Drop drop;

    public Slot[] GetSlots() { return slots; }

    [SerializeField] private Item[] items;
    public void LoadToDrop(int _arrayNum, string _itemName,float _itemweight, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == _itemName)
            {
                slots[_arrayNum].AddItem(items[i], _itemName,_itemweight, _itemNum);
                UpdateTotalWeight2();
            }
        }
    }
    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        quickSlots = go_QuickSlotParent.GetComponentsInChildren<Slot>();
        //drop = FindObjectOfType<Drop>();
    }
    private void Update()
    {
        TryOpenInventory();
        TryDoubleClick();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            inventoryActiveated = !inventoryActiveated;
            if (inventoryActiveated)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }
    public void OpenInventory()
    {
       
        go_inventotyBase.SetActive(true);
    }
    public void CloseInventory()
    {

        go_inventotyBase.SetActive(false);
        go_ToolTipBase.SetActive(false); // ���� CloseInventory�� �߰��ϱ�
    }
    public void ToggleinventoryBase()
    {
        inventoryActiveated = !inventoryActiveated;
         go_inventotyBase.SetActive(inventoryActiveated);
        //go_inventotyBase.SetActive(!go_inventotyBase.activeSelf);
    }
    public void AcquireItem2(Item _item, float _weight, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        
                        return;
                    }
                }

            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _item.itemName, _weight, _count);
               
                return;
            }
        }
        UpdateTotalWeight2();
    }

    private void TryDoubleClick()
    {
        // ���콺 ���� ����Ŭ�� ����
        if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(0))
        {
            // ����Ŭ���� ������ ã�Ƽ� �ش� �������� �����Կ� �߰�
            TryDoubleClickToAddToQuickSlot();
        }
    }
    private void TryDoubleClickToAddToQuickSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].isFirstClick)
            {
                // ����Ŭ���� ������ �������� �����Կ� �߰�
                AddToQuickSlot(slots[i].item, slots[i].item.itemName, slots[i].itemweight, slots[i].itemCount);
                break; // ����Ŭ�� ó���� �������Ƿ� �ݺ��� ����
            }
        }
    }
    private void AddToQuickSlot(Item _item, string _name, float _itemWeight, int _count = 1)
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (quickSlots[i].item == null)
            {
                // ������ �迭���� ����ִ� ù ��° ���Կ� ������ �߰�
                quickSlots[i].AddItem(_item, _name, _itemWeight, _count);
                // �ش� ������ isFirstClick ���¸� ���� (����Ŭ�� ������ ����)
                quickSlots[i].isFirstClick = false;
                break; // ������ �߰��� �Ϸ�Ǿ����Ƿ� �ݺ��� ����
            }
        }
    }


    public void UpdateTotalWeight2()
    {
        float totalWeight2 = 0f;

        // ��� ������ Ȯ���ϸ� �������� ���Ը� �ջ�
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                totalWeight2 += slots[i].itemweight * slots[i].itemCount;
            }
        }

        // �ؽ�Ʈ ������Ʈ
        text_inventoryweight.text = $"{totalWeight2.ToString()}/{invenmaxweight}";

        if (totalWeight2 >= invenmaxweight)
        {
            //������ �÷��̾� ���� ������ �Ѵٴ��� 
            player_move.speed = Mathf.Max(1.5f, Mathf.Min(3f, player_move.speed - 1.5f));
            //���̴ٴ� ������ ���� 
        }
        else if (totalWeight2 <=invenmaxweight)
        {
            //���ų� �۾����ٸ� 
            //�ӵ� ����ȭ 
            player_move.speed = Mathf.Max(1.5f, Mathf.Min(3f, player_move.speed + 1.5f));
            //���̴ٴ� ������ ���� 
        }
    }
    public void OnBag(int _count)
    {

        invenmaxweight += _count;
        StartCoroutine(UseObjectWithSlider(3f));
        
    }
    public void OffBag(int _count)
    {
        invenmaxweight -= _count;
        // Bag.SetActive(false);
        UpdateTotalWeight2(); // ���� ���� ������Ʈ
    }
    private IEnumerator UseObjectWithSlider(float duration)
    {
        float timer = 0f;
        slider.gameObject.SetActive(true); //�����̴� Ȱ��ȭ 
       // Bag.SetActive(true);


        while (timer < duration)
        {
            timer += Time.deltaTime; //�ð� �带���� Ÿ�̸� ����

            // �ð��� ���� �����̴� ���� 
            slider.value = timer / duration;

            yield return null; // ���� �����ӱ��� ��ٸ��� 
        }

        // �����̴� ��Ȱ��ȭ
        slider.gameObject.SetActive(false);
        UpdateTotalWeight2(); // ���� ���� ������Ʈ
    }

}

