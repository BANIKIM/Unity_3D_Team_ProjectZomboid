using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_Transparent : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform
    public float offset;  // �÷��̾�� ī�޶��� �Ÿ�
    public float zoomSpeed;  // �� �ӵ�
    public float minZoom;  // �ּ� ��
    public float maxZoom;  // �ִ� ��

    private float xOffset = 2f;
    private float zOffset = 3f;

    private void Start()
    {
        Camera_Early();
    }

    private void LateUpdate()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("EnvironmentObject"));

        for (int i = 0; i < hits.Length; i++)
        {
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();

            for(int j=0; j< obj.Length; j++)
            {
                obj[j]?.becomeTransParent();
            }
            
        }


        float scrollData = Input.GetAxis("Mouse ScrollWheel");  // ��ũ�� ������

        float nextOffset = offset - scrollData * (zoomSpeed*10);  // ���� �����ӿ����� offset ���
        nextOffset = Mathf.Clamp(nextOffset, minZoom, maxZoom);  // �� ����

        // offset�� minZoom�� maxZoom ������ ���� xOffset, zOffset, offset ������Ʈ
        if (nextOffset > minZoom && nextOffset < maxZoom)
        {
            if (scrollData > 0) // ��ũ���� �÷��� ��
            {
                xOffset -= zoomSpeed; // X�� Z�࿡ ���� �������� ����.
                zOffset -= zoomSpeed;
                offset = nextOffset;
            }
            else if (scrollData < 0) // ��ũ���� ������ ��
            {
                xOffset += zoomSpeed;
                zOffset += zoomSpeed; // X�� Z�࿡ ���� �������� �ø�.
                offset = nextOffset;
            }
        }

        transform.position = player.position + new Vector3(-xOffset, offset, -zOffset);  // ī�޶� ��ġ ������Ʈ
    }

    private void Camera_Early()
    {
        transform.position = player.position + new Vector3(-xOffset, offset, -zOffset);  // �ʱ� ī�޶� ��ġ ����

    }
}