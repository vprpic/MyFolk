using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
	public RadialButton buttonPrefab;
	public RadialButton selected;
	public void SpawnButtons(Interactable obj)
	{
		StartCoroutine(AnimateButtons(obj));
	}

	IEnumerator AnimateButtons(Interactable obj)
	{
		for (int i = 0; i < obj.options.Length; i++)
		{
			RadialButton newButton = Instantiate(buttonPrefab) as RadialButton;
			newButton.transform.SetParent(transform, false);

			float theta = (2 * Mathf.PI / obj.options.Length) * i;
			float xPos = Mathf.Sin(theta);
			float yPos = Mathf.Cos(theta);
			newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 100f;
			newButton.circle.color = obj.options[i].color;
			newButton.icon.sprite = obj.options[i].sprite;
			newButton.title = obj.options[i].title;
			newButton.menuParent = this;
			newButton.Animate();
			yield return new WaitForSeconds(0.03f);
		}
	}

	private void Update()
	{
		//TODO: remove
		if (Input.GetMouseButtonUp(0))
		{
			if (selected)
			{
				Debug.Log(selected.title+" was selected.");
			}
			Destroy(gameObject);
		}
	}
}
