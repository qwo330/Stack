using UnityEngine;

public class GameScene : MonoBehaviour {

    void Start ()
    {
        InGameManager.Instance.Init();
        int level = SceneChanger.Instance.StageLevel;
        InGameManager.Instance.StartStage(level);
	}
}