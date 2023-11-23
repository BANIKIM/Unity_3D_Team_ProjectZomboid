using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    public string itemName;
    public int itemCount;
    public float itemweight;
    public Image itemImage;

    public bool isFirstClick = true;

    [SerializeField]
    private Text text_Name;
    [SerializeField]
    private Text text_Count;

    [SerializeField]
    private GameObject go_NameImage;
    [SerializeField]
    private GameObject go_CountImage;

    public GameObject Bat_B;
    public GameObject Gun_B;
    public Player_Attack player_Attack;


    [SerializeField]
    private Slider slider;

    [SerializeField] private RectTransform baseRect;  // Inventory_Base �� ����
    [SerializeField] private RectTransform dropRect;
    // private Rect baseRect;  // Inventory_Base �̹����� Rect ���� �޾� ��.
    [SerializeField] RectTransform quickSlotBaseRect;


    private ItemEffectDataBase theitemEffectDataBase;

    private Drop drop;

    private Inventory inventory;

    private ActionController thePlayer;

    private InputNumber theInputNumber;
    private Player_Move playerMove;
    private void Start()
    {
        /* Transform parentTransform = transform.parent.parent.parent;
         baseRect = parentTransform.GetComponent<RectTransform>().rect;*/


        theitemEffectDataBase = FindObjectOfType<ItemEffectDataBase>();
        drop = FindObjectOfType<Drop>();
        inventory = FindObjectOfType<Inventory>();
        theInputNumber = FindObjectOfType<InputNumber>();
        thePlayer = FindObjectOfType<ActionController>();
        playerMove = FindObjectOfType<Player_Move>();
    }

    //�̹����� ���� ���� 
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    //������ ȹ��
    public void AddItem(Item _item, string _name, float _itemWeight, int _count = 1)
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



        if (item.itemType != Item.ItemType.Equipment)
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

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }
    //���� �ʱ�ȭ
    public void ClearSlot()
    {
        // itemweight -= item.itemweight * itemCount; // ���� ���� ����


        item = null;
        itemImage.sprite = null;
        itemName = null;
        SetColor(0);

        go_NameImage.SetActive(false);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    //��� ����
                    if (item.itemName == "Bat")
                    {
                        Bat_B.SetActive(true);
                        Gun_B.SetActive(false);
                        player_Attack.Bat_Get = true;
                        player_Attack.Gun_Get = false;
                    }
                    else if (item.itemName == "Gun")
                    {
                        Gun_B.SetActive(true);
                        Bat_B.SetActive(false);
                        player_Attack.Gun_Get = true;
                        player_Attack.Bat_Get = false;
                    }

                }
                else if (item.itemType == Item.ItemType.Used)
                {
                    RectTransform slotRectTransform = GetComponent<RectTransform>();
                    if (!IsInsideBaseRect(slotRectTransform))
                    {
                        StartCoroutine(UseItemWithSlider(item, 2f));
                        isFirstClick = false;
                    }
                    else
                    {
                        isFirstClick = true;
                    }

                }
                else if (item.itemType == Item.ItemType.objectUsed)
                {
                    StartCoroutine(UseObjectWithSlider(item, 8f));


                }
                else if (item.itemType == Item.ItemType.Ingredient)
                {
                    // ù ��° Ŭ�� ��
                    if (isFirstClick)
                    {
                        inventory.OnBag(20);
                        isFirstClick = false;
                    }
                    // �� ��° Ŭ�� ��
                    else
                    {
                        inventory.OffBag(20);
                        isFirstClick = true;
                    }
                }
                else
                {

                }
            }
        }
    }
    private bool IsInsideBaseRect(RectTransform slotRectTransform)
    {
        // ���� Rect ���ο� �ִ��� ���� Ȯ��
        return RectTransformUtility.RectangleContainsScreenPoint(dropRect, slotRectTransform.position);
    }
    private IEnumerator UseItemWithSlider(Item _item, float duration)
    {
        float timer = 0f;
        slider.gameObject.SetActive(true); //�����̴� Ȱ��ȭ 
        player_Attack.anim.SetBool("isDrinking", true);
        SetSlotCount(-1);

        while (timer < duration)
        {
            timer += Time.deltaTime; //�ð� �带���� Ÿ�̸� ����

            // �ð��� ���� �����̴� ���� 
            slider.value = timer / duration;

            yield return null; // ���� �����ӱ��� ��ٸ��� 
        }




        // ������ ȿ�� ���� 
        theitemEffectDataBase.UseItem(_item);


        // �����̴� ��Ȱ��ȭ
        slider.gameObject.SetActive(false);
        player_Attack.anim.SetBool("isDrinking", false);
        //currentWeight -= _item.itemweight;
        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();

    }
    private IEnumerator UseObjectWithSlider(Item _item, float duration)
    {
        float timer = 0f;
        slider.gameObject.SetActive(true); //�����̴� Ȱ��ȭ 
        playerMove.speed = Mathf.Max(0.1f, Mathf.Min(2f, playerMove.speed - 1.5f));

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


        // �����̴� ��Ȱ��ȭ
        slider.gameObject.SetActive(false);
        playerMove.speed = Mathf.Max(0.1f, Mathf.Min(2f, playerMove.speed + 1.5f));
        //currentWeight -= _item.itemweight;
        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
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


        if (!((DragSlot.instance.transform.localPosition.x > baseRect.rect.xMin
           && DragSlot.instance.transform.localPosition.x < baseRect.rect.xMax
           && DragSlot.instance.transform.localPosition.y > baseRect.rect.yMin
           && DragSlot.instance.transform.localPosition.y < baseRect.rect.yMax)
           ||
           (DragSlot.instance.transform.localPosition.x > quickSlotBaseRect.rect.xMin
           && DragSlot.instance.transform.localPosition.x < quickSlotBaseRect.rect.xMax
           && DragSlot.instance.transform.localPosition.y + baseRect.transform.localPosition.y > quickSlotBaseRect.rect.yMin + quickSlotBaseRect.transform.localPosition.y
           && DragSlot.instance.transform.localPosition.y + baseRect.transform.localPosition.y < quickSlotBaseRect.rect.yMax + quickSlotBaseRect.transform.localPosition.y)))
        {

            if (DragSlot.instance.dragSlot!=null)
            {
                for (int i = 0; i < DragSlot.instance.dragSlot.itemCount; i++)
                {
                    Instantiate(DragSlot.instance.dragSlot.item.itemPrefab,
                                thePlayer.transform.position + thePlayer.transform.forward * 1.5f,
                                Quaternion.identity);
                   

                }
                DragSlot.instance.dragSlot.ClearSlot();
            }

            //������ ���� �� ����  Todo...      Exit_item
          

        }
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();





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

            //ItemDisObject();

        }
        else

            ItemDisObject();




        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();



    }
    private void ChangeSlot()
    {


        Item _tempItem = item;
        int _tempItemCount = itemCount;
        string _tempItemName = itemName;

        float _tempItemWeight = itemweight;

        //�־��ֱ� 
        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemName, DragSlot.instance.dragSlot.itemweight, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemName, _tempItemWeight, _tempItemCount);

            itemweight -= _tempItemWeight * _tempItemCount;



        }
        else
        {
            ItemDisObject();

            DragSlot.instance.dragSlot.ClearSlot();


        }

    }
    private void ItemDisObject()
    {
        // OverlapBox�� ����� �ڽ� ���� ����
        Vector3 halfExtents = new Vector3(0.8f, 4f, 0.8f);
        // �÷��̾� �ֺ� ������ �ڽ� ���� ���� ��� �ݶ��̴� ��������
        Collider[] hitColliders = Physics.OverlapBox(thePlayer.transform.position, halfExtents);

        foreach (Collider coll in hitColliders)
        {
            // �ݶ��̴��� ������ �±׸� ������ �ִ��� Ȯ��
            if (coll.CompareTag("Item"))
            {
                Item draggedItem = DragSlot.instance.dragSlot.item;

                // ���Կ� �Ű��� �����۰� �÷��̾� �ֺ� �������� ��
                if (draggedItem != null && draggedItem == coll.GetComponent<ItemPickup>().item)
                {
                    int itemCountToDestroy = DragSlot.instance.dragSlot.itemCount; // �巡�� �� �������� ���� ��������

                    // �巡�� �� ������ŭ ������ �ı�
                    for (int i = 0; i < itemCountToDestroy; i++)
                    {
                        Destroy(coll.gameObject);
                    }

                }
            }
        }
    }
    /*    private void OnDrawGizmos()
        {
            // OverlapBox�� ����� �ڽ� ���� ����
            Vector3 halfExtents = new Vector3(0.5f, 4f, 1f);

            // ������� ���� ����
            Gizmos.color = Color.yellow;

            // ������ �ڽ��� ������ �׸���
            Gizmos.DrawWireCube(thePlayer.transform.position,  halfExtents);
        }
    */




    //���콺�� ���Կ� �� �� 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null) //����ó�� 
        {
            theitemEffectDataBase.ShowToolTip(item, transform.position);
        }

    }
    //���콺�� ���Կ��� �������� ��
    public void OnPointerExit(PointerEventData eventData)
    {
        theitemEffectDataBase.HideToolTip();
    }


}
