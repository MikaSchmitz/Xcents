using System;
using System.Collections.Generic;
using System.Text;

namespace Xcents.Model
{
    public class TimeUnits
    {
        public static IEnumerable<string> Get()
        {
            return new List<string>
            {
                "Day",
                "Week",
                "Month",
                "Year"
            };
        }
    }
}
