using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightClickMenu : MonoBehaviour, IPointerClickHandler, IClickState
{
    private GameObject rightClickMenu; // UI ������ ��ġ
    private RectTransform rightClickRect;
    private float objectDistance;
    private float minDistance = 0;
    private RaycastHit minHit;

    [SerializeField] private GameObject buttonPrefab;
    private class RightClickButton
    {
        public GameObject button;
        public Text buttonText;
    }
    private int buttonCount = 0;
    private RightClickButton[] rightClickButtons;
    private Transform objectPos;
    private GameObject hitObject;
    private string[] objectList = { "Window", "Door", "Fence" }; // ��ȣ �ۿ��ϴ� ������Ʈ ����

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
            OnPointerObject();
            rightClickMenu.transform.position = new Vector2(pointerEventData.position.x + 150f, pointerEventData.position.y - 50f);
            rightClickMenu.SetActive(true);
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
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        RaycastHit[] hit;

        /*
            ��ȣ �ۿ��ϴ� �͸� �ν�
            CompareTag() �� �� �ִٸ� �ٷ� ��ȣ�ۿ��� hitObject = hit[i].collider... return
            switch (hitObject) {
                case "":
                    break;
                case "":
                    break;
            }
        */

        hit = Physics.RaycastAll(ray);
        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            { // ���콺 Ŭ�� �� ��ǥ�� �±װ� �ִ� ������Ʈ ���� �� �Ÿ��� ���� ����� ������Ʈ�� ��ȣ�ۿ� �ϱ� ���� �� ����
                // CompareTag()
                if (hit.Length > 1)
                {
                    objectPos = hit[i].collider.gameObject.transform.parent;
                } else
                {
                    objectPos = hit[i].collider.gameObject.transform;
                }
                
                objectDistance = Vector2.Distance(Input.mousePosition, objectPos.position);
                Debug.Log($"{hit[i].collider.gameObject.name}'s Hit Position: {hit[i].collider.gameObject.transform.position}");
                if (minDistance == 0 || minDistance > objectDistance)
                {
                    // minDistance == 0�� ó��, ���� min�� object distance�� ��
                    minDistance = objectDistance;
                    minHit = hit[i];
                }
            }
        }

        Debug.Log($"Min Position: {minHit.collider.tag}");
        // minHit�� ���� �޴� ���... todo
        // Player Tag�� ���� ��
        // Door Tag ����
        // Window �μ���, ����, ����ġ���, �Ѿ�� ��...
        // �޴� ����
        if (minHit.transform.gameObject.CompareTag("Player"))
        {
            PlayerClick();
        } else if (minHit.transform.gameObject.CompareTag("Door"))
        {
            DoorClick();
        }
    }

    private void ClickMenuLoad(string[] menu)
    {
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
    #endregion
}