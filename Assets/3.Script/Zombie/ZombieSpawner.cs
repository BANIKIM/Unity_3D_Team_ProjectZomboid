using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public ZombieData[] zombieDatas;
    public ZombieController zombieController;
    [SerializeField] private Transform[] spawnPoint;
    private int zombieCount = 50;
    public List<ZombieController> zombieList = new List<ZombieController>();

    private float dayTime = 1440f; // 1day time count

    private void Start()
    {
        StartCoroutine(SetUpSpawnPoint());
    }

    private IEnumerator SetUpSpawnPoint()
    {
        spawnPoint = new Transform[transform.childCount]; // transform.childCount: �ڽ��� ����

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            spawnPoint[i] = transform.GetChild(i).transform;
        }

        yield return new WaitForSeconds(dayTime); // 1 day Time���� �ٲ��ֱ�
    }

    private void Update()
    {
        if (zombieList.Count <= zombieCount)
        { // 20���� ������ �� ���� ����
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        for (int i = 0; i < zombieCount - zombieList.Count; i++)
        { // ���ڶ� ����ŭ ����
            CreateZombie();
        }
    }

    private void CreateZombie()
    {
        ZombieData data = zombieDatas[Random.Range(0, zombieDatas.Length)];
        Transform point = spawnPoint[Random.Range(0, spawnPoint.Length)];
        ZombieController zombieController = Instantiate(this.zombieController, point.position, point.rotation);
        zombieController.transform.SetParent(gameObject.transform); // zombie list ZombieListObject�� ���
        zombieController.SetUp(data); // RandomZombie Data SetUp
        zombieList.Add(zombieController);

        // zombie Die()
        zombieController.onDead += () => { zombieList.Remove(zombieController); };
        zombieController.onDead += () => { Destroy(zombieController.gameObject, 10f); };
    }
}