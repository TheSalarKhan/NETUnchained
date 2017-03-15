using System;
using System.Globalization;

namespace Utils {
    public class DateUtils {
        public static string DateTimeToString(DateTime DateTime) {
            return String.Format("{0:yyyy-MM-dd hh:mm tt}", DateTime);
        }

        public static DateTime StringToDateTime(string DateTimeString) {

            return DateTime.Parse(DateTimeString);
            // return DateTime.ParseExact(, "yyyy-MM-dd",  CultureInfo.InvariantCulture);
        }

        public static String CalculateTimeDifference(String timeStamp)
        {
            DateTime dateTime = Convert.ToDateTime(timeStamp);

            return CalculateTimeDifference(dateTime);
        }

        public static String CalculateTimeDifference(DateTime dateTime) {
            
            String date;

            TimeSpan span = DateTime.Now.Subtract(dateTime);
            if (span.Days == 0)
            {
                if (span.Hours == 0)
                {
                    date = span.Minutes.ToString() + "m ago";
                }
                else
                {
                    date = span.Hours.ToString() + "hr ago";
                }
            }
            else if (span.Days >= 365)
            {
                date = "Year ago";
            }

            else if (span.Days == 1)
            {
                date = "Yesterday";
            }
            else if (span.Days <= 6 && span.Days > 0)
            {
                date = dateTime.DayOfWeek.ToString();
            }
            else
            {
                date = dateTime.ToString("d");
            }

            return date;
        }
    }
}