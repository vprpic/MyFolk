using UnityEngine;

namespace MyFolk.Time
{
	[CreateAssetMenu(fileName = "New Calendar Preset", menuName = "Calendar Preset")]
	public class CalendarScriptableObject : ScriptableObject
	{
		/// <summary>
		/// e.g. ingameSecond = 6, means that for each real life second 6 ingame seconds pass
		/// </summary>
		public float ingameSecond;

		public string[] daysOfTheWeek;
		public Month[] months;
		public int dayStartOfFirstSeasonInList;
		public Season[] seasons;

		[System.Serializable]
		public struct Month
		{
			public string name;
			public int numOfDays;
		}

		public int NumOfDaysInYear()
		{
			int days = 0;
			foreach (Month month in months)
			{
				days += month.numOfDays;
			}
			return days;
		}
	}
}