using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyFolk
{
	[CreateAssetMenu(fileName = "new Character Data", menuName = "Character Data")]
	[System.Serializable]
	public class CharacterData : ScriptableObject
	{
		private void Init()
		{
			id = GlobalsDataObject.instance.totalCharactersInstantiated;
			GlobalsDataObject.instance.totalCharactersInstantiated++;
			Debug.Log("Created a character! Id: " + id);

			Hunger = new CharacterStat();
			Energy = new CharacterStat();
			Fun = new CharacterStat();
			Social = new CharacterStat();
			Comfort = new CharacterStat();
			Health = new CharacterStat();

			Hunger.charName = "Hunger";
			Energy.charName = "Energy";
			Fun.charName = "Fun";
			Social.charName = "Social";
			Comfort.charName = "Comfort";
			Health.charName = "Health";
		}

		private int _id = -1;
		public int id
		{
			get
			{
				if (_id < 0)
				{
					this.Init();
				}
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		public CharacterStat Hunger = new CharacterStat("Hunger");
		public CharacterStat Energy = new CharacterStat("Energy");
		public CharacterStat Fun = new CharacterStat("Fun");
		public CharacterStat Social = new CharacterStat("Social");
		public CharacterStat Comfort = new CharacterStat("Comfort");
		public CharacterStat Health = new CharacterStat("Health");

		public Vector3 currentWorldPosition;
		public string characterFirstName;
		public string characterLastName;

		public bool isSelected;
		//public bool isActive;
	}
}