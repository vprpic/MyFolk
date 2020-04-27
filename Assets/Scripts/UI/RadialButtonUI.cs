using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using MyFolk.FlexibleUI;
using EventCallbacks;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class RadialButtonUI : FlexibleUIButton
{
	[HideInInspector]
	public RadialMenu menuParent;
	public Image background;
	public Image icon;
	public TextMeshProUGUI text;
	public string title;
	public float animateSpeed = 8f;
	public InteractableItemClickedEventInfo interactableItemEventInfo;

	//used in the radialMenuSpawner to disable the menu
	public VoidEvent onRadialButtonClick;
	[HideInInspector]
	public ScriptableAction buttonAction;

	public void Init(RadialMenu parent, InteractableItemClickedEventInfo eventInfo, ScriptableAction action)
	{
		this.interactableItemEventInfo = eventInfo;
		this.buttonAction = action;

		this.icon.sprite = buttonAction.sprite;
		this.title = buttonAction.actionName;
		this.text.SetText(buttonAction.actionName);
		this.menuParent = parent;
	}

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
		buttonAction.PerformAction(interactableItemEventInfo.iitem.gameObject, interactableItemEventInfo.worldClickPoint);
		onRadialButtonClick.Raise();
	}

}
