using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : BaseSkill
{
    float moveSpeed = 80f;
    float offset = 0.5f;
    float playTime = 2f;

    protected override IEnumerator CO_Action(GameObject skillObject)
    {
        skillObject.GetComponent<SkillObject>().Power = Power;

        Transform trans = skillObject.transform;
        trans.position = transform.position + Vector3.up * offset;

        float waitTime = 0;
        while (waitTime < playTime)
        {
            trans.Translate(0f, moveSpeed * Time.deltaTime, 0f);
            yield return new WaitForSeconds(0.1f);
            waitTime += 0.1f;
        }

        ObjectPool.Get.ReturnObject(skillObject);
    }
}
