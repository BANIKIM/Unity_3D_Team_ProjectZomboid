using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WindowActive : MonoBehaviour
{
    // Player_Move�� �޷�����
    public float radius = 1f;
    private Collider windowCollider;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1.5f, 0), radius);
    }

    private void Update()
    {
        WindowInteraction();
    }

    private void WindowInteraction()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + new Vector3(0, 1.5f, 0), radius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Window"))
            {
                windowCollider = collider;
                // window tag�� ���� â�� �𵨸��� �޾Ƶα�
                if (Input.GetKeyDown(KeyCode.E))
                {
                    WindowOpen();
                };
            }
        }
    }

    public void WindowOpen()
    {
        // player�� â�� ���� Ű ������ ��
        WIndow_bool window = windowCollider.GetComponent<WIndow_bool>();
        if (!window.isBroken)
        {
            window.WindowAnimation();
        }
    }

    public void WindowBroken()
    {
        // â�� �μ���, Player Attack�� window�� enter���� ��
        WIndow_bool window = windowCollider.GetComponent<WIndow_bool>();
        MusicController.instance.PlaySFXSound("Window_Bottele");
        if (!window.isBroken)
        {
            window.isOpen = !window.isOpen;
            window.isBroken = true;
        }
    }
}