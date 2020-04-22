using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Character Data", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
	private void Awake() //OnCreate
	{
		Id = Globals.ins.data.totalCharactersInstantiated;
		Globals.ins.data.totalCharactersInstantiated++;
		Debug.Log("Created a character! Id: " + Id);

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

	public int Id;

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
	public bool isActive;
}
