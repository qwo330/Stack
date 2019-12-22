using UnityEngine.Events;

public static class Defines
{
    #region Player
    public const float Jump_Height = 3f;
    public const float JUMP_SPEED = 8f;
    public const float MOVE_SPEED = 6.5f; 
    public const float ATTACK_INTERVAL = 0.15f;
    public const int MAX_BULLET_COUNT = 50;
    #endregion

    #region Enemy
    //public static float ENEMYSPEED = 1.5f;
    #endregion

    #region System
    public const int PLAY_TIME = 180;
    public const int MAX_MANASTONE = 200;
    public const float SPAWN_INTERVAL = 2f;
    public const float WAVE_INTERVAL = 10f;

    public const int Screen_Width = 9;
    public const int Screen_Height = 16;

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