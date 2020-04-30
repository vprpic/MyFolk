using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFolk.FlexibleUI
{
    [CreateAssetMenu(menuName = "Flexible UI Data")]
    public class FlexibleUIData : ScriptableObject
    {
        public Sprite buttonSprite;
        public SpriteState buttonSpriteState;

        public Sprite interactionQueueSprite;

        public Color defaultColor;
        public Color confirmColor;
        public Color declineColor;
        public Color warningColor;

        #region Needs
        public Color thirdThirdBarColor;          //if a need is 2/3 and over
        public Color secondThirdBarColor;   //if a need is 1/3 and to 2/3
        public Color firstThirdBarColor;    //if a need is up to 1/3

        public Sprite panelBackground;
        public Color defaultPanelColor;
        #endregion Needs
    }
}