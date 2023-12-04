using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextMessenger.DataLayer.Helpers
{
    public static class DateConvertHelper
    {
        public static string ConvertDateToString(DateTimeOffset date)
        {
            DateTimeOffset today = DateTimeOffset.Now.Date;

            if (date.Date == today)
            {
                return date.ToString("HH:mm");
            }
            else if (date.Year == today.Year)
            {
                return date.ToString("dd MMMM");
            }
            else
            {
                return date.ToString("dd MMMM yyyy");
            }
        }
    }
}
