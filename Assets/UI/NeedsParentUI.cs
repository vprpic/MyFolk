using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;
using UnityEngine.UI;

namespace MyFolk.FlexibleUI
{
	public class NeedsParentUI : FlexibleUIPanel
	{
		Dictionary<Need.NeedType,NeedUI> needs;
		List<Need> dirtyNeeds; //needs that need updating

		public Character currentCharacter;

		private void Start()
		{
			currentCharacter = Globals.ins.currentlySelectedCharacter;
			needs = new Dictionary<Need.NeedType, NeedUI>();
			foreach (NeedUI item in FindObjectsOfType<NeedUI>())
			{
				needs.Add(item.type, item);
			}
			dirtyNeeds = new List<Need>();

			CurrentCharacterNeedChangedEvent.RegisterListener(OnNeedUpdated);
			CharacterSelectedEvent.RegisterListener(OnCurrentCharacterChanged);
			//EventSystem.Current.RegisterListener<CurrentCharacterNeedChangedEvent>(OnNeedUpdated);
			//EventSystem.Current.RegisterListener<CharacterSelectedEvent>(OnCurrentCharacterChanged);
		}
		
		public override void Update()
		{
#if UNITY_EDITOR
			OnSkinUI();
#endif
			if (dirtyNeeds != null && dirtyNeeds.Count > 0)
			{
				for (int i = dirtyNeeds.Count - 1; i >= 0; i--)
				{
					NeedUI ui = null;
					if (needs.TryGetValue(dirtyNeeds[i].type, out ui))
					{
						ui.SetNewNeed(dirtyNeeds[i]);
					}
					dirtyNeeds.RemoveAt(i);
				}
			}
		}

		public void OnCurrentCharacterChanged(CharacterSelectedEvent eventInfo)
		{
			this.currentCharacter = eventInfo.newCharacter;
			if (currentCharacter != null)
			{
				dirtyNeeds.Add(currentCharacter.data.hunger);
				dirtyNeeds.Add(currentCharacter.data.energy);
				dirtyNeeds.Add(currentCharacter.data.fun);
				dirtyNeeds.Add(currentCharacter.data.social);
				dirtyNeeds.Add(currentCharacter.data.hygiene);
				dirtyNeeds.Add(currentCharacter.data.bladder);
				dirtyNeeds.Add(currentCharacter.data.fulfillment);
				dirtyNeeds.Add(currentCharacter.data.comfort);
				dirtyNeeds.Add(currentCharacter.data.health);
			}
			//UpdateNeedsUI();
		}

		public void UpdateNeedsUI()
		{
			foreach (var item in needs)
			{
				item.Value.UpdateNeedUI();
			}
		}

		public void OnNeedUpdated(CurrentCharacterNeedChangedEvent eventInfo)
		{
			dirtyNeeds.Add(eventInfo.needChanged);
		}
	}
}
