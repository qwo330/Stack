using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// level 1: only zombie
/// level 2: zombei & ghost
/// level 3: zombei & ghost
/// level 4: boss dragon
/// </summary>

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public InGameManager gameMgr;

    [SerializeField]
    DestinationLine destinationLine;

    int stageLevel;
    int destinationHeight = 20;

    float prevSpawnX;
    float ZombieRatio, GhostRatio;


    WaitForSeconds spawnInterval, waveInterval;

    public void Init(int level)
    {
        SetLevel(level);
        StartCoroutine(CO_SpawnEnemy());
    }

    public void SetLevel(int level)
    {
        stageLevel = level;
        destinationLine.SetPosition(20 + level);
        float levelRate = (level - 1) * 0.2f;

        ZombieRatio = 1 - levelRate;
        GhostRatio = levelRate;

        spawnInterval = new WaitForSeconds(Defines.SPAWN_INTERVAL - (levelRate * 0.5f));
        waveInterval = new WaitForSeconds(Defines.WAVE_INTERVAL - levelRate);
    }

    IEnumerator CO_SpawnEnemy()
    {
        // todo :  level에 따른 몬스터 타입 변경 및 스폰 속도, 개수 조절 등 난이도 설정 필요
        while (InGameManager.Instance.CurrentState == GameState.Play)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject enemy = GetRandomEnemy();
                Vector3 spawnPos = GetRandomSpawnPosition();
                enemy.transform.position = spawnPos;
                enemy.GetComponent<BaseEnemy>().SetEnemy(stageLevel);
                yield return spawnInterval;
            }

            yield return waveInterval;
        }
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
}
