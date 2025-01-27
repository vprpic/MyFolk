﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MyFolk.FlexibleUI
{
    public class FlexibleUIInstance : Editor
    {

        [MenuItem("GameObject/Flexible UI/Button", priority = 0)]
        public static void AddButton()
        {
            Create("button");
        }

        [MenuItem("GameObject/Flexible UI/Panel", priority = 0)]
        public static void AddPanel()
        {
            Create("panel");
        }

        static GameObject clickedObject;

        private static GameObject Create(string objectName)
        {
            GameObject instance = Instantiate(Resources.Load<GameObject>(objectName));
            instance.name = objectName;
            clickedObject = UnityEditor.Selection.activeObject as GameObject;
            if (clickedObject != null)
            {
                instance.transform.SetParent(clickedObject.transform, false);
            }
            return instance;
        }

    }
}