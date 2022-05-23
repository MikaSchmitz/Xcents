using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xcents.Model;
using Xcents.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xcents.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewExpensePage : ContentPage
    {
        private Expense expense;
        public ViewExpensePage(Expense expense)
        {
            this.expense = expense;
            InitializeComponent();
            PopulatePage();
        }

        private void PopulatePage()
        {
            ExpenseNameLabel.Text = expense.Name;

            string timeUnit = expense.TimePeriodRepeatMultiplier == 1 ? expense.TimePeriodValue : expense.TimePeriodRepeatMultiplier.ToString() + " " + expense.TimePeriodValue + "s";
            ExpenseCostLabel.Text = $"Costs €{expense.Cost} every {timeUnit}.";

            ExpenseDueDateLabel.Text = $"Next payment in {expense.DueDaysRemaining()} days\n{expense.DueDate().ToString("dddd, d MMMM yyyy")}.";

            string paymentCounter = expense.TimesPaid().ToString() + " payment";
            if (expense.TimesPaid() > 1)
                paymentCounter = paymentCounter + "s";
            ExpensePaymentsLabel.Text = $"{paymentCounter} since {expense.StartDateTime.ToString("MMMM d, yyyy")}.";

            ExpenseTotalLabel.Text = $"€{expense.TotalPayed()} spent total.";
        }

        //occurs when player clicks the edit button
        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditExpensePage(expense));
        }
    }
}