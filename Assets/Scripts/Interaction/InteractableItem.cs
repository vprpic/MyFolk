using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
    public class InteractableItem : MonoBehaviour
    {
        private Vector3 _clickPoint;
        [SerializeField]
        public InteractableItemData data;
        public float RaycastRange => 100f;

        public string itemName => data.itemName;
        public Interaction[] Interactions => data.interactions;
        public Vector3 InteractionPoint => data.interactionPoint;
        public Sprite QueueSprite => data.queueSprite;
        private void Awake()
        {
            if (data == null)
            {
                Debug.LogError("InteractableItem's data isn't set: " + this.name);
            }
        }

        public void OnStartHover()
        {
        }

        public void OnInteract(Vector3 clickPoint)
        {
            this._clickPoint = clickPoint;
            //if (!clickPoint.Equals(Vector3.zero))
            //    onInteractable.Raise(this);
        }

        public void OnEndHover()
        {
        }

    }
}