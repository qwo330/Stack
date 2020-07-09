using UnityEngine;
using System.Collections;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    protected int hp, maxHP;

    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    protected float attackInterval;

    Coroutine attackRoutine;

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(0f, -moveSpeed * Time.deltaTime, 0f);
    }

    public void SetEnemy()
    {
        hp = maxHP;
        attackRoutine = StartCoroutine(CO_Attack());
    }

    public virtual IEnumerator CO_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);

            GameObject bullet = ObjectPool.Get.GetObject(Defines.key_HitEffect);
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0f);
        }
    }


    public void Damage(int power = 1)
    { 
        hp -= power;
        if (hp == 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        StopCoroutine(attackRoutine);
        ShowDeadEffect();
        DropManaStone();
        ObjectPool.Get.ReturnObject(gameObject);
    }

    void DropManaStone()
    {
        GameObject manaCube = ObjectPool.Get.GetObject(Defines.key_ManaCube);
        manaCube.transform.position = transform.position;
        InGameManager.Instance.EnemyDeadEvent.Invoke();
    }

    void ShowDeadEffect()
    {
        HitEffect effect = ObjectPool.Get.GetObject(Defines.key_HitEffect).GetComponent<HitEffect>();
        effect.transform.position = transform.position + new Vector3(0, 0, 1f);
        effect.ShowEffect();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Defines.key_Player) || other.gameObject.CompareTag(Defines.key_PlayerBullet))
        {
            Damage();
        }

        if (other.CompareTag(Defines.key_Ground))
        {
            ObjectPool.Get.ReturnObject(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Defines.key_Boundary))
        {
            ObjectPool.Get.ReturnObject(gameObject);
        }
    }
}