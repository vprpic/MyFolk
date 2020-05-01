using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.UI
{
	public class RadialMenu : MonoBehaviour
	{
		public Vector3 worldPoint;
		public RadialButtonUI buttonPrefab;

		public void SpawnButtons(InteractableItemClickedEvent obj, List<Interaction> possibleInteractions)
		{
			StartCoroutine(AnimateButtons(obj, possibleInteractions));
		}

		IEnumerator AnimateButtons(InteractableItemClickedEvent eventInfo, List<Interaction> possibleInteractions)
		{
			for (int i = 0; i < possibleInteractions.Count; i++)
			{
				RadialButtonUI newButton = Instantiate(buttonPrefab) as RadialButtonUI;
				newButton.transform.SetParent(transform, false);

				float theta = (2 * Mathf.PI / possibleInteractions.Count) * i;
				float xPos = Mathf.Sin(theta);
				float yPos = Mathf.Cos(theta);
				newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 100f;

				newButton.Init(this, eventInfo, possibleInteractions[i]);
				newButton.Animate();
				yield return new WaitForSeconds(0.03f);
			}
		}
	}
}