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
        public byte IsEnabled { get; set; }

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

        //the amount of times the cost has already been paid
        public int TimesPaid()
        {
            TimeCalculator timeCalculator = new TimeCalculator(StartDateTime, TimePeriodValue, TimePeriodRepeatMultiplier);
            return timeCalculator.GetPassedLaps();
        }

        //total amount paid since the expense first occured
        public double TotalPayed()
        {
            return TimesPaid() * Cost;
        }
    }
}
