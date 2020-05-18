using MyFolk.Building;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace MyFolk
{
	/// <summary>
	/// A place for all data, e.g. current character selected
	/// </summary>
	[CreateAssetMenu(fileName = "New GlobalsDataObject", menuName = "Globals/Data Object")]
	[System.Serializable]
	public class GlobalsDataObject : ScriptableObject
	{
		private static GlobalsDataObject _ins;
		public static GlobalsDataObject instance
		{
			get
			{
				if (!_ins)
					_ins = Resources.FindObjectsOfTypeAll<GlobalsDataObject>().FirstOrDefault();
				if (!_ins)
					_ins = CreateDefaultGameState();
				return _ins;
			}
		}

		private static GlobalsDataObject CreateDefaultGameState()
		{
			GlobalsDataObject gdo = CreateInstance<GlobalsDataObject>();
			gdo.hideFlags = HideFlags.HideAndDontSave;
			return gdo;
		}

		public CharacterData currentlySelectedCharacterData;
		public Vector3 worldClickPoint;
		public int totalCharactersInstantiated;

		public int interactableItemClickedEventsCount;

		[Header("Needs")]
		public float hungerDecreaseRate;
		public float funDecreaseRate;
		public float socialDecreaseRate;
		public float hygieneDecreaseRate;
		public float energyDecreaseRate;
		public float bladderDecreaseRate;
		public float fulfillmentDecreaseRate;
		public float healthDecreaseRate;
		public float comfortDecreaseRate;

		[Header("Scriptable objects")]
		public Interaction pickUpInteraction;
		public Interaction putDownInteraction;


		[Header("Build mode")]
		public GameObject buildTooltipPreviewPrefab;
		public StraightWallPath straightWallPathPrefab;
		public Material builtWallMaterial;
		public Material ghostWallMaterial;

	}
}