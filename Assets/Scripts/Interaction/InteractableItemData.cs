﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "InteractableItem/Data")]
public class InteractableItemData : ScriptableObject
{
    public string itemName;
    public ScriptableAction[] actions;
}