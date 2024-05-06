using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using CoreFoundation;
using Intents;
using MauiLivecharts2.Viewmodel;

namespace MauiLivecharts2;

public partial class DetailPageExpenses : ContentPage
{
    private string category;
    private string date;
    private bool isExpense;
    private string explanation;
    private string explanationExpense = "Expenses";
    private string explanationIncomes = "Incomes";
    private string expenseHeadline = "How much money did you spend ? ";
    private string incomeHeadline = "How much money did you get ? ";
    private string updateHeadline = "How do you want to change it ? ";
    private string bTNInsert = "Insert";
    private string bTNUpdate = "Update";
    private MainPageViewModel _parent;
    private Transaction _transaction;

    public DetailPageExpenses(string category, bool isExpense, MainPageViewModel parent)
    {
        //Insert item
        this.category = category;
        this.isExpense = isExpense;
        _parent = parent;
        InitializeComponent();
        DBtransaction.Verbinden();
        if (isExpense)
            Headline.Text = expenseHeadline;
        else
            Headline.Text = incomeHeadline;
        ButtonName.Text = bTNInsert;
    }

    public DetailPageExpenses(Transaction transaction, MainPageViewModel parent)
    {
        //Update item
        InitializeComponent();
        _transaction = transaction;
        _parent = parent;
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
            bool answer = await DisplayAlert($"{category}", "Do you want to add more?", "Yes", "No");
            Console.WriteLine("Answer: " + answer);
            if (!answer)
            {
                //await Navigation.PushAsync( new ContentPage());
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
        _parent.Update();
    }

    private void BackToMainPage(object sender, EventArgs args)
    {
        Navigation.PopToRootAsync();
        
    }
    

} 