using UnityEngine;
using InGameSystem.ObjectPool;

namespace Objects.Player
{
    public class Magician : Player
    {
        protected override void setPlayer()
        {
            maxHP = 300;
            HP = maxHP;
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
}