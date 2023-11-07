using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_Car : MonoBehaviour
{
    public Transform follow;  // �÷��̾��� Transform
    private bool isCar;
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
        float scrollData = Input.GetAxis("Mouse ScrollWheel");  // ��ũ�� ������

        float nextOffset = offset - scrollData * (zoomSpeed * 10);  // ���� �����ӿ����� offset ���
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

        transform.position = follow.position + new Vector3(-xOffset, offset, -zOffset);  // ī�޶� ��ġ ������Ʈ
    }

    private void Camera_Early()
    {
        transform.position = follow.position + new Vector3(-xOffset, offset, -zOffset);  // �ʱ� ī�޶� ��ġ ����

    }

}