using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieAudio
{
    Hit = 0,
    Dead,
    Idle,
    Walk,
}

public class ZombieController : MonoBehaviour, IState
{
    // Zombie NavMesh
    private NavMeshAgent nav;
    public Vector3 targetPos;
    private Vector3 randomPos; // �÷��̾� �������� �ʾ��� �� ��ġ
    private Vector3 playerPos; // �÷��̾��� ��ġ
    private bool isTarget = false;

    // RandomTarget NavMesh
    [SerializeField] private float range = 10f;

    private Animator zombieAnim;
    [SerializeField] private GameObject screamRange;
    private bool isScreamZombie = false;

    [Header("ȿ����")]
    private AudioSource zombieAudio;
    [SerializeField] private AudioClip[] audioClip;

    // ZombieData
    private SkinnedMeshRenderer skinned;

    // ZombieAttack Collider
    [SerializeField] private Collider[] zombieAttackCol;

    private void Awake()
    {
        TryGetComponent(out nav);
        TryGetComponent(out zombieAnim);
        TryGetComponent(out zombieAudio);
        /*if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        }*/
        skinned = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        for (int i = 0; i < zombieAttackCol.Length; i++)
        {
            zombieAttackCol[i].enabled = false;
        }
        StartCoroutine(RandomTargetPos_Co());
    }

    private void FixedUpdate()
    {
        ZombieTransToPlayer();
    }

    public void SetUp(ZombieData data)
    {
        // ZombieDataSetUp
        skinned.sharedMesh = data.skinnedMesh;
        isScreamZombie = data.isScreamZombie;
    }

    private void ZombieTransToPlayer()
    {
        // target�� ���� �׺� ����
        nav.SetDestination(targetPos);
    }

    private IEnumerator RandomTargetPos_Co()
    {
        randomPos = GetRandomPosOnNav();
        targetPos = randomPos;
        yield return new WaitForSeconds(5f);
        StartCoroutine(RandomTargetPos_Co());
    }

    private Vector3 GetRandomPosOnNav()
    {
        Vector3 randomDir = Random.insideUnitSphere * range;
        randomDir += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, 20f, NavMesh.AllAreas))
        {
            return hit.position; // navmesh ���� ���� ��ġ ��ȯ
        }
        else
        {
            return transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            playerPos = other.transform.position;
            targetPos = playerPos;
            zombieAnim.SetBool("isPlayerFind", true);
            if (isScreamZombie)
            {
                StartCoroutine(ZombieScream_Co());
            }
        }
        if (other.CompareTag("Attack"))
        {
            // zombie Hit method... todo
            zombieAnim.SetTrigger("isDamage");
            zombieAudio.PlayOneShot(audioClip[(int)ZombieAudio.Hit]);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            playerPos = other.transform.position; // Update the player position
            isTarget = true;
            targetPos = playerPos;
            Debug.Log("�Ҹ�����");
            if (Vector3.Distance(targetPos, transform.position) <= 3f)
            {
                Debug.Log("����");
                StartCoroutine(ZombieAttack_Co());
            }
            Debug.Log("�Ҹ���⸸��");
        }
        else
        {
            if (other.CompareTag("Scream") && !isScreamZombie)
            {
                targetPos = other.gameObject.transform.position; // screamZombie position
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            isTarget = false;
            zombieAnim.SetBool("isPlayerFind", false);
        }
    }

    private IEnumerator ZombieAttack_Co()
    {
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
        zombieAnim.SetBool("isAttack", true);
        for (int i = 0; i < zombieAttackCol.Length; i++)
        {
            // Attack�� ���� Collider enable True
            zombieAttackCol[i].enabled = true;
        }
        // Damage �־��ֱ�... todo
        yield return new WaitForSeconds(1.5f);
        nav.isStopped = false;
        zombieAnim.SetBool("isAttack", false);
        for (int i = 0; i < zombieAttackCol.Length; i++)
        {
            zombieAttackCol[i].enabled = false;
        }
    }

    private IEnumerator ZombieScream_Co()
    {
        // Player Sound Range�� ����, Scream Range�� �ִ� Zombie �ҷ�����
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
        zombieAnim.SetBool("isScream", true);
        screamRange.SetActive(true);
        yield return null;
        nav.isStopped = false;
        zombieAnim.SetBool("isScream", false);
        yield return new WaitForSeconds(10f);
        screamRange.SetActive(false);
    }

    public void Idle()
    {
        if (!isTarget && Vector3.Distance(targetPos, transform.position) <= 0.5f)
        {
            nav.isStopped = true;
            nav.velocity = Vector3.zero;
            zombieAnim.SetBool("isIdle", true);
        }
        else
        {
            nav.isStopped = false;
            zombieAnim.SetBool("isIdle", false);
        }
    }
}
