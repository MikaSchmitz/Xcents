using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Xcents.Model
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull, MaxLength(32)]
        public string Name { get; set; }
        [NotNull]
        public double Cost { get; set; }
        [NotNull]
        public DateTime StartDateTime { get; set; }
        [NotNull]
        public string TimePeriodValue { get; set; }
        [NotNull]
        public int TimePeriodRepeatMultiplier { get; set; }

        public void SaveChanges()
        {

        }

        //get the next due date
        public DateTime DueDate()
        {
            TimeCalculator timeCalculator = new TimeCalculator(StartDateTime, TimePeriodValue, TimePeriodRepeatMultiplier);
            return timeCalculator.GetNextPaymentDate();
        }

        //get days until due date
        public int DueDaysRemaining()
        {
            return DueDate().Subtract(DateTime.Now).Days;
        }
    }
}
