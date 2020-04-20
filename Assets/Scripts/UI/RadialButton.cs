using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Image circle;
	public Image icon;
	public string title;
	public RadialMenu menuParent;
	public float animateSpeed = 8f;

	Color defaultColor;

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

	public void OnPointerEnter(PointerEventData eventData)
	{
		menuParent.selected = this;
		defaultColor = circle.color;
		circle.color = Color.white;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		menuParent.selected = null;
		circle.color = defaultColor;
	}
}
