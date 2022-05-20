using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xcents.Model;

namespace Xcents
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            List<Expense> expenseList = new List<Expense>();
            expenseList.Add(new Expense() { Name="Zuyd", StartDateTime=DateTime.Now.AddDays(-12), Cost=110, TimePeriodRepeatMultiplier=1, TimePeriodValue="Month" });
            expenseList.Add(new Expense() { Name = "Sporten", StartDateTime = DateTime.Now.AddDays(-33), Cost = 33, TimePeriodRepeatMultiplier=1, TimePeriodValue = "Month" });
            expenseList.Add(new Expense() { Name = "Daily", StartDateTime = DateTime.Now, Cost = 1, TimePeriodRepeatMultiplier = 2, TimePeriodValue = "Day" });
            PopulateRows(expenseList);
        }

        //add row to form
        protected void AddRow(Expense expense, int row=1)
        {
            string format = "days";
            if (expense.DueDaysRemaining() == 1)
                format = format.Remove(format.Length-1);
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 17, HorizontalOptions = LayoutOptions.Start, Text = expense.Name }, 0, row);
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 17, HorizontalOptions = LayoutOptions.End, Text = $"{expense.DueDaysRemaining()} {format}" }, 1, row);
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 17, HorizontalOptions = LayoutOptions.End, Text = $"€{expense.Cost}" }, 2, row);
        }

        protected void PopulateRows(List<Expense> expenses)
        {
            ExpensesGrid.Children.Clear(); 
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 18, HorizontalOptions = LayoutOptions.Start, Text = "Expense", FontAttributes = FontAttributes.Bold }, 0, 0);
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 18, HorizontalOptions = LayoutOptions.End, Text = "Due in", FontAttributes = FontAttributes.Bold }, 1, 0);
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 18, HorizontalOptions = LayoutOptions.End, Text = "Cost", FontAttributes = FontAttributes.Bold }, 2, 0);

            double total = 0;
            for(int i = 0; i < expenses.Count(); i++)
            {
                AddRow(expenses[i], i+1);
                total += expenses[i].Cost;
            }
        }
    }
}
