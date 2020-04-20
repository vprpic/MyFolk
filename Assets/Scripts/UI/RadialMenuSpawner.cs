using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour
{
	//TODO: remove
	public static RadialMenuSpawner ins;
	public RadialMenu menuPrefab;

	private void Awake()
	{
		ins = this;
	}

	public void SpawnMenu(Interactable obj)
	{
		RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
		newMenu.transform.SetParent(transform, false);
		newMenu.transform.position = Input.mousePosition;
		newMenu.SpawnButtons(obj);
	}
}
