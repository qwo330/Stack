using System.Collections;
using UnityEngine;
namespace Objects.Enemy
{
    public class Ghost : BaseEnemy
    {
        void Start()
        {
            maxHP = 3;
            hP = maxHP;
            moveSpeed = 1.5f;
            attackInterval = 4f;
        }
    }
}