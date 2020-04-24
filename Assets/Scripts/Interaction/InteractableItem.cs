using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractableItem
{
    public InteractableItem ins => this;
    public InteractableItemEvent onInteractable;
    private Vector3 _clickPoint;
    [SerializeField]
    public InteractableItemData data;
    public float MaxRange => 100f;

    public string itemName => data.itemName;
    public ScriptableAction[] Actions => data.actions;
    public Vector3 ClickPoint => _clickPoint;

    private void Awake()
    {
        if(data == null)
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
        if (!clickPoint.Equals(Vector3.zero))
            onInteractable.Raise(this);
    }

    public void OnEndHover()
    {
    }

}