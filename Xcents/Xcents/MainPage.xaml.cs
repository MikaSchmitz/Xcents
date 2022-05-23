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
        private ExpenseManager expenseManager = new ExpenseManager();
        public MainPage()
        {
            InitializeComponent();
            PopulateRows(expenseManager.GetExpenses.OrderBy(e => e.DueDaysRemaining()).ToList());
        }

        //add row to form
        protected void AddRow(Expense expense, int row=1)
        {
            //get days remaining until payment
            string daysRemaining = expense.DueDaysRemaining().ToString() + " days";
            if (expense.DueDaysRemaining() == 1)
                daysRemaining = daysRemaining.Remove(daysRemaining.Length-1);
            if (expense.DueDaysRemaining() == 0)
                daysRemaining = "today";
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 17, HorizontalOptions = LayoutOptions.Start, Text = expense.Name }, 0, row);
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 17, HorizontalOptions = LayoutOptions.End, Text = $"{daysRemaining}" }, 1, row);
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 17, HorizontalOptions = LayoutOptions.End, Text = $"€{expense.Cost}" }, 2, row);
        }

        //populate all rows
        protected void PopulateRows(List<Expense> expenses)
        {
            //clear rows
            ExpensesGrid.Children.Clear(); 
            //set labels
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 18, TextColor=(Color)App.Current.Resources["ThemeColor"], HorizontalOptions = LayoutOptions.Start, Text = "Expense", FontAttributes = FontAttributes.Bold }, 0, 0);
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 18, TextColor = (Color)App.Current.Resources["ThemeColor"], HorizontalOptions = LayoutOptions.End, Text = "Due in", FontAttributes = FontAttributes.Bold }, 1, 0);
            ExpensesGrid.Children.Add(new Label { Margin = 2, FontSize = 18, TextColor = (Color)App.Current.Resources["ThemeColor"], HorizontalOptions = LayoutOptions.End, Text = "Cost", FontAttributes = FontAttributes.Bold }, 2, 0);

            //show text if no expenses were found
            if(expenses.Count == 0)
            {
                DisplayAlert("Keep track of expenses!", "Start by adding expenses using the '+' button at the bottom of the screen.", "Confirm");
                return;
            }

            //show expenses
            double total = 0;
            for(int i = 0; i < expenses.Count(); i++)
            {
                AddRow(expenses[i], i+1);
                total += expenses[i].Cost;
            }
        }
    }
}
