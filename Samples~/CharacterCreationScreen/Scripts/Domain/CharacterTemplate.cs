using UnityEngine;

namespace QuickEye.Utility.CharacterCreation
{
    [System.Serializable]
    public class CharacterTemplate
    {
        public Sprite avatar;
        public Skill[] skills;
        public int hp;
        public string name;
    }
}
