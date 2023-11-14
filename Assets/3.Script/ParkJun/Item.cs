using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public ItemType itemType; // ������ ����
    public string itemName; //�̸� 
    [TextArea]
    public string itemDesc; //������ ���� 
    public Sprite itemImage; //�̹���
    public GameObject itemPrefab; //������ ������
    public string weight; // �������� ����
    public float itemweight;

    public string weaponType; 

    public enum ItemType
    {
        Equipment,
        Used,
        objectUsed,
        Ingredient,
        ETC
    }
}
