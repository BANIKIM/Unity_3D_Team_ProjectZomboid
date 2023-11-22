using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData
{
    public Vector3 playerPos;
    public Vector3 playerRot;

    public float savedHp;
    public int savedSp;
    public int savedDp;
    public int saveAtt;
    public int savedHungry;
    public int savedThirsty;



    // ������ ����ȭ�� �Ұ���. ����ȭ�� �Ұ����� �ֵ��� �ִ�.

    //������ �ҷ����� 
    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
    public List<float> invenItemweight = new List<float>();
    //������ 
   
    public List<int> quickSlotArrayNumber = new List<int>();
    public List<string> quickSlotItemName = new List<string>();
    public List<int> quickSlotItemNumber = new List<int>();
    public List<float> quickSlotItemweigh = new List<float>();

    //���� 
   
}


public class SaveAndLoad : MonoBehaviour
{
    public  SaveData saveData = new SaveData();
    

    private string save_data_directory; // ��� 
    private string save_filename = "/SaveFile.txt";

    private Player_Move thePlayer;

    private ZombieSpawner theZombie;

    private Inventory theInventory;
    private StatusController theStatus;

    // Load �� �� SetUp ����ó��
    public bool isLoad = true; // ����� ������ ���� �� true

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
        theZombie = FindObjectOfType<ZombieSpawner>();


        //�÷��̾� ��ġ 
        saveData.playerPos = thePlayer.transform.position; //��ġ ���� 
        saveData.playerRot = thePlayer.transform.eulerAngles; //ȸ���� ���� 



        // ����
     


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

        Slot[] quickSlots = theInventory.GetQuickSlots();
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (quickSlots[i].item != null)
            {
                saveData.quickSlotArrayNumber.Add(i);
                saveData.quickSlotItemName.Add(quickSlots[i].item.itemName);
                saveData.quickSlotItemweigh.Add(slots[i].itemweight);
                saveData.quickSlotItemNumber.Add(quickSlots[i].itemCount);
            }
        }
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(save_data_directory + save_filename, json);
    }
    public void LoadData()
    {
        if (File.Exists(save_data_directory + save_filename)) //������ �������� �ε�
        {
            isLoad = true;
            string loadJson = File.ReadAllText(save_data_directory + save_filename);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            thePlayer = FindObjectOfType<Player_Move>();
            theInventory = FindObjectOfType<Inventory>();
            theStatus = FindObjectOfType<StatusController>();
            theZombie = FindObjectOfType<ZombieSpawner>();

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
                theInventory.LoadToDrop(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemweight[i], saveData.invenItemNumber[i]);
            }
            // ������ �ε�
            for (int i = 0; i < saveData.quickSlotItemName.Count; i++)
            {
                theInventory.LoadToQuick(saveData.quickSlotArrayNumber[i], saveData.quickSlotItemName[i], saveData.quickSlotItemweigh[i], saveData.quickSlotItemNumber[i]);

            }
        }
    }
 



}
