using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
    [System.Serializable]
    public class InteractionPoint
    {
        public Vector3 point;
        public Character occupiedBy;
    }
    public class InteractableItem : MonoBehaviour
    {
        private Vector3 _clickPoint;
        [SerializeField]
        public InteractableItemData data;
        public float RaycastRange => 100f;

        public string itemName => data.itemName;
        public Interaction[] Interactions => data.interactions;
        public Sprite QueueSprite => data.queueSprite;

        public Character isCurrentlyBeingUsedBy;

        public List<InteractionPoint> interactionPoints;

        private void Awake()
        {
            if (data == null)
            {
                Debug.LogError("InteractableItem's data isn't set: " + this.name);
            }
            this.isCurrentlyBeingUsedBy = null;
            this.interactionPoints = new List<InteractionPoint>();
            Transform interactionPointsParent = this.transform.Find("interactionPoints");
            if (interactionPointsParent != null)
            {
                foreach (Transform item in interactionPointsParent)
                {
                    InteractionPoint ip = new InteractionPoint();
                    ip.occupiedBy = null;
                    ip.point = item.transform.position;
                    this.interactionPoints.Add(ip);
                    //Debug.Log(this.name + ": interaction point added: " + item.ToString());
                }
            }
            else
            {
                //Debug.Log(this.name + ": doesn't have interaction points set.");
            }
        }

        public void OnStartHover()
        {
        }

        public void OnInteract(Vector3 clickPoint)
        {
            this._clickPoint = clickPoint;
        }

        public void OnEndHover()
        {
        }

    }
}