using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableItem
{
	InteractableItem ins { get; }
	string itemName { get; }
	float MaxRange { get; }
	Vector3 ClickPoint { get; }
	ScriptableAction[] Actions { get; }
	
	void OnStartHover();
	void OnInteract(Vector3 clickPoint);
	void OnEndHover();
}
