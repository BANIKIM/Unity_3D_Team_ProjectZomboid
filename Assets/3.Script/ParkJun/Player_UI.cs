using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_UI : MonoBehaviour
{
    public Text name;
    public Text attack;
    public Text health;
    
    // public Text maxhealth;
    public Text ZombieCount;

    private void Start()
    {
        DataManager.instance.PlayerLoadData();
        name.text += DataManager.instance.nowPlayer.name;

        //DataManager.instance.nowPlayer.health = DataManager.instance.nowPlayer.maxhealth;
        health.text = $"ü�� : {DataManager.instance.nowPlayer.health}/{DataManager.instance.nowPlayer.maxhealth}";
        attack.text = $"���ݷ� : {DataManager.instance.nowPlayer.attack}";
        ZombieCount.text = $"������ ���� �� :{DataManager.instance.nowPlayer.ZombieCount}";
    }
    public void UsingBeef()
    {
       
        health.text = $"ü�� : {DataManager.instance.nowPlayer.health}/{DataManager.instance.nowPlayer.maxhealth}";
        DataManager.instance.nowPlayer.health += 10;
        DataManager.instance.PlayerSaveData();

    }
    public void UsingBat()
    {
        DataManager.instance.nowPlayer.attack += 20;
        attack.text = $"���ݷ� : {DataManager.instance.nowPlayer.attack}";
        DataManager.instance.PlayerSaveData();
       
    }
    public void OffBat()
    {
        DataManager.instance.nowPlayer.attack =10;
        attack.text = $"���ݷ� : {DataManager.instance.nowPlayer.attack}";
        DataManager.instance.PlayerSaveData();
    }
    public void UsingSword()
    {
        DataManager.instance.nowPlayer.attack += 30;
        attack.text = $"���ݷ� : {DataManager.instance.nowPlayer.attack}";
        DataManager.instance.PlayerSaveData();
    }
    public void OffSword()
    {
        DataManager.instance.nowPlayer.attack = 10;
        attack.text = $"���ݷ� : {DataManager.instance.nowPlayer.attack}";
        DataManager.instance.PlayerSaveData();
    }
    public void Damage()
    {
        DataManager.instance.nowPlayer.health -= 10;
        health.text = $"ü�� : {DataManager.instance.nowPlayer.health}/{DataManager.instance.nowPlayer.maxhealth}";
        DataManager.instance.PlayerSaveData();
    }

}
