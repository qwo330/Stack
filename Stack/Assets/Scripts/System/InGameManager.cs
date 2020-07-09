﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Ready,
    Play,
    Stop,
    Clear,
    GameOver,
}

public class InGameManager : Singleton<InGameManager>
{
    #region Instpectors
    [SerializeField]
    UI_Ingame gameUI;

    [SerializeField]
    CameraCtrl cameraCtrl;

    int stageLevel = 1;
    #endregion

    #region Log
    public int KillMonsterCount;
    public int UseCubeCount;
    #endregion

    public Event<float> PlayerHitEvent = new Event<float>();
    public Event EnemyDeadEvent = new Event();
    public Event UseSkillEvent = new Event();


    public GameState CurrentState { get; private set; }

    public Player player { get; private set; }
    WaitForSeconds updateTime = new WaitForSeconds(1f);

    float remainPlayTime;
    int cubeCount;

    /*     */
    Stack<GameObject> cubeStack = new Stack<GameObject>();
    SortedDictionary<int, GameObject> dic = new SortedDictionary<int, GameObject>();
    // 배열로??

    #region Set Stage
    void Start()
    {
        PlayerHitEvent.AddListener(ShowPlayerLife);
        EnemyDeadEvent.AddListener(EnemyDead);
        UseSkillEvent.AddListener(UseSkill);
    }

    public void Init()
    {
        SetGameState(GameState.Ready);
        LogClear();

        SetPlayer();
        remainPlayTime = SetPlayTime(stageLevel);
        gameUI.InitUI(stageLevel);
    }

    void LogClear()
    {
        KillMonsterCount = 0;
        UseCubeCount = 0;
    }

    public void StartStage()
    {
        SetGameState(GameState.Play);
        StartCoroutine(CO_SpawnEnemy());
        StartCoroutine(CO_Elapse());
    }

    public void SetGameState(GameState state)
    {
        CurrentState = state;
    }

    void SetPlayer()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();

            if (player == null)
            {
                Debug.Log("플레이어를 찾을 수 없음");
                return;
            }

            player.Init();
            cameraCtrl.Init(player.transform);
        }
    }

    float SetPlayTime(int level)
    {
        // todo : level에 따른 plattime 세팅?
        return Defines.PLAY_TIME;
    }

    void ShowPlayerLife(float ratio)
    {
        gameUI.ShowPlayerLife(ratio);
    }

    void ShowPlayTime(float time)
    {
        gameUI.ShowPlayTime(time);
    }
    #endregion

    IEnumerator CO_Elapse()
    {
        while (remainPlayTime > 0)
        {
            if (CurrentState == GameState.Stop || CurrentState == GameState.Ready
                || CurrentState == GameState.GameOver || CurrentState == GameState.Clear)
            {
                yield return null;
            }

            if (CurrentState == GameState.Play)
            {
                yield return updateTime;
                remainPlayTime--;
                ShowPlayTime(remainPlayTime);
            }
        }

        GameOver();
    }

    IEnumerator CO_SpawnEnemy()
    {
        // todo :  level에 따른 몬스터 타입 변경 및 스폰 속도, 개수 조절 등 난이도 설정 필요
        while (true)
        {
            if (CurrentState == GameState.Play)
            {
                for (int i = 0; i < 5; i++)  
                {
                    GameObject enemy = ObjectPool.Get.GetObject(Defines.key_Zombie);
                    Vector3 spawnPos = new Vector3(Random.Range(0, Defines.Screen_Width + 1), player.transform.position.y + Defines.Screen_Height, 0);
                    enemy.transform.position = spawnPos;
                    enemy.GetComponent<BaseEnemy>().SetEnemy();
                    yield return new WaitForSeconds(Defines.SPAWN_INTERVAL);
                }
            }
            yield return new WaitForSeconds(Defines.WAVE_INTERVAL);
        }
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!!");
        SetGameState(GameState.GameOver);
        
    }

    void Clear()
    {
        Debug.Log("GAME CLEAR~@@");
        SetGameState(GameState.Clear);
    }

    void QuitGame()
    {
        remainPlayTime = 0;
        Application.Quit();
#if !UNITY_EDITOR
        System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
    }

    #region ManaCube
    public bool UseManaCube(int count)
    {
        // todo : 나중에 얻은 것부터 count 수만큼의 마나 스톤을 제거하고 스킬 발동
        if (cubeStack.Count < count)
        {
            Debug.Log("자원이 부족합니다.");
            return false;
        }

        while (count != 0)
        {
            count--;
            cubeCount--;

            GameObject cube = cubeStack.Pop();
            cube.GetComponent<ManaCube>().ReturnPool();
            ObjectPool.Get.ReturnObject(cube);
        }

        gameUI.ShowCube(cubeCount);
        return true;
    }

    public void StackManaCube(GameObject manaCube)
    {
        cubeStack.Push(manaCube);
        gameUI.ShowCube(++cubeCount);
    }
    #endregion

    void EnemyDead()
    {
        KillMonsterCount++;
    }

    void UseSkill()
    {
        UseCubeCount++;
    }
}
