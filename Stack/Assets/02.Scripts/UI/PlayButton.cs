using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.SceneChanger;

namespace UIs.Button
{
    public class PlayButton : MonoBehaviour
    {
        void OnClick()
        {
            SceneChanger.Instance.ChanageScene(SceneNumber.InGame);
        }
    }
}
