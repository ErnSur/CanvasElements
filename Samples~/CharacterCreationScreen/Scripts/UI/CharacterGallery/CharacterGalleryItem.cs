using UnityEngine;
using UnityEngine.UI;

namespace QuickEye.Utility.CharacterCreation
{
    [System.Serializable]
    public class CharacterGalleryItems : Container<CharacterGalleryItem> { }
    public class CharacterGalleryItem : CanvasElement<(CharacterCreationEvents events, CharacterTemplate template)>
    {
        [SerializeField]
        private Image _avatar;
        [SerializeField]
        private Button _button;

        public override void Initialize((CharacterCreationEvents events, CharacterTemplate template) c)
        {
            base.Initialize(c);

            _button.onClick.AddListener(() => c.events.CharacterSelectedEvent?.Invoke(c.template)) ;
            _avatar.sprite = c.template.avatar;
        }
    }

}
