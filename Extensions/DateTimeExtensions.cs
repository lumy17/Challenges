namespace Challenges.WebApp.Extensions
{
	public static class DateTimeExtensions
	{
		public static string FormatTimeInterval(this TimeSpan interval)
		{
			var formattedInterval = "";
			if ((int)interval.TotalDays / 30 > 0)
				formattedInterval += $"{(int)interval.TotalDays / 30} months ";
			if ((int)interval.TotalDays % 30 > 0)
				formattedInterval += $"{(int)interval.TotalDays % 30} days ";
			if ((int)interval.TotalHours % 24 > 0)
				formattedInterval += $"{(int)interval.TotalHours % 24} hours ";
			if ((int)interval.TotalMinutes % 60 > 0)
				formattedInterval += $"{(int)interval.TotalMinutes % 60} minutes ";
			if ((int)interval.TotalSeconds % 60 > 0)
				formattedInterval += $"{(int)interval.TotalSeconds % 60} seconds ";
			if (!string.IsNullOrEmpty(formattedInterval))
			{
				formattedInterval = "now " + formattedInterval;
			}
			return formattedInterval.Trim();
		}
	}
}
