using QuickEye.Utility;
using UnityEngine;

namespace QuickEye.Utility.CharacterCreation
{
    public class SkillGallery : CanvasElement<CharacterCreationEvents>
    {
        [SerializeField]
        private SkillGalleryItems _galleryItems;

        public override void Initialize(CharacterCreationEvents eventHub)
        {
            base.Initialize(eventHub);
            eventHub.CharacterSelectedEvent += Refresh;
        }

        private void Refresh(CharacterTemplate character)
        {
            _galleryItems.Clear();

            if (character == null || character.skills == null)
                return;

            foreach (var skill in character.skills)
            {
                _galleryItems.AddNew().Initialize((Context, skill));
            }
        }
    }
}
