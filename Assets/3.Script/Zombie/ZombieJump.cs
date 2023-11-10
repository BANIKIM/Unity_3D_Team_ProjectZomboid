using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieJump : MonoBehaviour
{
    private ZombieController zombieController;
    private ZombieFieldOfView zombieFieldOfView;
    public Vector3 myPos;
    public float lookingAngle;
    public Vector3 lookDir;

    private Vector3 targetPos;
    

    private void Awake()
    {
        TryGetComponent(out zombieController);
        TryGetComponent(out zombieFieldOfView);
    }

    private void Start()
    {
        myPos = zombieFieldOfView.myPos;
        lookingAngle = zombieFieldOfView.lookingAngle;
        lookDir = zombieFieldOfView.lookDir;
    }

    private void Update()
    {
        JombieClimbing();
    }

    private void JombieClimbing()
    {
        // IState�� �ű���� �ֽ��ϴ�.
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.forward * 5f, Color.yellow);

        if (Physics.Raycast(myPos, lookDir * 2f, out hit))
        {
            if (hit.collider.CompareTag("Window"))
            {
                targetPos = hit.collider.gameObject.transform.position; // ������ ��ġ������ ������
                zombieController.targetPos = targetPos;
                if (Vector3.Distance(targetPos, transform.position) <= 1.5f)
                {
                    // jump anim
                    Debug.Log("Window");
                }
            }
            else if (hit.collider.CompareTag("Fence"))
            {
                if (Vector3.Distance(targetPos, transform.position) <= 1.5f)
                {
                    // jump anim
                    Debug.Log("Fence");
                }
            }
            else if (hit.collider.CompareTag("Door"))
            {
                Door_bool door = hit.collider.GetComponentInChildren<Door_bool>();
                if (!door.isOpen)
                {
                    // attack coroutine
                    if (Vector3.Distance(targetPos, transform.position) <= 1.5f)
                    {
                        // jump anim
                        Debug.Log("Door");
                    }
                }
            }
        }
    }
}
