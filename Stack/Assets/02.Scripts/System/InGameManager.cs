using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameSystem.ObjectPool;
using InGameSystem.Camera;
using Objects.Enemy;

namespace Managers.InGameManager
{
    public class InGameManager : Singleton<InGameManager>
    {
        public UILabel TimerLabel, ManaStoneLabel;

        public GameObject[] EnemyPrefabs;
        public GameObject ManaStonePrefab;
        public GameObject BulletPrefab;
        public GameObject EnemyBulletPrefab;
        public GameObject HitEffectPrefab;

        GameObject player;
        
        public int StageLevel;

        [HideInInspector]
        public ObjectPool Pool;

        [SerializeField]
        int remainTime;
        public int ManaStone;

        public bool isGameOver { get; private set; }
        public bool isClear { get; private set; }

        public void Init(int StageLevel)
        {
            this.StageLevel = StageLevel;
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.Log("플레이어를 찾을 수 없음");
                return;
            }

            Pool = GetComponent<ObjectPool>();
            Pool.Init(StageLevel);

            CameraCtrl camera = Camera.main.GetComponent<CameraCtrl>();
            camera.Init();

            remainTime = Defines.PLAYTIME;
            ManaStone = 0;

            TimerLabel.text = remainTime.ToString();
            ManaStoneLabel.text = ManaStone.ToString();

            StartCoroutine(spawnEnemy());
            StartCoroutine(elapse());
        }

        IEnumerator elapse()
        {
            while (remainTime != 0)
            {
                yield return new WaitForSeconds(1);
                remainTime--;
                TimerLabel.text = remainTime.ToString();
            }

            GameOver();
        }

        IEnumerator spawnEnemy()
        {
            if (isGameOver) yield return null;

            while (true)
            {
                for (int i = 0; i < 20; i++)
                {
                    Vector3 spawnPos = new Vector3(Random.Range(0, 9), player.transform.position.y + 13, 0);
                    GameObject enemy = Pool.GetObject(ObjectType.Enemy);
                    enemy.transform.position = spawnPos;

                    enemy.GetComponent<BaseEnemy>().ResetEnemy();
                    yield return new WaitForSeconds(Defines.WAVEINTERVAL);
                }
            }
        }

        public void GameOver()
        {
            Debug.Log("GAME OVER!!");
            isGameOver = true;
        }

        void Clear()
        {
            Debug.Log("GAME CLEAR~@@");
            isClear = true;
        }

        public void UseManaStone(int count)
        {
            // todo : 화면 상단부터 count 수만큼의 마나 스톤을 제거(하고 스킬 사용)
            if (dic.Count < count)
            {
                Debug.Log("자원이 부족합니다.");
                return;
            }

            //width : 9, height = 20
            for (int key = (MaxHeight*10) - 1; key >= 0; key--)
            {
                if (count == 0) return;
                if (key % 10 == 9) key--;

                if (dic.ContainsKey(key))
                {
                    Debug.Log("블럭 제거! key : " + key);
                    GameObject obj;
                    dic.TryGetValue(key, out obj);
                    dic.Remove(key);
                    count--;
                    ManaStone--;

                    Pool.ReturnObject(obj, ObjectType.ManaStone);
                    obj = null;
                }
            }
        }

        public void stackManaStone(GameObject manaStone)
        {
            int key = (int)(manaStone.transform.position.x) 
                + ((int)(Mathf.Round(manaStone.transform.position.y))*10);
            dic.Add(key, manaStone);
        }

        public void dropManaStone(Vector3 enemyPos)
        {
            GameObject obj = Pool.GetObject(ObjectType.ManaStone);
            ManaStone++;
            obj.transform.position = new Vector3(Mathf.Round(enemyPos.x), enemyPos.y, 0);
        }
        /*     */
        SortedDictionary<int, GameObject> dic = new SortedDictionary<int, GameObject>();
        // 배열로??
        public int MaxHeight = 20;
    }
}