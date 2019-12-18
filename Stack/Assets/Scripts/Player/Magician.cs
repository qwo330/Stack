using UnityEngine;

public class Magician : Player
{
    protected override void SetPlayer()
    {
        maxHP = 300;
        hp = maxHP;
    }

    public override void Skill1()
    {
        Debug.Log("Skill 1");
    }

    public override void Skill2()
    {
        Debug.Log("Skill 2");
    }

    public override void Skill3()
    {
        Debug.Log("Skill 3");
    }
}
