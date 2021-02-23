using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)
        {
            DateTime today = DateTime.Today;// get todays date
            int age = today.Year - dob.Year;// subtract their birth year from this year to get the difference
            if (dob.Date > today.AddYears(-age))// see if their birthday has already passed. if it hasn't then the age is 1 too many so decrement it.
            {
                age--;
            }
            return age;
        }
    }
}