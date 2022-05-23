using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xcents.Model
{
    public class TimeCalculator
    {
        private DateTime startDate { get; set; }
        private string timePeriod { get; set; }
        private int multiplier { get; set; }

        public TimeCalculator(DateTime startDate, string timePeriod, int multiplier)
        {
            this.startDate = startDate;
            this.timePeriod = timePeriod;
            this.multiplier = multiplier;
        }


        //add days/weeks/months/years to a date
        private DateTime GetNext()
        {
            //check if a valid time period was selected
            if (TimeUnits.Get().Contains(timePeriod))
            {
                if (timePeriod == "Day")
                    return startDate.AddDays(multiplier);
                if (timePeriod == "Week")
                    return startDate.AddDays(multiplier*7);
                if (timePeriod == "Month")
                    return startDate.AddMonths(multiplier);
                if (timePeriod == "Year")
                    return startDate.AddYears(multiplier);
            }
            throw new Exception($"Invalid time period `{timePeriod}` selected.");
        }

        //get next payment date
        public DateTime GetNextPaymentDate()
        {
            DateTime paymentDate = startDate;
            while(paymentDate < DateTime.Now)
            {
                TimeCalculator tm = new TimeCalculator(paymentDate, timePeriod, multiplier);
                paymentDate = tm.GetNext();
            }
            return paymentDate;
        }

        //get laps passed to far
        public int GetPassedLaps()
        {
            int passedLaps = 0;
            DateTime paymentDate = startDate;
            while (paymentDate < DateTime.Now)
            {
                TimeCalculator tm = new TimeCalculator(paymentDate, timePeriod, multiplier);
                paymentDate = tm.GetNext();
                passedLaps++;
            }
            return passedLaps;
        }
    }
}
