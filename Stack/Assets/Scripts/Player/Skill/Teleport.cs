using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : BaseSkill
{
    protected override IEnumerator CO_Action(GameObject skillObject)
    {
        Player player = GetComponent<Player>();

        Vector3 curPosition = transform.position;
        Vector3 newPosition = curPosition + Vector3.up * Power;

        ShowSkillEffect(curPosition);
        yield return null;

        ShowSkillEffect(newPosition);
        transform.position = newPosition;
        //player.isjump = false;
    }

    void ShowSkillEffect(Vector3 position)
    {
        HitEffect effect = ObjectPool.Get.GetObject(Defines.key_HitEffect).GetComponent<HitEffect>();
        effect.transform.position = position + Vector3.back;
        effect.ShowEffect();
    }
}
