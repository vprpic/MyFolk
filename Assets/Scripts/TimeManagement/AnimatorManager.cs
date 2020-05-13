using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFolk
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorManager : MonoBehaviour
    {
        [HideInInspector]
        public Animator animator;
        public float prevTimeScale;
        public float currentTimeScale;

        void Start()
        {
            this.animator = GetComponent<Animator>();
            prevTimeScale = currentTimeScale = UnityEngine.Time.timeScale;
            EventCallbacks.TimeScaleChangedEvent.RegisterListener(OnTimeScaleShanged);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTimeScaleShanged(EventCallbacks.TimeScaleChangedEvent eventInfo)
        {
            ChangeTimeScale(eventInfo.newTimeScale);
            //if(this.currentTimeScale == 0f)
            //{
            //    animator.speed = 0;

            //    // Continue
            //    animator.speed = prevSpeed;
            //}
        }

        public void ChangeTimeScale(float newTimeScale)
        {
            this.prevTimeScale = this.currentTimeScale;
            this.currentTimeScale = newTimeScale;
            //if (newTimeScale == 0f)
            //    Debug.Log("Pause animator");
            animator.speed = newTimeScale;
        }
    }
}