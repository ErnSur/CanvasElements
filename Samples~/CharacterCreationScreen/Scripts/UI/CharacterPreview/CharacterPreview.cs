using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace QuickEye.Utility.CharacterCreation
{
    public class CharacterPreview : CanvasElement<CharacterCreationEvents>
    {
        [SerializeField]
        private Image _avatar;

        [SerializeField]
        private GameObject _statsContainer;

        [SerializeField]
        private Text _hpLabel, _nameLabel;

        [SerializeField]
        private Sprite _nothingSelectedAvatar;

        public override void Initialize(CharacterCreationEvents eventHub)
        {
            base.Initialize(eventHub);
            Refresh(null);
            eventHub.CharacterSelectedEvent += Refresh;
        }

        private void Refresh(CharacterTemplate selectedFighter)
        {
            UpdateCharacterDetails(selectedFighter);
        }

        private void UpdateCharacterDetails(CharacterTemplate character)
        {
            if (character == null)
            {
                _statsContainer.SetActive(false);

                _avatar.sprite = _nothingSelectedAvatar;
                _nameLabel.text = "";
            }
            else
            {
                _statsContainer.SetActive(true);
                _hpLabel.text = $"HP: {character.hp}";

                _avatar.sprite = character.avatar;
                _nameLabel.text = character.name;
            }
        }
    }
}
