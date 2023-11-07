using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieAudio
{
    Hit = 0,
    Dead,
    Walk,
}

public class ZombieController : MonoBehaviour
{
    // Zombie NavMesh
    private NavMeshAgent nav;
    public Transform targetPos;
    private Transform randomTarget; // �÷��̾� �������� �ʾ��� �� ��ġ
    private Transform player; // �÷��̾��� ��ġ

    // RandomTarget NavMesh
    private float range = 10f;
    private Vector3 point;
    public bool isNonTarget = true; // player�� target�� �ƴ� ��

    private Animator zombieAnim;
    [SerializeField] private GameObject screamRange;
    private bool isScreamZombie = false;

    [Header("ȿ����")]
    private AudioSource zombieAudio;
    [SerializeField] private AudioClip[] audioClip;

    // ZombieData
    private SkinnedMeshRenderer skinned;

    private void Awake()
    {
        TryGetComponent(out nav);
        TryGetComponent(out zombieAnim);
        TryGetComponent(out zombieAudio);
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        randomTarget = GameObject.FindGameObjectWithTag("RandomTarget").transform;
        skinned = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
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
        nav.SetDestination(targetPos.position);
    }

    private IEnumerator RandomTargetPos_Co()
    {
        // random target position select
        if (isNonTarget)
        {
            if (RandomPoint(randomTarget.position, range, out point))
            {
                randomTarget.position = point;
            }
            targetPos = randomTarget;
            if (Vector3.Distance(randomTarget.position, transform.position) <= nav.stoppingDistance + 1.0f)
            {
                zombieAnim.SetBool("isIdle", true);
            }
            else
            {
                zombieAnim.SetBool("isIdle", false);
            }
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(RandomTargetPos_Co());
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        // �÷��̾� ���� ���� �� ���� Ÿ�� ��ġ ����, object �ƴ� position���θ� ���� �� �ֵ��� ����... todo
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            isNonTarget = false;
            targetPos = player;
            zombieAnim.SetBool("isPlayerFind", true);
            if (isScreamZombie && Vector3.Distance(player.position, transform.position) > 1.5f)
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
            targetPos = player;
            if (Vector3.Distance(player.position, transform.position) <= 1.5f)
            {
                StartCoroutine(ZombieAttack_Co());
            }
        } else
        {
            if (other.CompareTag("Scream") && !isScreamZombie)
            {
                targetPos = other.gameObject.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            zombieAnim.SetBool("isPlayerFind", false);
            isNonTarget = true;
        }
    }

    private IEnumerator ZombieAttack_Co()
    {
        zombieAnim.SetBool("isAttack", true);
        // Damage �־��ֱ�... todo
        yield return new WaitForSeconds(1.5f);
        zombieAnim.SetBool("isAttack", false);
    }

    private IEnumerator ZombieScream_Co()
    {
        // Player Sound Range�� ����, Scream Range�� �ִ� Zombie �ҷ�����
        zombieAnim.SetBool("isScream", true);
        screamRange.SetActive(true);
        yield return null;
        zombieAnim.SetBool("isScream", false);
        yield return new WaitForSeconds(10f);
        screamRange.SetActive(false);
    }
}
