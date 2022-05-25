using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xcents.Model;
using Xcents.Pages;

namespace Xcents
{
    public partial class MainPage : ContentPage
    {
        private ExpenseManager expenseManager = new ExpenseManager();

        //on generate page
        public MainPage()
        {
            InitializeComponent();
            PopulateRows(expenseManager.GetExpenses.ToList());
        }

        //on revisit page
        protected override void OnAppearing()
        {
            base.OnAppearing();
            PopulateRows(expenseManager.GetExpenses.ToList());
            TotalExpensesLabel.Text = "€" + expenseManager.CostsThisWeek();
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
            Button expenseNameButton = new Button { Margin = 2, FontSize = 12, HorizontalOptions = LayoutOptions.Start, Text = expense.Name };
            expenseNameButton.Clicked += (s, e) => EditExpenseButton_Clicked(expense);
            ExpensesGrid.Children.Add(expenseNameButton, 0, row);
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

        //occurs when player clicks on add expense button
        private async void AddExpenseButton_Clicked(object sender, EventArgs e)
        {
            //go to add expenses page
            await Navigation.PushAsync(new AddExpensePage());
        }

        //occurs when player clicks on the title of an existing expense
        private async void EditExpenseButton_Clicked(Expense expense)
        {
            await Navigation.PushAsync(new ViewExpensePage(expense));
        }

        //occurs when player clicks on the settings icon
        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
