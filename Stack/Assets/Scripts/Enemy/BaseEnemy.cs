using UnityEngine;
using System.Collections;

public interface IAttackable
{
    IEnumerator CO_Attack();
}

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    protected int hp, maxHP;

    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    protected float attackInterval;

    //bool isDead = false;

    //void OnDisable()
    //{
    //    isDead = false;
    //}

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (InGameManager.Instance.CheckPlaying())
            transform.Translate(0f, -moveSpeed * Time.deltaTime, 0f);
    }

    public abstract void SetEnemy(int level);

    public void Hit(int power = 1)
    { 
        hp -= power;
        if (hp <= 0/* && !isDead*/)
        {
            Dead();
        }
    }

    protected virtual void Dead()
    {
        //isDead = true;
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
        if (/*!isDead &&*/ other.gameObject.CompareTag(Defines.key_PlayerBullet))
        {
            Hit();
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