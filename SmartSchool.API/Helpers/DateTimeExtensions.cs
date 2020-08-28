using System;

namespace SmartSchool.API.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;

            int years = currentDate.Year - birthDate.Year;
            if (birthDate < currentDate)
            {
                years--;
            }

            return years;
        }
    }
}