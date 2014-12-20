using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Teirlinck.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime GetFromUnixTimeStamp(long unixTimeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return dt.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        public static DateTime GetFivePmYesterday()
        {
            DateTime today = DateTime.Today;

            DateTime fivePm = new DateTime(today.Year, today.Month, today.Day, 17, 0, 0);

            TimeZoneInfo estTz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            TimeSpan offset = estTz.GetUtcOffset(fivePm);

            DateTimeOffset dto = new DateTimeOffset(fivePm, offset);

            return dto.LocalDateTime;
        }
    }
}
