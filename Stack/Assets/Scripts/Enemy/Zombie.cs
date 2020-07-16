using System.Collections;
using UnityEngine;

public class Zombie : BaseEnemy
{
    public override void SetEnemy(int level)
    {
        maxHP = 5 + level;
        hp = maxHP;
        moveSpeed = 5f;
    }
}