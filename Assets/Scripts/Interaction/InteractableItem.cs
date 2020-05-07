using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

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
        private List<Interaction> tempInteractions;
        public List<Interaction> Interactions => data.interactions.Concat(tempInteractions).ToList();
        public Sprite QueueSprite => data.queueSprite;

        public Character isCurrentlyBeingUsedBy;

        public List<InteractionPoint> interactionPoints;
        public CarriableItem.ItemPlacementType ItemPlacementType;

        private void Awake()
        {
            if (data == null)
            {
                Debug.LogError("InteractableItem's data isn't set: " + this.name);
            }
            this.tempInteractions = new List<Interaction>();
            this.isCurrentlyBeingUsedBy = null;
            if (gameObject.GetComponent<CarriableItem>() == null && this.data.isObstacle)
            {
                NavMeshObstacle nmo = gameObject.GetComponent<NavMeshObstacle>();
                if (nmo != null)
                {
                    nmo.carving = true;
                    nmo.carveOnlyStationary = true;
                }
                else
                {
                    Debug.LogError("Missing component NavMeshObstacle! "+itemName);
                }
            }
            SetInteractionPoints();
        }

        private void SetInteractionPoints()
        {
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

        public void AddInteraction(Interaction interaction)
        {
            this.tempInteractions.Add(interaction);
        }

        public void AddInteractions(List<Interaction> interactions)
        {
            this.tempInteractions.AddRange(interactions);
        }

        public bool RemoveInteraction(Interaction interaction)
        {
            return this.tempInteractions.Remove(interaction);
        }

        public void RemoveInteractions(List<Interaction> interactions)
        {
            foreach (Interaction interaction in interactions)
            {
                this.tempInteractions.Remove(interaction);
            }
        }
    }
}