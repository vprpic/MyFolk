using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractableItem
{
    public InteractableItemEvent onInteractable;
    private Vector3 _clickPoint;
    [SerializeField]
    private ButtonAction[] _actions;
    public float MaxRange => 100f;

    public ButtonAction[] Actions => _actions;
    public Vector3 ClickPoint => _clickPoint;


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