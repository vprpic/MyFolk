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
	}
}