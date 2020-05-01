using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyFolk.FlexibleUI;
using System;

namespace MyFolk.UI
{
	public class FamilyPanelUI : FlexibleUIPanel
	{
		public CharacterSelectorUI characterSelectorUIPrefab;
		[HideInInspector]
		public CharacterSelectorUI currentlySelectedCharacter;
		public List<CharacterSelectorUI> characterSelectorUIs;


		public void AddCharacterSelector(Character character)
		{
			CharacterSelectorUI cs = Instantiate(characterSelectorUIPrefab, this.transform);
			cs.Init(this, character);
			if (cs.character.isSelected)
			{
				currentlySelectedCharacter = cs;
			}
			characterSelectorUIs.Add(cs);
		}

		internal void OnCharacterSelectorClicked(CharacterSelectorUI cs)
		{
			(new EventCallbacks.CharacterSelectedEvent(currentlySelectedCharacter.character, cs.character)).FireEvent();
			currentlySelectedCharacter.UpdateHighlight();
			currentlySelectedCharacter = cs;
			currentlySelectedCharacter.UpdateHighlight();
		}
	}
}
