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
			EventCallbacks.EventSystem.Current.FireEvent(new EventCallbacks.CharacterSelectedEventInfo(currentlySelectedCharacter.character, cs.character));
			currentlySelectedCharacter.UpdateHighlight();
			currentlySelectedCharacter = cs;
			currentlySelectedCharacter.UpdateHighlight();
		}
	}
}
