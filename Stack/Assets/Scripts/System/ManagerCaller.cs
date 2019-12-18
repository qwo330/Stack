using UnityEngine;

public class ManagerCaller : MonoBehaviour {
	void Start () {
        SceneChanger.Instance.Init();
        SoundManager.Instance.Init();
    }
}
