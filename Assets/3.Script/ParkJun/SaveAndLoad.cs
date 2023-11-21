using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData
{
    public Vector3 playerPos;
    public Vector3 playerRot;

    public int savedHp;
    public int savedSp;
    public int savedDp;
    public int saveAtt;
    public int savedHungry;
    public int savedThirsty;



    //������ �ҷ����� 
    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
    public List<float> invenItemweight = new List<float>();

    //���� 
    /*
        1. ZombieSpawner�� ZombieList: ���� ���� ����Ǿ� ����
        2. Zombie
     */
}

public class SaveAndLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string save_data_directory; // ��� 
    private string save_filename = "/SaveFile.txt";

    private Player_Move thePlayer;
   
  

    private Inventory theInventory;
    private StatusController theStatus;

    private void Start()
    {
        save_data_directory = Application.dataPath + "/Saves/";
        if (!Directory.Exists(save_data_directory)) //���丮�� ������ �ϳ��� ������ 
        {
            Directory.CreateDirectory(save_data_directory);
        }
    }
    public void SaveData()
    {
        thePlayer = FindObjectOfType<Player_Move>();
        theInventory = FindObjectOfType<Inventory>();
        
        theStatus = FindObjectOfType<StatusController>();


        //�÷��̾� ��ġ 
        saveData.playerPos = thePlayer.transform.position; //��ġ ���� 
        saveData.playerRot = thePlayer.transform.eulerAngles; //ȸ���� ���� 



        //�÷��̾� ���� 
        saveData.savedHp = theStatus.GetcurrentHP();
        saveData.savedDp = theStatus.GetcurrentDP();
        saveData.savedSp = theStatus.GetcurrentSP();
        saveData.savedSp = theStatus.GetcurrentAtt();
        saveData.savedHungry = theStatus.GetcurrentHungry();
        saveData.savedThirsty = theStatus.GetcurrentThirsty();

        //�÷��̾� ������ (�κ��丮)
        Slot[] slots = theInventory.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item !=null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemweight.Add(slots[i].itemweight);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(save_data_directory + save_filename, json);

        Debug.Log("���� �Ϸ�");
        Debug.Log(json);
    }
    public void LoadData()
    {
        if (File.Exists(save_data_directory + save_filename)) //������ �������� �ε�
        {
            string loadJson = File.ReadAllText(save_data_directory + save_filename);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            thePlayer = FindObjectOfType<Player_Move>();
            theInventory = FindObjectOfType<Inventory>();
           
            theStatus = FindObjectOfType<StatusController>();

            //�÷��̾� ��ġ 
            thePlayer.transform.position = saveData.playerPos; //��ġ �ҷ����� 
            thePlayer.transform.eulerAngles = saveData.playerRot; //ȸ���� �ҷ����� 


            //�÷��̾� ���� 
            theStatus.SetcurrentHP(saveData.savedHp);
            theStatus.SetcurrentDP(saveData.savedDp);
            theStatus.SetcurrentSP(saveData.savedSp);
            theStatus.SetcurrentAtt(saveData.saveAtt);
            theStatus.SetcurrentHungry(saveData.savedHungry);
            theStatus.SetcurrentThirsty(saveData.savedThirsty);

            //�÷��̾� ������ (�κ��丮)
            for (int i = 0; i < saveData.invenItemName.Count; i++)
            {
                theInventory.LoadToDrop(saveData.invenArrayNumber[i], saveData.invenItemName[i],saveData.invenItemweight[i], saveData.invenItemNumber[i]);
            }

            Debug.Log("�ε� �Ϸ�");
            Debug.Log(loadJson);
        }
        else
            Debug.Log("���̺� ������ �����ϴ�.");
        
    }
}
