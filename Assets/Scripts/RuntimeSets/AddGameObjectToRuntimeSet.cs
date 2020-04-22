using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGameObjectToRuntimeSet : MonoBehaviour
{
	public GameObjectRuntimeSet gameObjectRuntimeSet;

	private void OnEnable()
	{
		gameObjectRuntimeSet.Add(this.gameObject);
	}
	private void OnDisable()
	{
		gameObjectRuntimeSet.Remove(this.gameObject);
	}
}
