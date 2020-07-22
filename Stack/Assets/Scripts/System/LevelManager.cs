using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// level 1: only zombie 1 ~ 3
/// level 2: zombei & ghost 2 ~ 4
/// level 3: zombei & ghost 3 ~ 5
/// level 4: + boss dragon
/// </summary>

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public InGameManager gameMgr;

    [SerializeField]
    DestinationLine destinationLine;

    int destinationHeight;

    int stageLevel;
    int minSpawnCount, maxSpawnCount;

    float prevSpawnX;
    float ZombieRatio, GhostRatio;

    WaitForSeconds spawnInterval;
    Stack<GameObject> SpawnedEnemys = new Stack<GameObject>();
    Coroutine enemySpawnCoroutine;

    void Start()
    {
        InGameManager.Instance.GameClearEvent.AddListener(ClearGame);
    }

    public void Init(int level)
    {
        Debug.Log(" ====== Level Init =======");
        SetLevel(level);
        SpawnEnemyLoop();
        //StopCoroutine(CO_SpawnEnemy());
        //StartCoroutine(CO_SpawnEnemy());
    }

    public void SetLevel(int level)
    {
        stageLevel = level;
        destinationHeight = Defines.Screen_Height + level;
        destinationLine.SetPosition(destinationHeight);
        float levelRate = (level - 1) * 0.2f;

        minSpawnCount = stageLevel % 4;
        maxSpawnCount = Mathf.Min(minSpawnCount + 2, Defines.Screen_Width);

        ZombieRatio = 1 - levelRate;
        GhostRatio = levelRate;

        spawnInterval = new WaitForSeconds(Defines.SPAWN_INTERVAL);
    }

    void SpawnEnemyLoop()
    //IEnumerator CO_SpawnEnemy()
    {
        // todo :  level에 따른 몬스터 타입 변경 및 스폰 속도, 개수 조절 등 난이도 설정 필요
        System.Action loopMethod = () =>
        {
            int spawnCount = Random.Range(minSpawnCount, maxSpawnCount);

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnEnemy();
            }
        };

        enemySpawnCoroutine = StartCoroutine(CO_PlayLoop(loopMethod));




        //if (InGameManager.Instance.CheckPlaying())
        //{
        //    while (InGameManager.Instance.CheckPlaying())
        //    {
        //        yield return spawnInterval;
        //        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount);

        //        for (int i = 0; i < spawnCount; i++)
        //        {
        //            SpawnEnemy();
        //        }
        //    }
        //}
        //else
        //{
        //    while (true)
        //        yield return null;
        //}
    }

    public IEnumerator CO_PlayLoop(System.Action method)
    {
        while (true)
        {
            bool isPlaying = InGameManager.Instance.CheckPlaying();

            if (isPlaying)
            {
                yield return spawnInterval;
                method();
            }
            else
            {
                yield return null;
            }
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = GetRandomEnemy();
        Vector3 spawnPos = GetRandomSpawnPosition();
        enemy.transform.position = spawnPos;
        enemy.GetComponent<BaseEnemy>().SetEnemy(stageLevel);

        SpawnedEnemys.Push(enemy);
    }

    GameObject GetRandomEnemy()
    {
        float rdValue = Random.Range(0, 1f);
        string name = string.Empty;

        if (rdValue <= ZombieRatio)
        {
            name = Defines.key_Zombie;
        }
        else
        {
            name = Defines.key_Ghost;
        }

        GameObject obj = ObjectPool.Get.GetObject(name);
        return obj;
    }

    Vector3 GetRandomSpawnPosition()
    {
        int newX;
        do
        {
            newX = Random.Range(0, Defines.Screen_Width + 1);
        } while (newX == prevSpawnX);
        prevSpawnX = newX;
        Vector3 result = new Vector3(newX, destinationHeight, 0);
        return result;
    }

    void ClearGame()
    {
        ReturnAllEnemys();
        if (enemySpawnCoroutine != null)
            StopCoroutine(enemySpawnCoroutine);
    }

    void ReturnAllEnemys()
    {
        for (int i = 0; i < SpawnedEnemys.Count; i++)
        {
            var obj = SpawnedEnemys.Pop();
            ObjectPool.Get.ReturnObject(obj);
        }
    }
}
