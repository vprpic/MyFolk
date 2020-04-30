﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFolk.FlexibleUI
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class FlexibleUIButton : FlexibleUI
    {
        protected Button button;
        protected Image image;

        public ButtonType buttonType;

        public enum ButtonType
        {
            Default,
            Highlight,
            Decline,
            Warning
        }

        public override void Awake()
        {
            button = GetComponent<Button>();
            image = GetComponent<Image>();

            base.Awake();
        }

        protected override void OnSkinUI()
        {
            button.transition = Selectable.Transition.SpriteSwap;
            button.targetGraphic = image;

            image.sprite = skinData.buttonSprite;
            image.type = Image.Type.Sliced;
            button.spriteState = skinData.buttonSpriteState;

            switch (buttonType)
            {
                case ButtonType.Highlight:
                    image.color = skinData.highlightColor;
                    break;
                case ButtonType.Decline:
                    //image.color = skinData.declineColor;
                    break;
                case ButtonType.Default:
                    image.color = skinData.defaultColor;
                    break;
                case ButtonType.Warning:
                    //image.color = skinData.warningColor;
                    break;
            }


            base.OnSkinUI();
        }
    }
}