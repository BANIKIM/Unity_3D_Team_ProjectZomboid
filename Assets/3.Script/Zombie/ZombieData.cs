using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ZombieData", fileName = "ZombieData")]
public class ZombieData : ScriptableObject
{
    /*
        ü��, �޽� ������, ��ũ�� ���� ����
     */
    // public float zombieHp = 5f;
    public Mesh skinnedMesh;
    public bool isScreamZombie = false;
}
