using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
	public bool isHovering;

	/// <summary>
	/// Event response, attached in the editor.
	/// </summary>
	/// <param name="isHovering">true if a mouse is hovering over the UI element</param>
	public void OnEnterExitUI(bool isHovering)
	{
		Debug.Log("OnEnterExitUI: " + isHovering.ToString());
		this.isHovering = isHovering;
	}

}
