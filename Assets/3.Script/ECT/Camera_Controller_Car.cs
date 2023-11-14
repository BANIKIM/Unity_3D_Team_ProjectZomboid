using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_Car : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform

    public float zoomSpeed;  // �� �ӵ�
    public float minZoom;  // �ּ� ��
    public float maxZoom;  // �ִ� ��

    [Header("�⺻������")]
    public float Yoffset;  // �÷��̾�� ī�޶��� �Ÿ�
    [SerializeField] private float xOffset;
    [SerializeField] private float zOffset;

    private void Start()
    {
        //Camera_Early();
    }

    private void LateUpdate()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");  // ��ũ�� ������

        float nextOffset = Yoffset - scrollData * (zoomSpeed * 10);  // ���� �����ӿ����� offset ���
        nextOffset = Mathf.Clamp(nextOffset, minZoom, maxZoom);  // �� ����

        // offset�� minZoom�� maxZoom ������ ���� xOffset, zOffset, offset ������Ʈ
        if (nextOffset > minZoom && nextOffset < maxZoom)
        {
            if (scrollData > 0) // ��ũ���� �÷��� ��
            {
                xOffset -= zoomSpeed; // X�� Z�࿡ ���� �������� ����.
                zOffset -= zoomSpeed;
                Yoffset = nextOffset;
            }
            else if (scrollData < 0) // ��ũ���� ������ ��
            {
                xOffset += zoomSpeed;
                zOffset += zoomSpeed; // X�� Z�࿡ ���� �������� �ø�.
                Yoffset = nextOffset;
            }
        }


        transform.position = player.position + new Vector3(-xOffset, Yoffset, -zOffset);  // ī�޶� ��ġ ������Ʈ

    }

    private void Camera_Early()
    {

        transform.position = player.position + new Vector3(-8, 16, -8);  // �ʱ� ī�޶� ��ġ ����

    }

}