using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public ButtonAction[] options;

    private void OnMouseDown()
    {
        //tell the canvas to spawn a menu
        RadialMenuSpawner.ins.SpawnMenu(this);
    }
}
