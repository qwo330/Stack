using UnityEngine;
using Managers.InGameManager;
using Managers.SceneChanger;

public class GameStarter : MonoBehaviour {
    public int StageLevel;

    void Start ()
    {
        StageLevel = SceneChanger.Instance.StageLevel;
        InGameManager.Instance.Init(StageLevel);
	}
}
