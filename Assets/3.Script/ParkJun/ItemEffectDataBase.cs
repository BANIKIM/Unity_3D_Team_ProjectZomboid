using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemEffect
{
    public string itemName; // �������� �̸� (Ű��) 
    [Tooltip("HP,SP,DP,HUNGRY,THIRSTY�� �����մϴ�")]
    public string[] part; //���� ȿ�� 
    public int[] num; //��ġ 

}

public class ItemEffectDataBase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;

    //�ʿ��� ������Ʈ 
    [SerializeField]
    private StatusController thePlayerStatus;
    [SerializeField]
    private Inventory theinven;
    [SerializeField]
    private SlotToolTip theSlotToolTip;

    private const string HP = "HP", SP = "SP", DP = "DP", ATT = "ATT", Hungry = "HUNGRY", THIRSTY = "THIRSTY", BAG = "BAG";

    public void ShowToolTip(Item _item,Vector3 _pos)
    {
        theSlotToolTip.ShowToolTip(_item,_pos);
    }
    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }

    public void UseItem(Item _item)
    {
        if (_item.itemType==Item.ItemType .Used)
        {
            for (int x = 0; x < itemEffects.Length; x++)
            {
                if (itemEffects[x].itemName==_item.itemName)
                {
                    for (int y = 0; y < itemEffects[x].part.Length; y++)
                    {
                        switch (itemEffects[x].part[y])
                        {
                            case HP:
                                thePlayerStatus.increaseHP(itemEffects[x].num[y]);
                                break;
                            case SP:
                                thePlayerStatus.increaseSP(itemEffects[x].num[y]);
                                break;
                            case DP:
                                thePlayerStatus.increaseDP(itemEffects[x].num[y]);
                                break;
                            case Hungry:
                                thePlayerStatus.increaseHungry(itemEffects[x].num[y]);
                                break;
                            case THIRSTY:
                                thePlayerStatus.increaseThirsty(itemEffects[x].num[y]);
                                break;
                        }
                    }
                    return; //for���� �����Ҷ����� �����Ѱ� ������ ����������
                }
            }
        }
        if (_item.itemType == Item.ItemType.objectUsed)
        {
            for (int x = 0; x < itemEffects.Length; x++)
            {
                if (itemEffects[x].itemName == _item.itemName)
                {
                   
                    for (int y = 0; y < itemEffects[x].part.Length; y++)
                    {
                        switch (itemEffects[x].part[y])
                        {
                            case ATT:
                                thePlayerStatus.increaseATT(itemEffects[x].num[y]);
                                break;
                            default:
                                break;
                        }
                    }
                    return; //for���� �����Ҷ����� �����Ѱ� ������ ����������
                }
            }
        }
    }
}
