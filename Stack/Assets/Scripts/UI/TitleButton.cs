using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    void OnClick()
    {
        SceneChanger.Instance.ChanageScene(SceneNumber.Robby);
    }
}