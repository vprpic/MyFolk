using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
	public CharacterData currentCharacter;
	public RadialButtonUI buttonPrefab;
	public RadialButtonUI selected;
	public Vector3 mousePosition;
	public bool firstClick;
	public bool menuVisible;
	public void SpawnButtons(Interactable obj, Vector3 mousePosition)
	{
		this.mousePosition = mousePosition;
		menuVisible = true;
		firstClick = true;
		if (selected == null)
		{
			StartCoroutine(AnimateButtons(obj));
		}
	}

	IEnumerator AnimateButtons(Interactable obj)
	{
		for (int i = 0; i < obj.options.Length; i++)
		{
			RadialButtonUI newButton = Instantiate(buttonPrefab) as RadialButtonUI;
			newButton.transform.SetParent(transform, false);

			float theta = (2 * Mathf.PI / obj.options.Length) * i;
			float xPos = Mathf.Sin(theta);
			float yPos = Mathf.Cos(theta);
			newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 100f;
			newButton.icon.sprite = obj.options[i].sprite;
			newButton.title = obj.options[i].title;
			newButton.menuParent = this;
			newButton.buttonAction = obj.options[i];
			newButton.Animate();
			yield return new WaitForSeconds(0.03f);
		}
	}

	private void Update()
	{
		if (menuVisible && !firstClick && Input.GetMouseButtonDown(0))
		{
			firstClick = true;
			if(selected != null)
			{

				selected.OnClick(this.mousePosition);
			}
			menuVisible = false;
			Destroy(gameObject);
		}
		else
		{
			firstClick = false;
		}
	}
}
