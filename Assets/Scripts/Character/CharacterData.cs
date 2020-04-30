using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyFolk
{
	[CreateAssetMenu(fileName = "new Character Data", menuName = "Character Data")]
	[System.Serializable]
	public class CharacterData : ScriptableObject
	{
		private Character owner;

		private void Init()
		{
			id = GlobalsDataObject.instance.totalCharactersInstantiated;
			GlobalsDataObject.instance.totalCharactersInstantiated++;
			Debug.Log("Created a character! Id: " + id);
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

		//public CharacterSkill Hunger = new CharacterSkill("Hunger");
		//public CharacterSkill Energy = new CharacterSkill("Energy");
		//public CharacterSkill Fun = new CharacterSkill("Fun");
		//public CharacterSkill Social = new CharacterSkill("Social");
		//public CharacterSkill Hygiene = new CharacterSkill("Hygiene");
		//public CharacterSkill Bladder = new CharacterSkill("Bladder");
		//public CharacterSkill Fulfillment = new CharacterSkill("Fulfillment");
		//public CharacterSkill Comfort = new CharacterSkill("Comfort");

		public Sprite imageUI;
		public Need hunger = new Need(Need.NeedType.Hunger, 0, 100);
		public Need energy = new Need(Need.NeedType.Energy, 0, 100);
		public Need fun = new Need(Need.NeedType.Fun, 0, 100);
		public Need social = new Need(Need.NeedType.Social, 0, 100);
		public Need hygiene = new Need(Need.NeedType.Hygiene, 0, 100);
		public Need bladder = new Need(Need.NeedType.Bladder, 0, 100);
		public Need fulfillment = new Need(Need.NeedType.Fulfillment, 0, 100);
		public Need comfort = new Need(Need.NeedType.Comfort, 0, 100);

		public Need health = new Need(Need.NeedType.Health, 0, 100);

		public Vector3 currentWorldPosition;
		public string characterFirstName;
		public string characterLastName;

		public bool isSelected;
	}
}