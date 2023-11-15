using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Fog : MonoBehaviour
{
    [Range(0f, 360f)] [SerializeField] public float viewAngle = 130f; // �����ϴ� ���� ����
    [SerializeField] public float ViewRadius = 2f; // ���� ����
    [SerializeField] private LayerMask TargetMask; // Ÿ�� �ν� ���̾�, Player
    [SerializeField] private LayerMask ObstacleMask;
    private List<Collider> hitTargetList = new List<Collider>(); // ������ Ÿ�� ����Ʈ
    
    public Vector3 myPos;
    public float lookingAngle;
    public Vector3 lookDir;

    private void Awake()
    {
       
    }



    private void OnDrawGizmos()
    {
        myPos = transform.position + Vector3.up * 1.5f; // ĳ���� ������
        Gizmos.DrawWireSphere(myPos, ViewRadius);

        lookingAngle = transform.eulerAngles.y; //ĳ���Ͱ� �ٶ󺸴� ������ ����
        lookDir = AngleToDir(lookingAngle);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - viewAngle * 0.5f);

        Debug.DrawRay(myPos, rightDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, leftDir * ViewRadius, Color.blue);
        // Debug.DrawRay(myPos, lookDir * ViewRadius, Color.cyan);

        hitTargetList.Clear();
        Collider[] targets = Physics.OverlapSphere(myPos, ViewRadius, TargetMask);

        if (targets.Length == 0) return;
        foreach (Collider playerColli in targets)
        {
            Vector3 targetPos = playerColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;

            if (playerColli.transform.childCount > 0) // �ڽ� ������Ʈ ���� Ȯ��
            {
                if (targetAngle <= viewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, ViewRadius, ObstacleMask))
                {
                    hitTargetList.Add(playerColli);
                    playerColli.transform.GetChild(0).gameObject.SetActive(true);
                    Debug.DrawLine(myPos, targetPos, Color.red);
                }
                else
                {
                    playerColli.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
        /* foreach (Collider playerColli in targets)
         { // target list

             Vector3 targetPos = playerColli.transform.position;
             Vector3 targetDir = (targetPos - myPos).normalized;
             float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
             if (targetAngle <= viewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, ViewRadius, ObstacleMask))
             {
                 hitTargetList.Add(playerColli);
                 playerColli.transform.GetChild(0).gameObject.SetActive(true);
                 Debug.DrawLine(myPos, targetPos, Color.red);
             }
             else
             {
                 playerColli.transform.GetChild(0).gameObject.SetActive(false);
             }


         }*/





    }

    public Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }
}


/* //
 private float viewAngle = 130f; // �����ϴ� ���� ����
 [SerializeField] private float ViewRadius = 2f; // ���� ����
 [SerializeField] private LayerMask TargetMask; // Ÿ�� �ν� ���̾�, Player

 private void Update()
 {
     GameObject[] secondtag = GameObject.FindGameObjectsWithTag(targetTag);

     // ī�޶�� �÷��̾��� �߰� ����
     Vector3 midPoint = (AA.transform.position + transform.position) / 2f;

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
             obj.transform.GetChild(0).gameObject.SetActive(false);
         }
         else
         {
             obj.transform.GetChild(0).gameObject.SetActive(true);
         }
     }
 }

}
*/