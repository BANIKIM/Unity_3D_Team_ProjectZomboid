using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightClickMenu : MonoBehaviour, IPointerClickHandler, IClickState
{
    private GameObject rightClickMenu;
    private RectTransform rightClickRect;
    private float objectDistance;
    private float minDistance = 0;
    private RaycastHit minHit;

    [SerializeField] private Button buttonPrefab;
    private class RightClickButton
    {
        public Button button;
        public Text buttonText;
    }
    private int buttonCount = 0;
    private RightClickButton[] rightClickButtons;

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
            rightClickMenu.transform.position = new Vector2(pointerEventData.position.x + 150f, pointerEventData.position.y - 50f);
            Debug.Log(pointerEventData.position);
            rightClickMenu.SetActive(true);
            OnPointerObject();
        } else if (pointerEventData.button.Equals(PointerEventData.InputButton.Left))
        {
            rightClickMenu.SetActive(false);
            if (buttonCount > 0)
            {
                for (int i = 0; i < buttonCount; i++)
                {
                    Destroy(rightClickMenu.transform.GetChild(i));
                }
            }
        }
    }

    private void MenuSizeUpdate(int buttonCount)
    { // ��ư ������ŭ ���̰� �þ
        rightClickRect.sizeDelta = new Vector2(300, 100 * buttonCount); // STate�� ���� ��ư ī��Ʈ ��ȭ
    }

    private void OnPointerObject()
    { // Raycast Point�� ���� ������Ʈ ��� ��������
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ray�� ��ǥ�� Ŭ�� �� ��ǥ�� ��ġ��Ű�� �ɵ�...
        RaycastHit[] hit;

        hit = Physics.RaycastAll(ray);
        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            { // ���콺 Ŭ�� �� ��ǥ�� �±װ� �ִ� ������Ʈ ���� �� �Ÿ��� ���� ����� ������Ʈ�� ��ȣ�ۿ� �ϱ� ���� �� ����
                if (hit[i].transform.gameObject.CompareTag("Untagged")) continue; // ��ȣ�ۿ� �ȵǴ� ������Ʈ �߰� ����
                objectDistance = Vector2.Distance(Input.mousePosition, hit[i].transform.position);

                Debug.Log(hit[i].transform.position);
                if (minDistance == 0 || minDistance > objectDistance)
                {
                    // minDistance == 0�� ó��, ���� min�� object distance�� ��
                    minDistance = objectDistance;
                    minHit = hit[i];
                }
            }
        }

        Debug.Log($"{minHit.transform.tag} Hit: {minHit.transform.position}");
        // minHit�� ���� �޴� ���... todo
        // Player Tag�� ���� ��
        // Door Tag ����
        // Window �μ���, ����, ����ġ���, �Ѿ�� ��...
        // �޴� ����
        if (minHit.transform.gameObject.CompareTag("Player"))
        {
            DoorClick();
        }
    }

    #region Click State
    public void DoorClick()
    {
        string[] menu = { "menu1", "menu2", "menu3" }; // Text, ������Ʈ���� �޶���
        buttonCount = menu.Length;
        //  button ����ͼ� butto text �������ֱ�
        for (int i = 0; i < buttonCount; i++)
        {
            rightClickButtons[i].button = Instantiate(buttonPrefab);
            rightClickButtons[i].button.transform.SetParent(rightClickMenu.transform);
            rightClickButtons[i].buttonText = rightClickButtons[i].button.transform.GetChild(0).GetComponent<Text>();
            rightClickButtons[i].buttonText.text = menu[i];
        }
        MenuSizeUpdate(buttonCount);
    }
    #endregion
}