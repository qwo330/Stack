using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    void OnClick()
    {
        SceneChanger.Instance.ChanageScene(SceneNumber.InGame);
    }
}
