using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Character Data", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
	private static int IDCount;
	private void Awake()
	{
		Id = IDCount;
		IDCount++;
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
