using UnityEngine;

public class GameScene : MonoBehaviour {

    void Start ()
    {
        InGameManager.Instance.Init();
        InGameManager.Instance.StartStage();
	}
}