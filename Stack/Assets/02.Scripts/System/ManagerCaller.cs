using UnityEngine;
using Managers.SceneChanger;
using Managers.SoundManager;

public class ManagerCaller : MonoBehaviour {
	void Start () {
        SceneChanger.Instance.Init();
        SoundManager.Instance.Init();
    }
}
