using UnityEngine;

namespace QuickEye.Utility.CharacterCreation
{

    public class CharacterGallery : CanvasElement<(CharacterCreationEvents events, CharacterTemplate[] fighters)>
    {
        [SerializeField]
        private CharacterGalleryItems _galleryItems;

        public override void Initialize((CharacterCreationEvents events, CharacterTemplate[] fighters) c)
        {
            base.Initialize(c);
            
            foreach (var item in c.fighters)
            {
                _galleryItems.AddNew().Initialize((c.events, item));
            }
        }
    }
}
