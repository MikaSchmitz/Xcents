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
    public partial class EditExpensePage : ContentPage
    {
        private Expense expense;
        public EditExpensePage(Expense expense)
        {
            this.expense = expense;
            InitializeComponent();
            Title = "Edit " + expense.Name;
        }

        private async void DeleteExpenseButton_Clicked(object sender, EventArgs e)
        {
            ExpenseManager.DeleteExpense(expense);
            await Navigation.PopToRootAsync();
        }
    }
}