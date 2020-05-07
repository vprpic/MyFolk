using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Interactable Item Data", fileName ="_Data")]
    public class InteractableItemData : ScriptableObject
    {
        public bool isObstacle;
        public string itemName;
        public List<Interaction> interactions;
        public Sprite queueSprite;
	}
}
