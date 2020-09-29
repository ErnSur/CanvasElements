using UnityEngine;
using UnityEngine.UI;

namespace QuickEye.Utility.CharacterCreation
{
    [System.Serializable]
    public class SkillGalleryItems : PoolContainer<SkillGalleryItem> { }
    public class SkillGalleryItem : CanvasElement<(CharacterCreationEvents, Skill)>
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Sprite _noSkillIcon;

        public override void Initialize((CharacterCreationEvents, Skill) context)
        {
            base.Initialize(context);
            var skill = context.Item2;
            _icon.sprite = skill != null ? skill.icon : _noSkillIcon;
        }
    }
}
