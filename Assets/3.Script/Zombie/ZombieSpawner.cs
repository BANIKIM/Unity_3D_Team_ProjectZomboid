using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public ZombieData[] zombieDatas;
    public ZombieController zombieController;
    [SerializeField] private Transform[] spawnPoint;
    private List<ZombieController> zombieList = new List<ZombieController>();
    private int zombieCount = 100;

    private void Awake()
    {
        SetUpSpawnPoint();
    }

    private void SetUpSpawnPoint()
    {
        spawnPoint = new Transform[transform.childCount]; // transform.childCount: �ڽ��� ����

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            spawnPoint[i] = transform.GetChild(i).transform;
        }
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
        zombieController.SetUp(data);
        zombieList.Add(zombieController);
    }
}