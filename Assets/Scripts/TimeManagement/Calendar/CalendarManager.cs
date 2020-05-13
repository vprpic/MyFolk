using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Time
{
    public class CalendarManager : MonoBehaviour
    {
        public CalendarScriptableObject currentCalendar;

        public float secondsCounter;
        public int minutes;
        public int hours;
        public int days;
        public int dayOfYear;
        public int monthIndex;   //starts at 0 bc it's an index for a list
        public int year;
        public int seasonIndex;
        public int seasonChange => (currentCalendar.NumOfDaysInYear()) / currentCalendar.seasons.Length; //the season with the index 0 will have the remaining one day left

        public float counterTemp;
        private GUIStyle guiStyle = new GUIStyle();

        void Start()
        {
            if (currentCalendar.ingameSecond <= 0)
            {
                Debug.LogError("currentCalendar.ingameSecond <= 0");
            }
            this.dayOfYear = days;
            for (int i = 0; i < monthIndex; i++)
            {
                this.dayOfYear += currentCalendar.months[i].numOfDays;
            }
        }

        void Update()
        {
            this.counterTemp += UnityEngine.Time.deltaTime;
            this.secondsCounter += Globals.ins.timeManager.currentTimeScale / currentCalendar.ingameSecond;
            //Debug.Log("secondsCounter: " + secondsCounter + " 'real'Counter: " + this.counterTemp);
            if (this.secondsCounter >= 60f)
            {
                this.secondsCounter = 0f;
                UpdateMinutes();
            }
        }

        public void UpdateMinutes()
        {
            this.minutes++;
            if (this.minutes >= 60)
            {
                this.minutes = 0;
                UpdateHours();
            }
        }

        public void UpdateHours()
        {
            this.hours++;
            if (this.hours >= 24)
            {
                this.hours = 0;
                UpdateDays();
            }

        }
        public void UpdateDays()
        {
            int oldDay = this.days;
            this.days++;
            if (this.days > currentCalendar.months[this.monthIndex].numOfDays) //TODO: is it > ??
            {
                this.days = 1;
                (new EventCallbacks.DayPassedEvent(oldDay, this.days)).FireEvent();
                this.dayOfYear++;
                UpdateMonths();
            }
            else
            {
                (new EventCallbacks.DayPassedEvent(oldDay, this.days)).FireEvent();
            }
        }
        public void UpdateMonths()
        {
            int oldMonth = this.monthIndex;
            this.monthIndex++;
            if (this.monthIndex >= currentCalendar.months.Length)
            {
                this.monthIndex = 0;
                (new EventCallbacks.MonthPassedEvent(oldMonth, this.monthIndex)).FireEvent();
                UpdateYears();
            }
            else
            {
                (new EventCallbacks.MonthPassedEvent(oldMonth, this.monthIndex)).FireEvent();
            }
        }
        public void UpdateYears()
        {
            this.year++;
            this.dayOfYear = 1;
            (new EventCallbacks.YearPassedEvent(this.year - 1, this.year)).FireEvent();

        }

        public void UpdateSeason()
        {

        }

        private void OnGUI()
        {
            guiStyle.fontSize = 60;
            GUI.Label(new Rect(10, 10, 140, 50), this.days + "/" + (this.currentCalendar.months[this.monthIndex].name) + "/" + this.year, guiStyle);
            GUI.Label(new Rect(10, 60, 140, 50), this.hours + ":" + this.minutes + ":" + (int)this.secondsCounter, guiStyle);
            GUI.Label(new Rect(10, 110, 140, 50), this.currentCalendar.seasons[this.seasonIndex].seasonName, guiStyle);
        }
    }
}