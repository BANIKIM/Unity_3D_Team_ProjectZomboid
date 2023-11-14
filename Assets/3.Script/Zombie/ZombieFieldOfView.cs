using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFieldOfView : MonoBehaviour
{
    private ZombieController zombieController;
    // [Range(0f, 360f)] [SerializeField]
    private float viewAngle = 130f; // �����ϴ� ���� ����
    [SerializeField] private float viewRadius = 10f; // ���� ����
    [SerializeField] private LayerMask targetMask; // Ÿ�� �ν� ���̾�, Player, Object(Window, Door, Fence)
    [SerializeField] private LayerMask obstacleMask;
    private List<Collider> hitPlayerList = new List<Collider>(); // ������ �÷��̾� ����Ʈ

    private Vector3 playerPos;
    public Vector3 zombiePos;
    public Vector3 lookDir;
    public float lookingAngle;

    private void Awake()
    {
        TryGetComponent(out zombieController);
    }

    private void ZombieTargeting(Vector3 targetPos)
    {
        zombieController.targetPos = targetPos;
    }

    private void OnDrawGizmos()
    {
        zombiePos = transform.position + Vector3.up * 1.5f; // ���� ������
        Gizmos.DrawWireSphere(zombiePos, viewRadius);

        lookingAngle = transform.eulerAngles.y; // ���� �ٶ󺸴� ������ ����
        lookDir = AngleToDir(lookingAngle);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - viewAngle * 0.5f);

        Debug.DrawRay(zombiePos, rightDir * viewRadius, Color.blue);
        Debug.DrawRay(zombiePos, leftDir * viewRadius, Color.blue);
        Debug.DrawRay(zombiePos, lookDir * viewRadius, Color.cyan);

        hitPlayerList.Clear();

        Collider[] targets = Physics.OverlapSphere(zombiePos, viewRadius, targetMask);

        if (targets.Length.Equals(0)) return;

        foreach (Collider playerColli in targets)
        { // target list
            playerPos = playerColli.transform.position;
            Vector3 targetDir = (playerPos - zombiePos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= viewAngle * 0.5f && !Physics.Raycast(zombiePos, targetDir, viewRadius, obstacleMask))
            {
                hitPlayerList.Add(playerColli);
                ObjectTargeting(playerColli); // tag Ȯ�� �� targeting method
                Debug.DrawLine(zombiePos, playerPos, Color.red);
            }
        }
    }

    public Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    private void ObjectTargeting(Collider colli)
    {
        if (colli.CompareTag("Player"))
        {
            ZombieTargeting(playerPos); // target ��ġ
        }
        else if (colli.CompareTag("Window") || colli.CompareTag("Fence"))
        {
            Debug.Log(Vector3.Distance(colli.gameObject.transform.position, transform.position));
            if (Vector3.Distance(colli.gameObject.transform.position, transform.position) <= 2f)
            {
                // Jump
                zombieController.Jump();
            }
        } else if (colli.CompareTag("Door"))
        {
            Debug.Log(Vector3.Distance(colli.gameObject.transform.position, transform.position));
            if (Vector3.Distance(colli.gameObject.transform.position, transform.position) <= 2f)
            {
                // Attack
                StartCoroutine(zombieController.ZombieAttack_Co());
            }
        }
    }
}