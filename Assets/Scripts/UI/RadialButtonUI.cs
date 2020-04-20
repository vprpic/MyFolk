using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public RadialMenu menuParent;
	public Image background;
	public Image icon;
	public string title;
	public float animateSpeed = 8f;
	public Color highlightColor;

	public ButtonAction buttonAction;


	public void Animate()
	{
		StartCoroutine(AnimateButtonIn());
	}

	public void OnClick(Vector3 clickPoint)
	{
		buttonAction.PrepareExecution(menuParent.currentCharacter.agent, clickPoint);
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

	public void OnPointerEnter(PointerEventData eventData)
	{
		menuParent.selected = this;
		highlightColor = background.color;
		background.color = Color.white;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		menuParent.selected = null;
		background.color = highlightColor;
	}

}
