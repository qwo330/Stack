using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ingame : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    Text txtTime;

    [SerializeField]
    Text txtCube;

    [SerializeField]
    Image imgLife;
    #endregion

    float lifeWitdh, lifeHeight;

    void Awake()
    {
        Sprite heart = imgLife.sprite;
        lifeWitdh = heart.rect.width;
        lifeHeight = heart.rect.height;
    }

    public void StartStage(int level)
    {
        // todo : level에 따른 plattime 세팅?
        ShowPlayTime(Defines.PLAY_TIME);
        ShowCube(0);
        ShowPlayerLife(InGameManager.Instance.player.maxHP);
    }

    public void ShowPlayerLife(float life)
    {
        float width = lifeWitdh * life;
        imgLife.rectTransform.sizeDelta = new Vector2(width, lifeHeight);
    }

    public void ShowPlayTime(float time)
    {
        txtTime.text = time.ToString("N2");
    }

    public void ShowCube(int count)
    {
        txtTime.text = count.ToString();
    }
}
