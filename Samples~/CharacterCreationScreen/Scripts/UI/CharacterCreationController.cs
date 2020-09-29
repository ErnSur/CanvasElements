using System;
using UnityEngine;

namespace QuickEye.Utility.CharacterCreation
{
    public class CharacterCreationEvents
    {
        public Action<CharacterTemplate> CharacterSelectedEvent;
        public Action<CharacterTemplate> RemoveFromTeam;
    }

    public class CharacterCreationController : MonoBehaviour
    {
        private CharacterCreationEvents events = new CharacterCreationEvents();

        [SerializeField]
        private CharacterTemplate[] _selectableFighters;

        private void Awake()
        {
            GetComponentInChildren<CharacterGallery>()
                .Initialize((events, _selectableFighters));

            GetComponentInChildren<SkillGallery>()
                .Initialize(events);

            GetComponentInChildren<CharacterPreview>()
                .Initialize(events);
        }
    }
}
