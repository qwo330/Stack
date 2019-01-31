using UnityEngine;
using System.Collections;
using Managers.InGameManager;

namespace Objects.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        protected float moveSpeed;
        [SerializeField]
        protected int hP, maxHP;
        protected float attackInterval;

        void FixedUpdate()
        {
            move();
        }

        void move()
        {
            transform.Translate(0f, -moveSpeed * Time.deltaTime, 0f);
        }

        public void Demage()
        {
            hP--;
            if (hP <= 0)
            {
                StartCoroutine(deadEffect());
            }
        }

        void dead()
        {
            StopCoroutine(attack());
            InGameManager.Instance.dropManaStone(transform.position);
            InGameManager.Instance.Pool.ReturnObject(gameObject, ObjectType.Enemy);
        }

        IEnumerator deadEffect()
        {
            GameObject effect = InGameManager.Instance.Pool.GetObject(ObjectType.HitEffect);
            effect.transform.position = transform.position + new Vector3(0, 0, 1f);

            yield return new WaitForSeconds(0.1f);
            InGameManager.Instance.Pool.ReturnObject(effect, ObjectType.HitEffect);

            StopCoroutine(deadEffect());
            dead();
        }

        public virtual IEnumerator attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(attackInterval);

                GameObject bullet = InGameManager.Instance.Pool.GetObject(ObjectType.EnemyBullet);
                bullet.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0f);
            }
        }

        void createManaStone()
        {
            GameObject manaStone = InGameManager.Instance.Pool.GetObject(ObjectType.EnemyBullet);
            manaStone.transform.position = transform.position;
        }

        public void ResetEnemy()
        {
            hP = maxHP;
            StartCoroutine(attack());
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerBullet"))
            {
                Demage();
            } 
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Boundary"))
            {
                InGameManager.Instance.Pool.ReturnObject(gameObject, ObjectType.Enemy);
            }
        }
    }
}