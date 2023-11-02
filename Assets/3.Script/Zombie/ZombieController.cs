using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    // Zombie NavMesh
    private NavMeshAgent nav;
    private GameObject player; // �÷��̾��� ���� ��ġ
    private Animator zombieAnim;
    private int animSelect = 0;
    private bool isPlayer = false; // �÷��̾� �Ҹ����� ������ trigger �Ǿ��� �� true

    private void Awake()
    {
        TryGetComponent(out nav);
        TryGetComponent(out zombieAnim);
        player = GameObject.FindGameObjectWithTag("Player");

        nav.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(ZombieAnim_Co());
    }

    private void FixedUpdate()
    {
        ZombieTransToPlayer();
    }

    private void ZombieTransToPlayer()
    { // Zombie�� �÷��̾ ��ô�ϴ� �޼ҵ�
        if (player)
        { // �÷��̾ ������� ��
            if (isPlayer)
            { // �÷��̾� �Ҹ� ���� �ȿ� �ְ�
                nav.SetDestination(player.transform.position);
            } else
            { // �÷��̾� �Ҹ� ���� �ȿ� ����(Hide ��)
              // ���� ���̴� �þ߰� �־��ֱ�...
                
            }
        }
    }

    private IEnumerator ZombieAnim_Co()
    {
        yield return new WaitForSeconds(3f);

        animSelect = Random.Range(0, 3);
        if (animSelect.Equals(0))
        { // idle
            zombieAnim.SetBool("isWalk", false);
        }
        else
        { // walk
            zombieAnim.SetBool("isWalk", true);
        }
        StartCoroutine(ZombieAnim_Co());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            zombieAnim.SetBool("isPlayerFind", true);
            isPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            zombieAnim.SetBool("isPlayerFind", false);
            isPlayer = false;
        }
    }
}
