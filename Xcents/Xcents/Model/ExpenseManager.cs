using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using System.Diagnostics;

namespace Xcents.Model
{
    public class ExpenseManager
    {
        //list of expenses for private use
        private List<Expense> expenses { get; set; }

        #region DATABASE HANDLER
        //REFRESH: expenses from database
        private void LoadExpenses()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Expense>();
                expenses = conn.Table<Expense>().ToList();
            }
        }
        #endregion DATABASE HANDLER


        #region GET
        //GET: select expenses
        public IEnumerable<Expense> GetExpenses {
            get
            {
                LoadExpenses();
                return expenses.OrderBy(x => x.DueDaysRemaining()).ToList();
            }
        }

        //get todays expenses
        public IEnumerable<Expense> GetExpensesThisDay
        {
            get
            {
                return GetExpenses.Where(x => x.DueDaysRemaining() <= 0);
            }
        }

        //get this weeks expenses
        public IEnumerable<Expense> GetExpensesThisWeek
        {
            get
            {
                return GetExpenses.Where(x => x.DueDaysRemaining() <= 7);
            }
        }

        //get this months expenses
        public IEnumerable<Expense> GetExpensesThisMonth
        {
            get
            {
                return GetExpenses.Where(x => x.DueDaysRemaining() <= 31);
            }
        }

        //get this yeary expenses
        public IEnumerable<Expense> GetExpensesThisYear
        {
            get
            {
                return GetExpenses.Where(x => x.DueDaysRemaining() <= 365);
            }
        }
        #endregion GET


        #region INSERT
        //INSERT: new expense
        public static void CreateNewExpense(string name, double cost, DateTime startDateTime, string timePeriodValue, int timePeriodRepeatMultiplier)
        {
            Expense expense = new Expense() { Name = name, Cost = cost, StartDateTime = startDateTime, TimePeriodValue = timePeriodValue, TimePeriodRepeatMultiplier = timePeriodRepeatMultiplier, IsEnabled = 1 };
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Expense>();
                int insertedRows = conn.Insert(expense);
                if (insertedRows == 0)
                    Debug.WriteLine("Expense insertion failed");
            }
        }
        #endregion INSERT


        #region UPDATE
        //UPDATE: change existing expense
        public static void UpdateExpense(Expense expense)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Expense>();
                int changedRows = conn.Update(expense);
                if (changedRows == 0)
                    Debug.WriteLine("Expense update failed");
            }
        }
        #endregion UPDATE


        #region DELETE
        //DELETE: delete existing expense
        public static void DeleteExpense(Expense expense)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Expense>();
                int deletedRows = conn.Delete(expense);
                if (deletedRows == 0)
                    Debug.WriteLine("Expense deletion failed");
            }
        }
        #endregion DELETE


        #region METHODS
        public double CostsThisMonth()
        {
            return GetExpensesThisMonth.Sum(x => x.Cost);
        }

        public double CostsThisWeek()
        {
            return GetExpensesThisWeek.Sum(x => x.Cost);
        }
        #endregion METHODS
    }
}
