using UnityEngine;
using Managers.SceneChanger;
using Managers.AssetBundleManager;
using Managers.SoundManager;

public class ManagerCaller : MonoBehaviour {
	void Start () {
        AssetBundleManager.Instance.Init();
        SceneChanger.Instance.Init();
        SoundManager.Instance.Init();
    }
}
