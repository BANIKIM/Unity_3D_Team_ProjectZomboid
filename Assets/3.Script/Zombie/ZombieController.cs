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
    private Vector3 screamPos; // Scream�� ������ ��ġ

    public bool nonTarget = true;
    private bool nonScreamZombie = true;

    // RandomTarget NavMesh
    private float range = 10f;

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
        Idle();
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
        if (nonTarget)
        {
            targetPos = randomPos;
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(RandomTargetPos_Co());
    }

    private Vector3 GetRandomPosOnNav()
    {
        Vector3 randomDir = Random.insideUnitSphere * range;
        randomDir += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, range, NavMesh.AllAreas))
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
            zombieAnim.SetBool("isPlayerFind", true);
            nonTarget = false;
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
            targetPos = playerPos;
            if (Vector3.Distance(targetPos, transform.position) <= 1f)
            {
                StartCoroutine(ZombieAttack_Co());
            }
        }
        else
        {
            if (other.CompareTag("Scream") && !isScreamZombie)
            {
                nonTarget = false;
                nonScreamZombie = false;
                screamPos = other.gameObject.transform.position;
                targetPos = screamPos; // screamZombie position
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            zombieAnim.SetBool("isPlayerFind", false);
            nonTarget = true;
        }
    }

    private IEnumerator ZombieAttack_Co()
    {
        zombieAnim.SetBool("isAttack", true);
        NavmeshStop();
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < zombieAttackCol.Length; i++)
        {
            // Attack�� ���� Collider enable True
            zombieAttackCol[i].enabled = true;
        }
        // Damage �־��ֱ�... todo
        yield return new WaitForSeconds(1.5f);
        zombieAnim.SetBool("isAttack", false);
        NavmeshResume();
        for (int i = 0; i < zombieAttackCol.Length; i++)
        {
            zombieAttackCol[i].enabled = false;
        }
        if (Vector3.Distance(targetPos, transform.position) <= 2f)
        {
            StartCoroutine(ZombieAttack_Co());
        }
    }

    private IEnumerator ZombieScream_Co()
    {
        // Player Sound Range�� ����, Scream Range�� �ִ� Zombie �ҷ�����
        zombieAnim.SetBool("isScream", true);
        screamRange.SetActive(true);
        NavmeshStop();
        yield return null;
        zombieAnim.SetBool("isScream", false);
        yield return new WaitForSeconds(10f);
        screamRange.SetActive(false);
        NavmeshResume();
        nonScreamZombie = true;
    }

    public void Idle()
    {
        if (nonTarget && Vector3.Distance(targetPos, transform.position) <= 0.7f)
        {
            zombieAnim.SetBool("isIdle", true);
            NavmeshStop();
        }
        else
        {
            zombieAnim.SetBool("isIdle", false);
            NavmeshResume();
        }
    }

    private void NavmeshStop()
    {
        // don't slide
        nav.isStopped = true;
        nav.updatePosition = false;
        nav.updateRotation = false;
        nav.velocity = Vector3.zero;
    }

    private void NavmeshResume()
    {
        // stop
        nav.isStopped = false;
        nav.updatePosition = true;
        nav.updateRotation = true;
    }
}