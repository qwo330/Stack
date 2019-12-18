using System.Collections;
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

    [SerializeField]
    int StageLevel;


    #endregion
    public Event<float> playerHitEvent = new Event<float>();
    public GameState CurrentState { get; private set; }

    public Player player { get; private set; }
    WaitForSeconds updateTime = new WaitForSeconds(1f);


    float remainPlayTime;
    int cubeCount;


    public void Init()
    {
        SetGameState(GameState.Ready);
        playerHitEvent.AddListener(ShowPlayerLife);
    }

    void ShowPlayerLife(float ratio)
    {
        gameUI.ShowPlayerLife(ratio);
    }

    void ShowPlayTime(float time)
    {
        gameUI.ShowPlayTime(time);
    }

    public void StartStage(int level)
    {
        StageLevel = level;
        if (player == null)
        {
            player = FindObjectOfType<Player>();

            if (player == null)
            {
                Debug.Log("플레이어를 찾을 수 없음");
                return;
            }

            player.Init();
        }

        cameraCtrl.Init(player.transform);

        remainPlayTime = SetPlayTime(level);
        gameUI.StartStage(level);

        SetGameState(GameState.Play);
        //StartCoroutine(CO_SpawnEnemy());
        StartCoroutine(CO_Elapse());
    }

    float SetPlayTime(int level)
    {
        // todo : level에 따른 plattime 세팅?
        return Defines.PLAY_TIME;
    }

    public void SetGameState(GameState state)
    {
        CurrentState = state;
    }

    IEnumerator CO_Elapse()
    {
        while (remainPlayTime > 0)
        {
            //if (CurrentState == GameState.Stop)
            //{
            //    yield return null;
            //}
            if (CurrentState == GameState.Play)
            {
                yield return updateTime;
                remainPlayTime--;
                ShowPlayTime(remainPlayTime);
            }
            else
                yield return null;
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
                    Vector3 spawnPos = new Vector3(Random.Range(0, Defines._Width), player.transform.position.y + Defines._Height, 0);
                    enemy.transform.position = spawnPos;
                    enemy.GetComponent<BaseEnemy>().SetEnemy();
                    yield return new WaitForSeconds(Defines.SPAWN_INTERVAL);
                }
                yield return new WaitForSeconds(Defines.WAVE_INTERVAL);
            }
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

    public bool UseManaStone(int count)
    {
        // todo : 화면 상단부터 count 수만큼의 마나 스톤을 제거(하고 스킬 사용)
        if (cubeCount < count)
        {
            Debug.Log("자원이 부족합니다.");
            return false;
        }

        //width : 9, height = 20
        for (int key = (MaxHeight * 10) - 1; key >= 0; key--)
        {
            if (count == 0) return false;
            if (key % 10 == 9) key--;

            if (dic.ContainsKey(key))
            {
                Debug.Log("블럭 제거! key : " + key);
                GameObject obj;
                dic.TryGetValue(key, out obj);
                dic.Remove(key);
                count--;
                cubeCount--;

                ObjectPool.Get.ReturnObject(obj);
                obj = null;
            }
        }

        gameUI.ShowCube(cubeCount);
        return true;
    }

    public void StackManaStone(GameObject manaStone)
    {
        int key = (int)(manaStone.transform.position.x)
            + ((int)(Mathf.Round(manaStone.transform.position.y)) * 10);
        dic.Add(key, manaStone);
    }

    public void DropManaStone(Vector3 enemyPos)
    {
        GameObject obj = ObjectPool.Get.GetObject(Defines.key_ManaStone);
        cubeCount++;
        obj.transform.position = new Vector3(Mathf.Round(enemyPos.x), enemyPos.y, 0);
    }
    /*     */
    SortedDictionary<int, GameObject> dic = new SortedDictionary<int, GameObject>();
    // 배열로??
    public int MaxHeight = 20;
}
