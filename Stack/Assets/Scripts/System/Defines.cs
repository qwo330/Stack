﻿using UnityEngine.Events;

public static class Defines
{
    #region Player
    public static float JUMP_SPEED = 8f;
    public static float MOVE_SPEED = 6.5f; 
    public static float ATTACK_INTERVAL = 0.15f;
    public static int MAX_BULLET_COUNT = 50;
    #endregion

    #region Enemy
    //public static float ENEMYSPEED = 1.5f;
    #endregion

    #region System
    public static int PLAY_TIME = 180;
    public static int MAX_MANASTONE = 200;
    public static float SPAWN_INTERVAL = 2f;
    public static float WAVE_INTERVAL = 10f;

    public static int _Width = 9;
    public static int _Height = 20;

    #endregion

    #region Text Keys
    public const string key_HitEffect = "HitEffect";
    public const string key_Bullet = "Bullet";
    public const string key_PlayerBullet = "PlayerBullet";
    public const string key_EnemyBullet = "EnemyBullet";
    public const string key_Ground = "Ground";
    public const string key_ManaStone = "ManaStone";
    public const string key_Boundary = "Boundary";

    public const string key_Player = "Player";
    public const string key_Enemy = "Enemy";
    public const string key_Zombie = "Zombie";

    public const string key_RunR = "RunR";
    public const string key_RunL = "RunL";
    public const string key_Jump = "Jump";

    #endregion
}

public class Event<T> : UnityEvent<T> { }