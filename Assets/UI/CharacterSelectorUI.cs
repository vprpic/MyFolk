using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyFolk.FlexibleUI;
using UnityEngine.UI;

namespace MyFolk.UI
{
    public class CharacterSelectorUI : FlexibleUIButton
    {
        [HideInInspector]
        public FamilyPanelUI menuParent;
        [HideInInspector]
        public Character character;
        public Image characterImage;

        public void Init(FamilyPanelUI parent, Character character)
        {
            this.menuParent = parent;
            this.character = character;
            if(characterImage != null)
            {
                characterImage.sprite = character.data.imageUI;
            }
            UpdateHighlight();
        }

        public void OnCharacterSelectorClicked()
        {
            menuParent.OnCharacterSelectorClicked(this);
        }

        public void UpdateHighlight()
        {
            if (character.isSelected)
            {
                this.buttonType = ButtonType.Highlight;
            }
            else
            {
                this.buttonType = ButtonType.Default;
            }
            OnSkinUI();
        }
    }
}