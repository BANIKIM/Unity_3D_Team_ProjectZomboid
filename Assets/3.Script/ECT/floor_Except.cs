using UnityEngine;

public class SecondFloor_Except : MonoBehaviour
{
    public string targetTag = "Except";
    public string floorDelet = "2floor";
    public float deletionRadius = 20f; 
    public Camera mainCamera; 


    private void Update()
    {
        // ī�޶�� �÷��̾��� �߰� ����
        Vector3 midPoint = (mainCamera.transform.position + transform.position) / 2f;

        // Ư�� �±׸� ���� ������Ʈ
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);

        // ã�� ������Ʈ���� ����
        foreach (GameObject obj in objectsWithTag)
        {
            // ������Ʈ�� �߰� ���� ���� �Ÿ��� ���
            float distance = Vector3.Distance(midPoint, obj.transform.position);

            // ���� �ݰ� ���� �ִ� ������Ʈ�� ����
            if (distance <= deletionRadius)
            {
                Destroy(obj);
            }
        }
    }
}
