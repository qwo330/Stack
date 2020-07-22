using UnityEngine.Events;

public static class Defines
{
    #region Enemy
    //public static float ENEMYSPEED = 1.5f;
    #endregion

    #region System
    public const int PLAY_TIME = 180;
    public const float SPAWN_INTERVAL = 0.8f;
    //public const float WAVE_INTERVAL = 3f;

    public const int Screen_Width = 6;
    public const int Screen_Height = 10;

    #endregion

    #region Text Keys
    public const string key_HitEffect = "HitEffect";
    public const string key_Bullet = "Bullet";
    public const string key_PlayerBullet = "PlayerBullet";
    public const string key_EnemyBullet = "EnemyBullet";
    public const string key_Ground = "Ground";
    public const string key_ManaCube = "ManaCube";
    public const string key_Boundary = "Boundary";

    public const string key_Player = "Player";
    public const string key_Enemy = "Enemy";
    public const string key_Zombie = "Zombie";
    public const string key_Ghost = "Ghost";
    public const string key_Dragon = "Dragon";

    public const string key_RunR = "RunR";
    public const string key_RunL = "RunL";
    public const string key_Jump = "Jump";

    #endregion
}

public class Event : UnityEvent { }
public class Event<T> : UnityEvent<T> { }