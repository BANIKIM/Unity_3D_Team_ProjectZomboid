using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNumber : MonoBehaviour
{
    private bool activated = false;

    [SerializeField]
    private Text text_Preview;
    [SerializeField]
    private Text text_Input;
    [SerializeField]
    private InputField if_text;

    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private ActionController  thePlayer;
    [SerializeField]
    private Drop drop;
    [SerializeField]
    private Inventory inventory;

    void Update()
    {    
        if (activated)
        {
            OK();
        }
    }

    public void Call()
    {
        go_Base.SetActive(false);
        activated = true;
        if_text.text = "";
        text_Preview.text = DragSlot.instance.dragSlot.itemCount.ToString();
     
    }

    public void Cancel()
    {
        activated = false;
        DragSlot.instance.SetColor(0);
        go_Base.SetActive(false);
        DragSlot.instance.dragSlot = null;
    }

    public void OK()
    {
        DragSlot.instance.SetColor(0);

        int num;
        if (text_Input.text != "")
        {
            if (CheckNumber(text_Input.text))
            {
                num = int.Parse(text_Input.text);
                if (num > DragSlot.instance.dragSlot.itemCount)
                    num = DragSlot.instance.dragSlot.itemCount;
            }
            else
                num = 1;
        }
        else
            num = int.Parse(text_Preview.text);

        StartCoroutine(DropItemCorountine(num));
    }

    IEnumerator DropItemCorountine(int _num)
    {
        for (int i = 0; i < _num; i++)
        {
            Instantiate(DragSlot.instance.dragSlot.item.itemPrefab,
                thePlayer.transform.position + thePlayer.transform.forward*1.5f,
                Quaternion.identity);
            DragSlot.instance.dragSlot.SetSlotCount(-1);
            yield return new WaitForSeconds(0.05f);
        }
        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();
        DragSlot.instance.dragSlot = null;
        go_Base.SetActive(false);
        activated = false;
    }

    private bool CheckNumber(string _argString)
    {
        char[] _tempCharArray = _argString.ToCharArray();
        bool isNumber = true;

        for (int i = 0; i < _tempCharArray.Length; i++)
        {
            if (_tempCharArray[i] >= 48 && _tempCharArray[i] <= 57)
                continue;
            isNumber = false;
        }
        return isNumber;
    }
}