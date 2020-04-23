using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		Hunger.name = "Hunger";
		Energy.name = "Energy";
		Fun.name = "Fun";
		Social.name = "Social";
		Comfort.name = "Comfort";
		Health.name = "Health";
	}

	private int _id = -1;
	public int id
	{
		get
		{
			if(_id < 0)
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

	public CharacterStat Hunger;
	public CharacterStat Energy;
	public CharacterStat Fun;
	public CharacterStat Social;
	public CharacterStat Comfort;
	public CharacterStat Health;

	public Vector3 currentWorldPosition;
	public string characterFirstName;
	public string characterLastName;

	public bool isSelected;
	//public bool isActive;
}
