using EventCallbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	public class Globals : MonoBehaviour
	{
		public static Globals ins;

		[SerializeField]
		public GlobalsDataObject data = null;
		//public GlobalsDataObject Data { get { return data; } }
		//public CharactersRuntimeSet allCharacters;
		public List<Character> allCharacters;
		public Character currentlySelectedCharacter;


		//TODO: remove
		public MyFolk.UI.FamilyPanelUI familyPanel;

		private void Awake()
		{
			if (ins == null)
			{
				ins = this;
				//SetSelectedCharacter();
			}
			else
			{
				Debug.LogWarning("Multiple Globals instances");
				Destroy(this);
			}
			EventSystem.Current.RegisterListener<CharacterSelectedEventInfo>(OnCharacterSelected);
			//SetSelectedCharacter();
		}

		private void Start()
		{
			if (GetSelectedCharacter() == null)
			{
				SetSelectedCharacter();
			}
		}

		private void SetSelectedCharacter()
		{
			Character c = null;
			if (allCharacters.Count == 0)
			{
				allCharacters = FindObjectsOfType<Character>().ToList();
				if (allCharacters.Count == 0)
				{
					Debug.LogWarning("No characters available to set selected.");
					return;
				}
			}
			bool foundOneSelected = false;
			for (int i = 0; i < allCharacters.Count; i++)
			{
				c = allCharacters[i];
				familyPanel.AddCharacterSelector(c);
				if (c.data.isSelected)
				{
					if (!foundOneSelected)
					{
						SetSelectedCharacter(FindCharacterFromId(c.data.id));
						foundOneSelected = true;
					}
					else
					{
						c.data.isSelected = false;
					}
				}
			}
			if (!foundOneSelected)
			{
				c = allCharacters[0];
				c.data.isSelected = true;
				SetSelectedCharacter(c);
			}
			if (c != null)
				EventSystem.Current.FireEvent(new CharacterSelectedEventInfo(null, c));
		}

		private Character FindCharacterFromId(int characterId)
		{
			if (allCharacters.Count == 0)
			{
				allCharacters = FindObjectsOfType<Character>().ToList();
			}

			foreach (Character c in allCharacters)
			{
				if (c.data.id == characterId)
					return c;
			}
			return null;
		}

		public Character GetSelectedCharacter() => currentlySelectedCharacter;
		public void SetSelectedCharacter(Character character)
		{
			currentlySelectedCharacter = character;
			data.currentlySelectedCharacterData = character.data;
			character.data.isSelected = true;
		}

		public Vector3 GetLastWorldClickPoint() => data.worldClickPoint;
		public void SetLastWorldClickPoint(Vector3 clickPoint)
		{
			data.worldClickPoint = clickPoint;
		}

		public void OnCharacterSelected(CharacterSelectedEventInfo eventInfo)
		{
			if(eventInfo.oldCharacter != null)
				eventInfo.oldCharacter.data.isSelected = false;
			SetSelectedCharacter(eventInfo.newCharacter);
		}
	}
}