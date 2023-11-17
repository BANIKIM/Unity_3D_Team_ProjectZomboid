using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieAudio
{
    Dead = 0,
    Hit,
    Idle,
    Walk,
}

public class ZombieController : HP, IState
{
    public event Action onDead;

    // Zombie NavMesh
    private NavMeshAgent nav;
    public Vector3 targetPos;
    private Vector3 randomPos; // �÷��̾� �������� �ʾ��� �� ��ġ
    private Vector3 playerPos; // �÷��̾��� ��ġ
    private Vector3 screamPos; // Scream�� ������ ��ġ

    public bool isWakeUp = false; // player�� �׾��� �� player���� true ������ ����
    public bool nonTarget = true;
    private bool nonScreamZombie = true;

    // RandomTarget NavMesh
    private float range = 10f;

    public Animator zombieAnim;
    [SerializeField] private GameObject screamRange;
    private bool isScreamZombie = false;

    [Header("ȿ����")]
    private AudioSource zombieAudio;
    [SerializeField] private AudioClip[] audioClip;

    // Zombie Hit
    private float zombieHp;
    private bool isDie = false;

    // ZombieData
    private SkinnedMeshRenderer skinned;

    // ZombieAttack Collider
    [SerializeField] private Collider zombieAttackCol;
    public float zombieNavDistance;

    private void Awake()
    {
        TryGetComponent(out nav);
        TryGetComponent(out zombieAnim);
        TryGetComponent(out zombieAudio);

        skinned = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        zombieHp = hP;
    }

    private void Start()
    {
        StartCoroutine(RandomTargetPos_Co());
        WakeUp();
    }

    private void FixedUpdate()
    {
        if (!isDie)
        {
            ZombieTransToPlayer();
            Idle();
        }
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
        zombieNavDistance = nav.remainingDistance;
    }
    #region Nav Random Target
    private IEnumerator RandomTargetPos_Co()
    {
        if (!isDie)
        {
            randomPos = GetRandomPosOnNav();
            if (nonTarget)
            {
                targetPos = randomPos;
            }
            yield return new WaitForSeconds(5f);
            StartCoroutine(RandomTargetPos_Co());
        }
    }

    private Vector3 GetRandomPosOnNav()
    {
        Vector3 randomDir = UnityEngine.Random.insideUnitSphere * range;
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
    #endregion
    #region Collider conflict
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
        if (other.CompareTag("Kick") && !isDie)
        {
            // zombie down
            Stun();
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
        else if (other.CompareTag("Attack") || other.CompareTag("Stump") && !isDie)
        {
            StartCoroutine(ZombieDamage_Co());
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Attack") && !isDie)
        {
            zombieAnim.SetTrigger("isDamage");
            zombieHp = Damage(25f, zombieHp);

            if (zombieHp <= 0)
            {
                Die();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fence") || collision.gameObject.CompareTag("Window"))
        {
            // ������ �� Ż�� �ʿ��ؼ� stay�� �־��
            StartCoroutine(Jump());
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            Door_bool doorBool;
            if (collision.gameObject.TryGetComponent(out doorBool))
            {
                if (!doorBool.isOpen)
                {
                    StartCoroutine(ZombieAttack_Co());
                }
            }
        }
    }
    #endregion
    #region Coroutine Attack and Scream
    public IEnumerator ZombieAttack_Co()
    {
        if (!nonTarget)
        {
            zombieAnim.SetBool("isAttack", true);
            NavmeshStop();
            yield return new WaitForSeconds(3.0f);
            // Attack�� ���� Collider enable True
            zombieAttackCol.enabled = true;
            yield return new WaitForSeconds(0.5f);
            zombieAttackCol.enabled = false;
            zombieAnim.SetBool("isAttack", false);
            NavmeshResume();
            if (Vector3.Distance(targetPos, transform.position) <= 2f)
            {
                StartCoroutine(ZombieAttack_Co());
            }
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

    private IEnumerator ZombieDamage_Co()
    {
        yield return new WaitForSeconds(2f);
        zombieHp = Damage(25f, zombieHp);
        if (zombieHp <= 0)
        {
            Die();
        }
    }
    #endregion
    #region IState
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

    public void Die()
    {
        zombieAnim.SetBool("isDie", true);
        zombieAnim.SetTrigger("isD");
        transform.position = new Vector3(transform.position.x, 0, transform.position.z); // ���� �� y���� �ٲ�� ���� ������ y�� ����

        TryGetComponent(out CapsuleCollider collider);
        TryGetComponent(out Rigidbody rigid);
        isDie = true;
        NavmeshStop();
        zombieAudio.PlayOneShot(audioClip[(int)ZombieAudio.Dead]);

        rigid.isKinematic = true;
        collider.enabled = false;

        if (onDead != null)
        {
            onDead();
        }
    }

    public IEnumerator Jump()
    {
        NavmeshStop();
        zombieAnim.SetTrigger("isClimb");
        NavmeshResume();
        yield return new WaitForSeconds(2f);
    }

    public void WakeUp()
    {
        if (isWakeUp)
        {
            isWakeUp = false;
            NavmeshStop();
            zombieAnim.SetTrigger("isWakeUp");
            NavmeshResume();
        }
    }

    public void Stun()
    {
        NavmeshStop();
        zombieAnim.SetTrigger("isStun");
        NavmeshResume();
    }
    #endregion
    #region Nav Stop And Resume
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
        // stop after
        nav.isStopped = false;
        nav.updatePosition = true;
        nav.updateRotation = true;
    }
    #endregion
}