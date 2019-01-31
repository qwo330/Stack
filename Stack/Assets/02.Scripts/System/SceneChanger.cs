using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers.SceneChanger
{
    public enum SceneNumber
    {
        Title,
        Robby,
        SelectStage,
        InGame,
    }

    public enum PlayerType
    {
        Princess,

    }

    public class SceneChanger : Singleton<SceneChanger>
    {
        [SerializeField]
        SceneNumber currenScene;

        public int StageLevel;


        float fadeTime = 1f, waitTime = 1f;
        Color fadeColor;
        SpriteRenderer fadeObject;
        WaitForEndOfFrame frameDelay = new WaitForEndOfFrame();

        public void Init()
        {
            currenScene = SceneNumber.Title;
        }

        public void ChanageScene(SceneNumber nextScene)
        {
            //StartCoroutine(FadeOut());
            SceneManager.LoadScene(nextScene.ToString());
            //StartCoroutine(FadeIn());
        }

        IEnumerator FadeOut() // 시작 // 점점 검게
        {
            //UIPresenter.Instance.UICanvas.SetActive(false);
            //UIPresenter.Instance.UIRoot.SetActive(false);

            float elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                yield return frameDelay;
                elapsedTime += Time.deltaTime;
                fadeColor.a = Mathf.Clamp01(elapsedTime / fadeTime);
                fadeObject.color = fadeColor;
            }
        }

        IEnumerator FadeIn() // 끝 // 점점 투명하게
        {
            float elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                yield return frameDelay;
                elapsedTime += Time.deltaTime;
                fadeColor.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
                fadeObject.color = fadeColor;
            }

            //UIPresenter.Instance.UICanvas.SetActive(true);
            //UIPresenter.Instance.UIRoot.SetActive(true);
        }
    }
}