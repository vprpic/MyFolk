using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour
{
	//TODO: remove
	public static RadialMenuSpawner ins;
	public RadialMenu menuPrefab;
	public Camera mainCamera;

	private void Awake()
	{
		ins = this;
		mainCamera = Camera.main;
	}

	public void SpawnMenu(Interactable obj)
	{
		RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
		newMenu.transform.SetParent(transform, false);
		Vector3 mousePosition = Input.mousePosition;
		newMenu.transform.position = mousePosition;

		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, 100))
		{
			//agent.SetDestination(hitInfo.point);
			newMenu.SpawnButtons(obj, hitInfo.point);
		}
	}
}
