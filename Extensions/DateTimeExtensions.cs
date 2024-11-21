namespace Challenges.WebApp.Extensions
{
	public static class DateTimeExtensions
	{
		public static string FormatTimeInterval(this TimeSpan interval)
		{
			var formattedInterval = "";
			if ((int)interval.TotalDays / 30 > 0)
				formattedInterval += $"{(int)interval.TotalDays / 30} luni ";
			if ((int)interval.TotalDays % 30 > 0)
				formattedInterval += $"{(int)interval.TotalDays % 30} zile ";
			if ((int)interval.TotalHours % 24 > 0)
				formattedInterval += $"{(int)interval.TotalHours % 24} ore ";
			if ((int)interval.TotalMinutes % 60 > 0)
				formattedInterval += $"{(int)interval.TotalMinutes % 60} minute ";
			if ((int)interval.TotalSeconds % 60 > 0)
				formattedInterval += $"{(int)interval.TotalSeconds % 60} secunde ";
			if (!string.IsNullOrEmpty(formattedInterval))
			{
				formattedInterval = "acum " + formattedInterval;
			}
			return formattedInterval.Trim();
		}
	}
}
