using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StraightWallNode : WallNode
{
	[SerializeField]
	public StraightWallNode node1;
	[SerializeField]
	public StraightWallNode node2;

	
}
