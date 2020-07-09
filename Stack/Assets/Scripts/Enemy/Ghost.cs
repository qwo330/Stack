using System.Collections;
using UnityEngine;

public class Ghost : BaseEnemy, IAttackable
{
    Coroutine attackRoutine;

    public override void SetEnemy()
    {
        maxHP = 3;
        hp = maxHP;
        moveSpeed = 1.5f;
        attackInterval = 4f;

        attackRoutine = StartCoroutine(CO_Attack());
    }

    public IEnumerator CO_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);

            GameObject bullet = ObjectPool.Get.GetObject(Defines.key_HitEffect);
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0f);
        }
    }

    protected override void Dead()
    {
        base.Dead();
        StopCoroutine(attackRoutine);
    }
}