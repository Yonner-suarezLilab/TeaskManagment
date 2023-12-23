using System.Runtime.InteropServices;

namespace TaskManagment.Domain.Shared
{
    public static class DateTimeSystem
    {
        /// <summary>
        /// Returns TimeZone adjusted time for a given from a Utc or local time.
        /// Date is first converted to UTC then adjusted.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeZoneId"></param>
        /// <returns></returns>
        /// 

        public static DateTime Peru(this DateTime time)
        {
            string peruTimeZoneWindowsName = "";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                peruTimeZoneWindowsName = "SA Pacific Standard Time";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                peruTimeZoneWindowsName = "America/Lima";
            }

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(peruTimeZoneWindowsName);
            time.ToTimeZoneTime(tzi);

            return time.ToTimeZoneTime(tzi);
        }

        /// <summary>
        /// Returns TimeZone adjusted time for a given from a Utc or local time.
        /// Date is first converted to UTC then adjusted.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeZoneId"></param>
        /// <returns></returns>
        public static DateTime ToTimeZoneTime(this DateTime time, TimeZoneInfo tzi)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(time, tzi);
        }
    }
}