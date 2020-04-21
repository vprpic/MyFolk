﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class RadialButtonUI : FlexibleUIButton
{
	[HideInInspector]
	public RadialMenu menuParent;
	public Image background;
	public Image icon;
	public string title;
	public float animateSpeed = 8f;

	//used in the radialMenuSpawner to disable the menu
	public VoidEvent onRadialButtonClick;
	[HideInInspector]
	public ButtonAction buttonAction;

	public void Animate()
	{
		StartCoroutine(AnimateButtonIn());
	}

	IEnumerator AnimateButtonIn()
	{
		transform.localScale = Vector3.zero;
		float timer = 0f;
		while (timer < 1 / animateSpeed)
		{
			timer += Time.deltaTime;
			transform.localScale = Vector3.one * timer * animateSpeed;
			yield return null;
		}
		transform.localScale = Vector3.one;
	}

	public void OnRadialButtonClick()
	{
		buttonAction.PrepareExecution(menuParent.currentCharacter.currentAgent, menuParent.worldPoint);
		onRadialButtonClick.Raise();
	}

}
