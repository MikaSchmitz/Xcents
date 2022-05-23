﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Diagnostics;

namespace Xcents.Model
{
    public class ExpenseManager
    {
        //list of expenses for private use
        private List<Expense> expenses { get; set; }

        //REFRESH: expenses from database
        private void LoadExpenses()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Expense>();
                expenses = conn.Table<Expense>().ToList();
            }
        }

        //GET: select expenses
        public IEnumerable<Expense> GetExpenses {
            get
            {
                LoadExpenses();
                return expenses;
            }
        }

        //INSERT: new expense
        public void CreateNewExpense(string name, double cost, DateTime startDateTime, string timePeriodValue, int timePeriodRepeatMultiplier)
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
    }
}
