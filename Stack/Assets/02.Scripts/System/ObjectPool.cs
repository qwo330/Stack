using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.InGameManager;

public enum ObjectType
{
    Enemy,
    ManaStone,
    Bullet,
    EnemyBullet,
    HitEffect,
}

namespace InGameSystem.ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        public int MaxEnemyCount = 30;
        public int MaxManaStoneCount = 20;//180;
        public int MaxBulletCount = 15;

        Queue<GameObject> enemys = new Queue<GameObject>();
        Queue<GameObject> manaStones = new Queue<GameObject>();
        Queue<GameObject> bullets = new Queue<GameObject>();
        Queue<GameObject> enemyBullets = new Queue<GameObject>();
        Queue<GameObject> hitEffects = new Queue<GameObject>();

        private void Start()
        {
            createBullets();
            createManaStones();
            createEffects();
        }

        public void Init(int level)
        {
            ClearEnemys();
            CreateEnemys(level);

            SetStage(level);
        }

        public void SetStage(int level)
        {
            switch (level)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        public void CreateEnemys(int level)
        {
            for (int i = 0; i < MaxEnemyCount; i++)
            {
                int rand = Random.Range(0, level + 1);
                GameObject enemy = Instantiate(InGameManager.Instance.EnemyPrefabs[rand], transform);
                enemy.SetActive(false);
                enemys.Enqueue(enemy);
            }
        }

        void createManaStones()
        {
            for (int i = 0; i < MaxManaStoneCount; i++)
            {
                GameObject manaStone = Instantiate(InGameManager.Instance.ManaStonePrefab, transform);
                manaStone.SetActive(false);
                manaStones.Enqueue(manaStone);
            }
        }

        public void ClearEnemys()
        {
            enemys.Clear();
        }

        void createBullets()
        {
            for (int i = 0; i < MaxBulletCount; i++)
            {
                GameObject bullet = Instantiate(InGameManager.Instance.BulletPrefab, transform);
                bullet.SetActive(false);
                bullets.Enqueue(bullet);
            }

            for (int i = 0; i < (MaxBulletCount * 3); i++)
            {
                GameObject bullet = Instantiate(InGameManager.Instance.EnemyBulletPrefab, transform);
                bullet.SetActive(false);
                enemyBullets.Enqueue(bullet);
            }
        }

        void createEffects()
        {
            for (int i = 0; i < MaxEnemyCount; i++)
            {
                GameObject effect = Instantiate(InGameManager.Instance.HitEffectPrefab, transform);
                effect.SetActive(false);
                hitEffects.Enqueue(effect);
            }
        }

        public GameObject GetObject(ObjectType type)
        {
            Queue<GameObject> que = null;

            switch(type)
            {
                case ObjectType.Enemy:
                    que = enemys;
                    break;
                case ObjectType.ManaStone:
                    que = manaStones;
                    break;
                case ObjectType.Bullet:
                    que = bullets;
                    break;
                case ObjectType.EnemyBullet:
                    que = enemyBullets;
                    break;
                case ObjectType.HitEffect:
                    que = hitEffects;
                    break;
            }

            GameObject obj = que.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        public void ReturnObject(GameObject obj, ObjectType type)
        {
            Queue<GameObject> que = null;

            switch (type)
            {
                case ObjectType.Enemy:
                    que = enemys;
                    break;
                case ObjectType.ManaStone:
                    que = manaStones;
                    break;
                case ObjectType.Bullet:
                    que = bullets;
                    break;
                case ObjectType.EnemyBullet:
                    que = enemyBullets;
                    break;
                case ObjectType.HitEffect:
                    que = hitEffects;
                    break;
            }

            obj.SetActive(false);
            que.Enqueue(obj);
        }

        void StageSetting(int level)
        {
            // todo : 몬스터 리스트
        }
    }
}