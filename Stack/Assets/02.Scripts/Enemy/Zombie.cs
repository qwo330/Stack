using System.Collections;
using InGameSystem.ObjectPool;
using UnityEngine;

namespace Objects.Enemy
{
    public class Zombie : BaseEnemy
    {
        void Start()
        {
            maxHP = 6;
            hP = maxHP;
            moveSpeed = 1f;
        }

        public override IEnumerator attack()
        {
            yield return null;
        }
    }
}