using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightClickMenu : MonoBehaviour, IPointerClickHandler, IClickState
{
    private GameObject rightClickMenu; // UI ������ ��ġ
    private RectTransform rightClickRect;

    [SerializeField] private GameObject buttonPrefab;
    private List<GameObject> rightClickButtons = new List<GameObject>();
    private GameObject buttonList;
    private Text buttonText;
    private int buttonCount = 0;
    private RaycastHit hitObject;

    private void Awake()
    {
        rightClickMenu = transform.GetChild(0).gameObject;
        rightClickRect = rightClickMenu.GetComponent<RectTransform>();
    }

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button.Equals(PointerEventData.InputButton.Right))
        {
            ListClear();
            rightClickMenu.transform.position = new Vector2(pointerEventData.position.x + 150f, pointerEventData.position.y - (buttonCount * 50f));
            rightClickMenu.SetActive(true);
            OnPointerObject();
        } else if (pointerEventData.button.Equals(PointerEventData.InputButton.Left))
        {
            ListClear();
        }
    }

    private void ListClear()
    {
        if (buttonCount > 0)
        {
            for (int i = 0; i < rightClickButtons.Count; i++)
            {
                Destroy(rightClickButtons[i].gameObject);
            }
            rightClickButtons.Clear();
            rightClickMenu.SetActive(false);
        }
    }

    private void OnPointerObject()
    { // Raycast Point�� ���� ������Ʈ ��� ��������
        RaycastHit[] hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics.RaycastAll(ray);

        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            { // ��Ŭ�� �� ù��°�� ������ ������Ʈ�� ��ȯ
                hitObject = hit[i];
                if (hit[i].collider.CompareTag("Window") || hit[i].collider.CompareTag("Door") || hit[i].collider.CompareTag("Fence"))
                {
                    break;
                }
            }
        }
        // Player Tag�� ���� ��
        // Door Tag ����
        // Window �μ���, ����, ����ġ���, �Ѿ�� ��...
        // �޴� ����
        switch (hitObject.collider.tag)
        {
            case "Window":
                WindowClick();
                break;
            case "Door":
                DoorClick();
                break;
            case "Fence":
                FenceClick();
                break;
            default: // player, sound, untagged ��
                // player ���� ��ȣ�ۿ�
                PlayerClick();
                break;
        }
    }

    private void ClickMenuLoad(string[] menu)
    {
        buttonCount = menu.Length;
        rightClickRect.sizeDelta = new Vector2(300, 100 * buttonCount); // State�� ���� ��ư ī��Ʈ ��ȭ, ��ư ������ŭ ���̰� �þ
        //  button �ش� ��ȣ�ۿ��ϴ� �޴���ŭ ���� button text �°� �������ֱ�
        for (int i = 0; i < buttonCount; i++)
        {
            buttonList = Instantiate(buttonPrefab, rightClickMenu.transform);
            rightClickButtons.Add(buttonList);
            buttonText = buttonList.transform.GetChild(0).gameObject.transform.GetComponent<Text>();
            buttonText.text = menu[i];
        }
    }

    #region Click State
    public void PlayerClick()
    {
        string[] menu = { "Player1", "Player2", "Player3" }; // Text, ������Ʈ���� �޶���
        ClickMenuLoad(menu);
    }

    public void DoorClick()
    {
        string[] menu = { "Door1", "Door2", "Door3", "Door4" }; // Text, ������Ʈ���� �޶���
        ClickMenuLoad(menu);
    }

    public void WindowClick()
    {
        string[] menu = { "Window1" };
        ClickMenuLoad(menu);
    }

    public void FenceClick()
    {
        string[] menu = { "Fence1" };
        ClickMenuLoad(menu);
    }
    #endregion
}