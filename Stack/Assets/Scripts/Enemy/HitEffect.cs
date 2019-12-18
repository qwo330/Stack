using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField]
    float effectTime;

    static WaitForSeconds waitSeconds = null;

    void Start()
    {
        if (waitSeconds == null)
            waitSeconds = new WaitForSeconds(effectTime);
    }

    public void ShowEffect()
    {
        StartCoroutine(CO_ShowEffect());
    }

    IEnumerator CO_ShowEffect()
    {
        yield return waitSeconds;
        ObjectPool.Get.ReturnObject(gameObject);
    }

}
