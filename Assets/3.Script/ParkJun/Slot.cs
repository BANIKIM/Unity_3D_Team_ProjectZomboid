using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour ,IPointerEnterHandler ,IPointerExitHandler,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    public Item item;
    public string itemName;
    public int itemCount;
    public float itemweight;
    public Image itemImage;

    [SerializeField]
    private Text text_Name;
    [SerializeField]
    private Text text_Count;

    [SerializeField]
    private GameObject go_NameImage;
    [SerializeField]
    private GameObject go_CountImage;

   

    [SerializeField]
    private Slider slider;

    private bool isUsingItem = false;

    private Rect baseRect;  // Inventory_Base �̹����� Rect ���� �޾� ��.

    [SerializeField]
    private ItemEffectDataBase theitemEffectDataBase;
    [SerializeField]
    private  Drop drop;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private ActionController thePlayer;

    private InputNumber theInputNumber;

    private void Start()
    {
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
       // theitemEffectDataBase = get<ItemEffectDataBase>();
       
       // drop = FindObjectOfType<Drop>();
       // inventory = FindObjectOfType<Inventory>();
       // theInputNumber = FindObjectOfType<InputNumber>();
      //  thePlayer = FindObjectOfType<ActionController>();
    }

    //�̹����� ���� ���� 
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    //������ ȹ��
    public void AddItem(Item _item,string _name,float _itemWeight, int _count=1 )
    {
        item = _item;
        itemCount = _count;
        itemName = _name;
        itemImage.sprite = item.itemImage;

        itemweight = _itemWeight;
        /*  weight = _item.itemweight;
          weight2 = weight;*/
        /*   totalWeight += _item.itemweight * _count;
          Debug.Log("���� ����: " + totalWeight);*/



        if (item.itemType !=Item.ItemType.Equipment)
        {
           
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
            go_NameImage.SetActive(true);
            text_Name.text = itemName;
           

        }
        else
        {

            text_Count.text = "0";
            go_CountImage.SetActive(false);
            go_NameImage.SetActive(true);
            text_Name.text = itemName;
            
        }

      

        SetColor(1);
    }
    //������ ���� ���� 
    public void SetSlotCount(int _count)
    {
      
        itemCount += _count;
        text_Count.text = itemCount.ToString();
       
        if (itemCount<=0)
        {
            ClearSlot();
        }
    }
    //���� �ʱ�ȭ
    public void ClearSlot()
    {
            itemweight -= item.itemweight * itemCount; // ���� ���� ����
            Debug.Log("���� ����: " + itemweight);
        
     

        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        itemName = null;
        SetColor(0);

        go_NameImage.SetActive(false);
        

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button==PointerEventData.InputButton.Right)
        {
            if (item !=null)
            {
                if (item.itemType==Item.ItemType.Equipment)
                {
                    //��� ����
                }
                else if (item.itemType == Item.ItemType.Used)
                {
                    StartCoroutine(UseItemWithSlider(item, 2f));
                }
                else if (item.itemType==Item.ItemType.objectUsed)
                {
                    StartCoroutine(UseObjectWithSlider(item, 8f));
                }
                else
                {
                    inventory.increaseBag(20);
                    SetSlotCount(-1);
                }
            }
        }
    }
    private IEnumerator UseItemWithSlider(Item _item, float duration)
    {
        float timer = 0f;
        slider.gameObject.SetActive(true); //�����̴� Ȱ��ȭ 


        while (timer < duration)
        {
            timer += Time.deltaTime; //�ð� �带���� Ÿ�̸� ����

            // �ð��� ���� �����̴� ���� 
            slider.value = timer / duration;

            yield return null; // ���� �����ӱ��� ��ٸ��� 
        }

        // �Ҹ� ��Ų �� 
        SetSlotCount(-1);

        // ������ ȿ�� ���� 
        theitemEffectDataBase.UseItem(_item);
        Debug.Log(_item.itemName + " �� ����߽��ϴ�.");

        // �����̴� ��Ȱ��ȭ
        slider.gameObject.SetActive(false);

        //currentWeight -= _item.itemweight;
        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();

    }
    private IEnumerator UseObjectWithSlider(Item _item,float duration)
    {
        float timer = 0f;
        slider.gameObject.SetActive(true); //�����̴� Ȱ��ȭ 


        while (timer < duration)
        {
            timer += Time.deltaTime; //�ð� �带���� Ÿ�̸� ����

            // �ð��� ���� �����̴� ���� 
            slider.value = timer / duration;

            yield return null; // ���� �����ӱ��� ��ٸ��� 
        }

        // �Ҹ� ��Ų �� 
        SetSlotCount(-1);

        // ������ ȿ�� ���� 
        theitemEffectDataBase.UseItem(_item);
        Debug.Log(_item.itemName + " �� ����߽��ϴ�.");

        // �����̴� ��Ȱ��ȭ
        slider.gameObject.SetActive(false);

        //currentWeight -= _item.itemweight;
        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item!=null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
           
          
           
        }
      
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
        
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {

      
        if (DragSlot.instance.transform.localPosition.x < baseRect.xMin
            || DragSlot.instance.transform.localPosition.x > baseRect.xMax
            || DragSlot.instance.transform.localPosition.y < baseRect.yMin
            || DragSlot.instance.transform.localPosition.y > baseRect.yMax)
        {


            // theInputNumber.Call();

          
            DragSlot.instance.dragSlot.ClearSlot();
             drop.UpdateTotalWeight();
            inventory.UpdateTotalWeight2();

        }
        else
        {
            //drop.UpdateTotalWeight();
            inventory.UpdateTotalWeight2();
            Debug.Log("OnEndDrag ȣ���");
            DragSlot.instance.SetColor(0);
            DragSlot.instance.dragSlot = null;
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        //�巡�� ���� ������ �����ϴ��� Ȯ��
           if (DragSlot.instance.dragSlot != null)
           {
            ItemDisObject();
            //���� ���԰� �巡�� ���� ���Կ� ��� �������� �����ϴ��� Ȯ��
            if (item != null && DragSlot.instance.dragSlot.item != null &&
                   item.itemName == DragSlot.instance.dragSlot.item.itemName) //���� ���԰� �巡�� ���� ������ ������ �̸��� ������ Ȯ���մϴ�.
               {
                   // ���� �������̸� ������ ��ģ��
                   SetSlotCount(DragSlot.instance.dragSlot.itemCount);
               
                    DragSlot.instance.dragSlot.ClearSlot(); // �巡���� ���� ����
               
               }
               else
                ChangeSlot();
                drop.UpdateTotalWeight();
                inventory.UpdateTotalWeight2();
                ItemDisObject();

           }
          else
            drop.UpdateTotalWeight();
            inventory.UpdateTotalWeight2();
            ItemDisObject();






        Debug.Log("OnDrop ȣ���");

    }
    private void ChangeSlot()
    {
       

        Item _tempItem = item;
        int _tempItemCount = itemCount;
        string _tempItemName = itemName;

       float _tempItemWeight = itemweight;

        //�־��ֱ� 
        AddItem(DragSlot.instance.dragSlot.item,DragSlot.instance.dragSlot.itemName, DragSlot.instance.dragSlot.itemweight,DragSlot.instance.dragSlot.itemCount);

        if (_tempItem !=null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemName, _tempItemWeight, _tempItemCount);

            itemweight -= _tempItemWeight * _tempItemCount;

            Debug.Log("���� ����: " + itemweight);

        }
        else
        {
            ItemDisObject();

            DragSlot.instance.dragSlot.ClearSlot();
            Debug.Log("ChangeSlot - Cleared Slot");

        }

    }
    private void ItemDisObject()
    {
        // OverlapSphere�� ����� �ݰ� ����
        float range = 1f;
        // �÷��̾� �ֺ� ������ �ݰ� ���� ��� �ݶ��̴� ��������
        Collider[] hitcoll = Physics.OverlapSphere(thePlayer.transform.position, range);

        foreach (Collider coll in hitcoll)
        {
            // �ݶ��̴��� ������ �±׸� ������ �ִ��� Ȯ��
            if (coll.CompareTag("Item"))
            {
                Item draggedItem = DragSlot.instance.dragSlot.item;

                // ���Կ� �Ű��� �����۰� �÷��̾� �ֺ� �������� ��
                if (draggedItem != null && draggedItem == coll.GetComponent<ItemPickup>().item)
                {
                    Destroy(coll.gameObject);

                    Debug.Log(draggedItem.itemName + " �������� �ı��Ǿ����ϴ�.");
                }
            }
        }
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(thePlayer.transform.position, 1.5f);
    }

    //���콺�� ���Կ� �� �� 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item!=null) //����ó�� 
        {
            theitemEffectDataBase.ShowToolTip(item,transform.position);
        }
        
    }
    //���콺�� ���Կ��� �������� ��
    public void OnPointerExit(PointerEventData eventData)
    {
        theitemEffectDataBase.HideToolTip();
    }

   
}
