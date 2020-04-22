using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
	public Vector3 worldPoint;
	//public GlobalsDataObject currentCharacter;
	public RadialButtonUI buttonPrefab;

	public void SpawnButtons(IInteractableItem obj)
	{
		StartCoroutine(AnimateButtons(obj));
	}

	IEnumerator AnimateButtons(IInteractableItem obj)
	{
		for (int i = 0; i < obj.Actions.Length; i++)
		{
			RadialButtonUI newButton = Instantiate(buttonPrefab) as RadialButtonUI;
			newButton.transform.SetParent(transform, false);

			float theta = (2 * Mathf.PI / obj.Actions.Length) * i;
			float xPos = Mathf.Sin(theta);
			float yPos = Mathf.Cos(theta);
			newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 100f;
			newButton.icon.sprite = obj.Actions[i].sprite;
			newButton.title = obj.Actions[i].title;
			newButton.text.SetText(obj.Actions[i].title);
			newButton.menuParent = this;
			newButton.buttonAction = obj.Actions[i];

			if(newButton.buttonAction is WalkAction)
			{
				newButton.buttonAction.clickPoint = worldPoint;
			}

			newButton.Animate();
			yield return new WaitForSeconds(0.03f);
		}
	}
}
