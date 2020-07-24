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
    LevelManager levelMgr;

    int stageLevel = 1;
    #endregion

    #region Log
    public int KillMonsterCount;
    public int UseCubeCount;
    #endregion

    public Event<float> PlayerHitEvent = new Event<float>();
    public Event EnemyDeadEvent = new Event();
    public Event UseSkillEvent = new Event();
    public Event GameClearEvent = new Event();
    public Event GameStartEvent = new Event();

    public GameState CurrentState { get; private set; }

    public Player player { get; private set; }
    WaitForSeconds waitOneSeconds = new WaitForSeconds(1f);

    float remainPlayTime;
    int cubeCount;
    Coroutine elapseCoroutine;
    Coroutine readyCoroutine;

    Stack<GameObject> cubeStack = new Stack<GameObject>();

    void Awake()
    {
        levelMgr.gameMgr = this;
    }

    void Start()
    {
        PlayerHitEvent.AddListener(ShowPlayerLife);
        EnemyDeadEvent.AddListener(EnemyDead);
        UseSkillEvent.AddListener(UseSkill);
        GameClearEvent.AddListener(ClearGame);
    }

    public void Init()
    {
        SetGameState(GameState.Ready);
        GetPlayer();

        StartGame();
    }

    public void StartGame()
    {
        if (readyCoroutine == null)
        {
            string startMessage = string.Format("Level {0} Start", stageLevel);
            gameUI.ShowText(startMessage);

            SetGameState(GameState.Play);

            LogClear();
            SetPlayer();
            gameUI.InitUI(stageLevel);

            ResetCube();
            levelMgr.ResetEnemys();

            if (elapseCoroutine != null)
                StopCoroutine(elapseCoroutine);

            readyCoroutine = StartCoroutine(StartProcess());
        }
    }

    IEnumerator StartProcess()
    {
        yield return waitOneSeconds;
        yield return waitOneSeconds;

        elapseCoroutine = StartCoroutine(CO_Elapse());
        levelMgr.Init(stageLevel);

        readyCoroutine = null;
    }

    public void SetGameState(GameState state)
    {
        CurrentState = state;
    }

    public bool CheckPlaying()
    {
        return CurrentState == GameState.Play;
    }

    void GetPlayer()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();

            if (player == null)
            {
                Defines.PrintLog("플레이어를 찾을 수 없음");
                return;
            }

            player.GetPlayer();
            cameraCtrl.Init(player.transform);
        }
    }

    void SetPlayer()
    {
        player.Init();
    }

    IEnumerator CO_Elapse()
    {
        remainPlayTime = Defines.PLAY_TIME;

        while (remainPlayTime > 0)
        {
            if (CurrentState == GameState.Stop || CurrentState == GameState.Ready
                || CurrentState == GameState.GameOver || CurrentState == GameState.Clear)
            {
                yield return null;
            }

            else if (CheckPlaying())
            {
                yield return waitOneSeconds;
                remainPlayTime--;
                ShowPlayTime(remainPlayTime);
            }
        }

        SetGameState(GameState.GameOver);
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        gameUI.ShowText("Game Over");
    }

    void ClearGame()
    {
        SetGameState(GameState.Clear);
        gameUI.ShowText("Stage Clear");
        ++stageLevel;

        StartGame();
    }

    public void QuitGame()
    {
#if !UNITY_EDITOR
        Application.Quit();
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

    void ResetCube()
    {
        foreach (var obj in cubeStack)
        {
            ObjectPool.Get.ReturnObject(obj);
        }
        cubeStack.Clear();
        cubeCount = 0;
    }
    #endregion

    void ShowPlayerLife(float ratio)
    {
        gameUI.ShowPlayerLife(ratio);
    }

    void ShowPlayTime(float time)
    {
        gameUI.ShowPlayTime(time);
    }

    void EnemyDead()
    {
        KillMonsterCount++;
    }

    void UseSkill()
    {
        UseCubeCount++;
    }

    void LogClear()
    {
        KillMonsterCount = 0;
        UseCubeCount = 0;
    }
}
