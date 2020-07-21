using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ingame : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    GameObject PauseWindow;

    [SerializeField]
    Text GuildeText;

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
    WaitForSeconds showTextTime = new WaitForSeconds(2f);

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

    public void OnSlideSkill()
    {
        skillSliderValue = skillSlider.value;
    }

    public void InitUI(int level)
    {
        ShowPlayTime(Defines.PLAY_TIME);
        ShowCube(0);
        ShowPlayerLife(1);

        if (level % 4 == 0) // Boss Stage
        {
            ShowText("Boss has appeared!");
        }
    }

    public void ShowPlayTime(float time)
    {
        txtTime.text = time.ToString("N2");
    }

    public void ShowCube(int count)
    {
        txtCube.text = count.ToString();
    }

    public void ShowPlayerLife(float ratio)
    {
        imgLife.fillAmount = ratio;
    }

    public void OnClickPause()
    {
        GameState state = GameState.Stop;
        InGameManager.Instance.SetGameState(state);
        PauseWindow.SetActive(true);
    }

    public void OnClickContinue()
    {
        GameState state = GameState.Play;
        InGameManager.Instance.SetGameState(state);
        PauseWindow.SetActive(false);
    }

    public void OnClickRestart()
    {
        GameState state = GameState.Ready;
        InGameManager.Instance.StartStage();
    }

    public void OnClickQuit()
    {
        InGameManager.Instance.QuitGame();
    }

    public void ShowText(string text)
    {
        StartCoroutine(CO_ShowText(text));
    }

    IEnumerator CO_ShowText(string text)
    {
        GuildeText.text = text;
        GuildeText.gameObject.SetActive(true);

        yield return showTextTime;
        GuildeText.gameObject.SetActive(false);
    }

    void Skill_1()
    {
        InGameManager.Instance.player.Skill_1();
    }

    void Skill_2()
    {
        InGameManager.Instance.player.Skill_2();
    }

    void Skill_3()
    {
        InGameManager.Instance.player.Skill_3();
    }
}
