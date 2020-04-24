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

	IEnumerator AnimateButtons(IInteractableItem item)
	{
		for (int i = 0; i < item.Actions.Length; i++)
		{
			RadialButtonUI newButton = Instantiate(buttonPrefab) as RadialButtonUI;
			newButton.transform.SetParent(transform, false);

			float theta = (2 * Mathf.PI / item.Actions.Length) * i;
			float xPos = Mathf.Sin(theta);
			float yPos = Mathf.Cos(theta);
			newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 100f;

			newButton.Init(this, item, item.Actions[i]);
			newButton.Animate();
			yield return new WaitForSeconds(0.03f);
		}
	}
}
