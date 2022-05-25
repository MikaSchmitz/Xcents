using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xcents.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xcents.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExpensePage : ContentPage
    {
        public AddExpensePage()
        {
            InitializeComponent();
            PopulateRepeatMultiplierPicker();
            PopulateTimePeriodPicker();
            UpdateCostLabel();
        }

        private void PopulateRepeatMultiplierPicker()
        {
            RepeatMultiplierPicker.Items.Clear();
            for(int i = 1; i < 366; i++)
            {
                RepeatMultiplierPicker.Items.Add(i.ToString());
            }
            RepeatMultiplierPicker.SelectedIndex = 0;
        }

        private void PopulateTimePeriodPicker()
        {
            TimePeriodPicker.Items.Clear();
            foreach (string timeUnit in TimeUnits.Get())
            {
                TimePeriodPicker.Items.Add(timeUnit);
            }
            TimePeriodPicker.SelectedIndex = 2;
        }

        private void UpdateCostLabel()
        {
            string question = $"How much will you pay every {RepeatMultiplierPicker.SelectedItem.ToString()} {TimePeriodPicker.SelectedItem.ToString()}";
            if (RepeatMultiplierPicker.SelectedItem.ToString() != "1")
                question += "s";
            ExpenseCostLabel.Text = question + "?";
        }

        //change value on number multiplier
        private void RepeatMultiplierPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            //! find out why there is a null error here
            //UpdateCostLabel();
        }

        //change value on time period picker
        private void TimePeriodPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCostLabel();
        }

        //cost entry text changed
        private void ExpenseCostEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string costInput = ExpenseCostEntry.Text;
            if (!costInput.Contains("€"))
                costInput = "€" + costInput;
            ExpenseCostEntry.Text = costInput;
        }

        //clicked submit
        private async void SubmitExpenseButton_Clicked(object sender, EventArgs e)
        {

            double cost = 0;
            //check for wrong or empty inputs
            Queue<string> errors = new Queue<string>();
            if (string.IsNullOrEmpty(ExpenseNameEntry.Text))
                errors.Enqueue("Please give your expense a name");
            if (string.IsNullOrEmpty(ExpenseCostEntry.Text))
                errors.Enqueue("Make sure you've added a cost to your expense");
            if (!double.TryParse(ExpenseCostEntry.Text.Remove(0, 1), out cost))
                errors.Enqueue("you've entered an invalid cost");

            //show errors if mistakes were found
            if (errors.Count > 0)
            {
                _ = DisplayAlert("Sorry", errors.Dequeue(), "OK");
                return;
            }

            //create expense
            ExpenseManager.CreateNewExpense(ExpenseNameEntry.Text, cost, ExpenseStartDatePicker.Date, TimePeriodPicker.SelectedItem.ToString(), int.Parse(RepeatMultiplierPicker.SelectedItem.ToString()));
            await Navigation.PopToRootAsync();
        }
    }
}