using System.Diagnostics;
using MauiLivecharts2.Viewmodel;

namespace MauiLivecharts2;

public partial class MainPage : ContentPage
{
    
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        DBtransaction.Verbinden();
        Update();
    }

    private void ToggleIncomes(object? sender, EventArgs e)
    {
        if (ExpensesContainer.IsVisible == true)
        {
            ExpensesContainer.IsVisible = false;
            IncomesContainer.IsVisible = !IncomesContainer.IsVisible;
        }else IncomesContainer.IsVisible = !IncomesContainer.IsVisible;
        
    }
    private void ToggleExpenses(object? sender, EventArgs e)
    {
        if (IncomesContainer.IsVisible == true)
        {
            IncomesContainer.IsVisible = false;
            ExpensesContainer.IsVisible = !ExpensesContainer.IsVisible;
        }else ExpensesContainer.IsVisible = !ExpensesContainer.IsVisible;
        
    }
    private void OnExpenseButtonClicked(object sender, EventArgs e)
    {
        ImageButton i = (ImageButton)sender;
        string category = i.CommandParameter.ToString();
        Console.WriteLine($"Catagory: {category} ");
        Navigation.PushAsync(new DetailPageExpenses(category, true, BindingContext as MainPageViewModel));
        
    }
    private void OnIncomeButtonClicked(object? sender, EventArgs e)
    {
        ImageButton i = (ImageButton)sender;
        string category = i.CommandParameter.ToString();
        Console.WriteLine($"Catagory: {category} ");
        Navigation.PushAsync(new DetailPageExpenses(category, false,  BindingContext as MainPageViewModel));
    }

    private async void OnPieChartButtonClicked(object? sender, EventArgs e)
    {
        bool isIncome = await DisplayAlert("", "Which Piechart do you want to see?", "Income", "Expenses");
        Debug.WriteLine("Answer: " + isIncome);

        Navigation.PushAsync(new PiechartModel(new PiechartViewModel(isIncome), isIncome));
    }
    public void Update()
    {
        ((MainPageViewModel)BindingContext).Update();
    }
    
    
    private void OnButtonDeleteClicked(object sender, EventArgs e)
    {
        var i = (ImageButton)sender;
        var t = (Transaction)i.BindingContext;
        //string idString = i.CommandParameter.ToString();
        //int id = Convert.ToInt32(i.CommandParameter);
        Console.WriteLine(t.Id);
        DBtransaction.DeleteTransaction(t);
        Update();
    }

    private async void OnButtonUpdateClicked(object sender, EventArgs args)
    {
        ImageButton i = (ImageButton)sender;
        Transaction t = (Transaction)i.BindingContext;
        
        await Navigation.PushAsync(new DetailPageExpenses(t,  BindingContext as MainPageViewModel));
        Console.WriteLine($"\n\n\nthe price is{t.Value}");
    }
}