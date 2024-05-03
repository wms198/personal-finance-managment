using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using CoreFoundation;
using Intents;

namespace MauiLivecharts2;

public partial class DetailPageExpenses : ContentPage
{
    private string category;
    private string date;
    private bool isExpense;
    private string explanation;
    private string explanationExpense = "Expenses";
    private string explanationIncomes = "Incomes";
    private string expenseHeadline = "How much money did you spend?";
    private string incomeHeadline = "How much money did you get?";
    private string updateHeadline = "How do you want to change it?";
    private string bTNInsert = "Insert";
    private string bTNUpdate = "Update";
    private CollectionView _listView;
    private Transaction _transaction;

    public DetailPageExpenses(string category, bool isExpense, CollectionView listView)
    {
        //Insert item
        this.category = category;
        this.isExpense = isExpense;
        _listView = listView;
        InitializeComponent();
        DBtransaction.Verbinden();
        if (isExpense)
            Headline.Text = expenseHeadline;
        else
            Headline.Text = incomeHeadline;
        ButtonName.Text = bTNInsert;
        Console.WriteLine(listView.Id);
    }

    public DetailPageExpenses(Transaction transaction, CollectionView listView)
    {
        //Update item
        InitializeComponent();
        _transaction = transaction;
        _listView = listView;
        Headline.Text = updateHeadline;
        preise.Text = Math.Abs(transaction.Value).ToString();
        DatePicker.Date = transaction.Date;
        ButtonName.Text = bTNUpdate;
    }

    void OnEntryPreis(object sender, EventArgs e)
    {
        string text = ((Entry)sender).Text;
        //Console.WriteLine(text);
        preise.Text = text;
    }
    void OnEntryDate(object sender, EventArgs e)
    {
        String date = DatePicker.Date.ToString();
        entry.Text = date;
        
        //Console.WriteLine($"Date: {DatePicker.Date.ToString()}");
    }

    private async void OnButtonEInsertClicked(object sender, EventArgs args)
    {
        /*Console.WriteLine("Expense Insert");
        Console.WriteLine($"Date: {DatePicker.Date.ToString()}");
        Console.WriteLine($"Price: {preise.Text}");
        Console.WriteLine($"Category : {category}");
        Console.WriteLine($"ImageUrl : {category}.png");*/
        Decimal value = Decimal.Parse(preise.Text);
        if (isExpense || _transaction?.Value < 0){
            value *= -1;
            explanation = explanationExpense;
        }
        
        string imageUrl = category + ".png";
        Console.WriteLine($"ImageUrl: {imageUrl}");
        if (_transaction == null)
        {
            Transaction transaction = new Transaction(category, value, DatePicker.Date, imageUrl);
            DBtransaction.InsertTransaction(transaction);
            bool answer = await DisplayAlert("", "Do you want to add more?", "Yes", "No");
            Console.WriteLine("Answer: " + answer);
            if (!answer)
            {
                Navigation.PopToRootAsync();
                Console.WriteLine("byebye");
            }
        }
        else
        {
            _transaction.Value = value;
            _transaction.Date = DatePicker.Date;
            DBtransaction.UpdateTransaction(_transaction);
            Navigation.PopToRootAsync();
        }
        this._listView.ItemsSource = DBtransaction.SelectTransactions();
    
    }
    

}