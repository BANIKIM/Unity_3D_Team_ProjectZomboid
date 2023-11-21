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
    public GameObject slotPrefab;



    public  float invenmaxweight = 20f;
    public float currentWeight = 0f;

    private bool isDoubleClick = false;
    private float doubleClickTime = 0.4f; // ����Ŭ�� ���� (��)

    public Text text_inventoryweight;

    public GameObject Bag;
    [SerializeField ]
    public Slot[] slots;
    private Slot[] quickSlots; // �������� ���Ե�
    [SerializeField]
    private Drop drop;

    public Slot[] GetSlots() { return slots; }
    public Slot[] GetQuickSlots() { return quickSlots; }

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
    public void LoadToQuick(int _arrayNum, string _itemName, float _itemweight, int _itemNum)
    {

    }
    private void Start()
    {
        UpdateSlotCount();
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        quickSlots = go_QuickSlotParent.GetComponentsInChildren<Slot>();
        
        //drop = FindObjectOfType<Drop>();
    }
    private void Update()
    {
       
        TryOpenInventory();
        TryDoubleClick();
    }
    private void UpdateSlotCount()
    {
        // invenmaxweight ���� ���� ������ ��ȯ
        int slotCount = Mathf.CeilToInt(invenmaxweight);
        
        // ���� ������ ���� ������ �������� ����
        for (int i = 0; i < slotCount; i++)
        {
            // ������ �����ϰ� �θ� ����
            GameObject slotObject = Instantiate(slotPrefab, go_SlotsParent.transform);
          
        }
    }


    private void TryDoubleClick()
    {
        if (IsMouseOverInventoryArea())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isDoubleClick)
                {
                    // ����Ŭ���� ������ ã�Ƽ� �ش� �������� �����Կ� �߰�
                    DoubleClickAddQuickSlot();
                    isDoubleClick = false; // ����Ŭ�� ���� �ʱ�ȭ
                }
                else
                {
                    isDoubleClick = true; // ù ��° Ŭ�� ����
                    StartCoroutine(DoubleClickTimer());
                }
            }
        }
      
    }
    private IEnumerator DoubleClickTimer()
    {
        yield return new WaitForSeconds(doubleClickTime);

        // ����Ŭ�� Ÿ�̸Ӱ� ������ ��
        isDoubleClick = false;
    }
    private bool IsMouseOverInventoryArea()
    {
        RectTransform inventoryRect = go_inventotyBase.GetComponent<RectTransform>();
        Vector2 mousePosition = Input.mousePosition;
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePosition);
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


    private void DoubleClickAddQuickSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].isFirstClick)
            {
                // ����Ŭ���� ������ �������� �����Կ� �߰�
               if( AddQuickSlot(slots[i].item, slots[i].item.itemName, slots[i].itemweight, slots[i].itemCount))
                { 
                    // �������� �� ���� �ʾ����� �ش� ������ Ŭ�����ϰ� ���� ������Ʈ
                    slots[i].ClearSlot();
                    UpdateTotalWeight2();
                }
               
                return; // ����Ŭ�� ó���� �������Ƿ� �ݺ��� ����
            }
            
        }
    }
    private bool AddQuickSlot(Item _item, string _name, float _itemWeight, int _count = 1)
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (quickSlots[i].item == null)
            {
                // ������ �迭���� ����ִ� ù ��° ���Կ� ������ �߰�
                quickSlots[i].AddItem(_item, _name, _itemWeight, _count);
               
                // �ش� ������ isFirstClick ���¸� ���� (����Ŭ�� ������ ����)
                quickSlots[i].isFirstClick = false;

                return true; // ������ �߰��� �Ϸ�Ǿ����Ƿ� true ��ȯ
            }
        }

        Debug.Log("�������� �� ���� �߰����� �ʾҽ��ϴ�.");
        return false; // �������� �� á���Ƿ� false ��ȯ
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
        currentWeight = totalWeight2;
        // �ؽ�Ʈ ������Ʈ
        text_inventoryweight.text = $"{currentWeight}/{invenmaxweight}";

        if (  currentWeight  > invenmaxweight)
        {
            //������ �÷��̾� ���� ������ �Ѵٴ��� 
            player_move.speed = Mathf.Max(1.5f, Mathf.Min(3f, player_move.speed - 1.5f));
            //���̴ٴ� ������ ���� 
        }
        else if (currentWeight <= invenmaxweight)
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
        UpdateSlotCount();
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

