using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightClickMenu : MonoBehaviour, IPointerClickHandler, IClickState
{
    private GameObject rightClickMenu; // UI ������ ��ġ
    private RectTransform rightClickRect;

    [SerializeField] private Button buttonPrefab;
    private List<Button> rightClickButtons = new List<Button>();
    private Button buttonList;
    private Text buttonText;
    private int buttonCount = 0;
    private RaycastHit hitObject;

    public bool isAim = false; // player ������ �ƴ� ��

    private void Awake()
    {
        rightClickMenu = transform.GetChild(0).gameObject;
        rightClickRect = rightClickMenu.GetComponent<RectTransform>();
    }

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button.Equals(PointerEventData.InputButton.Right) && !isAim)
        {
            ListClear();
            rightClickMenu.transform.position = new Vector2(pointerEventData.position.x + 150f, pointerEventData.position.y - (buttonCount * 50f));
            rightClickMenu.SetActive(true);
            OnPointerObject();
        }
        else if (pointerEventData.button.Equals(PointerEventData.InputButton.Left))
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
                if (hit[i].collider.CompareTag("Window")) // window hit �߰�... todo
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
        string[] menu = { "�޽��ϱ�" }; // Text, ������Ʈ���� �޶���
        ClickMenuLoad(menu);
    }

    public void WindowClick()
    {
        string[] menu = { "â�� ����", "â�� �ݱ�" };
        ClickMenuLoad(menu);

        WIndow_bool window = hitObject.collider.transform.GetComponent<WIndow_bool>();
        rightClickButtons[0].onClick.AddListener(window.WindowAnimation);
        rightClickButtons[1].onClick.AddListener(window.WindowAnimation);
    }
    #endregion
}