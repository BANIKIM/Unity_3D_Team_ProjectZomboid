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

    private ItemEffectDataBase theitemEffectDataBase;
    private Drop theDrop;
   
    private void Start()
    {
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
        theitemEffectDataBase = FindObjectOfType<ItemEffectDataBase>();
    }

    //�̹����� ���� ���� 
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    //������ ȹ��
    public void AddItem(Item _item,string _name,int _count=1 )
    {
        item = _item;
        itemCount = _count;
        itemName = _name;
        itemImage.sprite = item.itemImage;

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
                else
                {
                    StartCoroutine(UseItemWithSlider(item, 2f));
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

            DragSlot.instance.dragSlot.ClearSlot();

            
        }




            Debug.Log("OnEndDrag ȣ���");
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
       
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot!=null)
        {
            ChangeSlot();
        }
        
       
        Debug.Log("OnDrop ȣ���");
      
    }
    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;
        string _tempItemName = itemName;

        //�־��ֱ� 
        AddItem(DragSlot.instance.dragSlot.item,DragSlot.instance.dragSlot.itemName, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem !=null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemName, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
           
        }

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
