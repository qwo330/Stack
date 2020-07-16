using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : BaseSkill
{
    float offset = 4f;

    protected override IEnumerator CO_Action(GameObject skillObject)
    {
        skillObject.GetComponent<SkillObject>().Power = Power;

        Transform trans = skillObject.transform;
        trans.position = transform.position + Vector3.up * offset;

        yield return new WaitForSeconds(0.5f);
        ObjectPool.Get.ReturnObject(skillObject);
    }
}
