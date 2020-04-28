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

		public void SpawnButtons(InteractableItemClickedEventInfo obj)
		{
			StartCoroutine(AnimateButtons(obj));
		}

		IEnumerator AnimateButtons(InteractableItemClickedEventInfo eventInfo)
		{
			for (int i = 0; i < eventInfo.iitem.Interactions.Length; i++)
			{
				RadialButtonUI newButton = Instantiate(buttonPrefab) as RadialButtonUI;
				newButton.transform.SetParent(transform, false);

				float theta = (2 * Mathf.PI / eventInfo.iitem.Interactions.Length) * i;
				float xPos = Mathf.Sin(theta);
				float yPos = Mathf.Cos(theta);
				newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 100f;

				newButton.Init(this, eventInfo, eventInfo.iitem.Interactions[i]);
				newButton.Animate();
				yield return new WaitForSeconds(0.03f);
			}
		}
	}
}