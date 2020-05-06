using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "InteractableItem/Data")]
    public class InteractableItemData : ScriptableObject
    {
        public string itemName;
        public List<Interaction> interactions;
        public Sprite queueSprite;
	}
}
