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

    [SerializeField]
    Button btnPause;

    [SerializeField]
    Slider skillSlider;
    #endregion

    float skillSliderValue;

    void Update()
    {
        if (skillSliderValue != 0 && Input.GetMouseButtonUp(0))
        {
            if (skillSliderValue == 1)
                Skill_3();
            else if (0.6f <= skillSliderValue && skillSliderValue < 1)
                Skill_2();
            else if (0.3f <= skillSliderValue && skillSliderValue < 0.6f)
                Skill_1();

            skillSlider.value = 0;
            skillSliderValue = 0;
        }
    }

    public void StartStage(int level)
    {
        // todo : level에 따른 plattime 세팅?
        ShowPlayTime(Defines.PLAY_TIME);
        ShowCube(0);
        ShowPlayerLife(1);
    }

    public void ShowPlayerLife(float ratio)
    {
        imgLife.fillAmount = ratio;
    }

    public void ShowPlayTime(float time)
    {
        txtTime.text = time.ToString("N2");
    }

    public void ShowCube(int count)
    {
        txtCube.text = count.ToString();
    }

    public void OnClickPause()
    {
        GameState state = InGameManager.Instance.CurrentState == GameState.Play ? GameState.Stop : GameState.Play;
        InGameManager.Instance.SetGameState(state);

        // todo : pause 팝업
    }

    public void OnSlideSkill()
    {
        skillSliderValue = skillSlider.value;
    }

    void Skill_1()
    {
        Debug.Log("Call Skill 1");
        InGameManager.Instance.player.Skill_1();
    }

    void Skill_2()
    {
        Debug.Log("Call Skill 2");
        InGameManager.Instance.player.Skill_2();
    }

    void Skill_3()
    {
        Debug.Log("Call Skill 3");
        InGameManager.Instance.player.Skill_3();
    }
}
