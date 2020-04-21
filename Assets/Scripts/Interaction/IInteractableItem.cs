using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableItem
{
	float MaxRange { get; }
	Vector3 ClickPoint { get; }
	ButtonAction[] Actions { get; }
	
	void OnStartHover();
	void OnInteract(Vector3 clickPoint);
	void OnEndHover();
}
