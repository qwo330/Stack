using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Enemy,
    ManaStone,
    Bullet,
    EnemyBullet,
    HitEffect,
}


public class ObjectPool : MonoBehaviour
{
    const string path = "Prefabs/";
    public int Max_Enemy_Count = 30;
    public int Max_ManaStone_Count = 20;//180; // c 9 * r 20
    public int Max_Bullet_Count = 30;

    public static ObjectPool Get { get; private set; }

    Dictionary<string, Stack<GameObject>> Pools = new Dictionary<string, Stack<GameObject>>();

    //Queue<GameObject> enemys = new Queue<GameObject>();
    //Queue<GameObject> manaStones = new Queue<GameObject>();
    //Queue<GameObject> bullets = new Queue<GameObject>();
    //Queue<GameObject> enemyBullets = new Queue<GameObject>();
    //Queue<GameObject> hitEffects = new Queue<GameObject>();

    //private void Start()
    //{
    //    createBullets();
    //    createManaStones();
    //    createEffects();
    //}

    void Awake()
    {
        Get = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Init(int level)
    {
        CreatePool(ObjectType.ManaStone.ToString(), Max_ManaStone_Count);
        CreatePool(ObjectType.Bullet.ToString(), Max_Bullet_Count);
        CreatePool(ObjectType.EnemyBullet.ToString(), Max_Bullet_Count);
        CreatePool(ObjectType.HitEffect.ToString(), Max_Enemy_Count);
        //CreatePool(ObjectType.Enemy.ToString(), Max_Enemy_Count);

        //ClearEnemys();
        //CreateEnemys(level);

        //SetStage(level);
    }

    public void CreatePool(string name, int count = 32)
    {
        Stack<GameObject> stack;

        if (Pools.ContainsKey(name))
            stack = Pools[name];
        else
        {
            stack = new Stack<GameObject>();
            Pools.Add(name, stack);
        }

        GameObject prefab = Resources.Load<GameObject>(path + name);
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.name = name;
            obj.SetActive(false);
            stack.Push(obj);
        }
    }

    public GameObject GetObject(string name)
    {
        if (Pools.ContainsKey(name) == false)
            CreatePool(name);

        var stack = Pools[name];

        if (stack.Count == 0)
            CreatePool(name);

        GameObject obj = stack.Pop();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        string name = obj.name;
        var stack = Pools[name];
        stack.Push(obj);
    }

    //public void CreateEnemys(int level)
    //{
    //    for (int i = 0; i < Max_Enemy_Count; i++)
    //    {
    //        int rand = Random.Range(0, level + 1);
    //        GameObject enemy = Instantiate(InGameManager.Instance.EnemyPrefabs[rand], transform);
    //        enemy.SetActive(false);
    //        enemys.Enqueue(enemy);
    //    }
    //}

    //void createManaStones()
    //{
    //    for (int i = 0; i < Max_ManaStone_Count; i++)
    //    {
    //        GameObject manaStone = Instantiate(InGameManager.Instance.ManaStonePrefab, transform);
    //        manaStone.SetActive(false);
    //        manaStones.Enqueue(manaStone);
    //    }
    //}

    //public void ClearEnemys()
    //{
    //    enemys.Clear();
    //}

    //void createBullets()
    //{
    //    for (int i = 0; i < Max_Bullet_Count; i++)
    //    {
    //        GameObject bullet = Instantiate(InGameManager.Instance.BulletPrefab, transform);
    //        bullet.SetActive(false);
    //        bullets.Enqueue(bullet);
    //    }

    //    for (int i = 0; i < (Max_Bullet_Count * 3); i++)
    //    {
    //        GameObject bullet = Instantiate(InGameManager.Instance.EnemyBulletPrefab, transform);
    //        bullet.SetActive(false);
    //        enemyBullets.Enqueue(bullet);
    //    }
    //}

    //void createEffects()
    //{
    //    for (int i = 0; i < Max_Enemy_Count; i++)
    //    {
    //        GameObject effect = Instantiate(InGameManager.Instance.HitEffectPrefab, transform);
    //        effect.SetActive(false);
    //        hitEffects.Enqueue(effect);
    //    }
    //}
}
