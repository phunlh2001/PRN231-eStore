using System;

namespace DataAccess.Utility
{
    public static class BaseDate
    {
        public static DateTime GetFiveNextDays()
        {
            var today = DateTime.Now;
            var next5days = today.AddDays(5);
            return next5days;
        }

        public static DateTime GetSevenNextDays()
        {
            var today = DateTime.Now;
            var next7days = today.AddDays(7);
            return next7days;
        }
    }
}
