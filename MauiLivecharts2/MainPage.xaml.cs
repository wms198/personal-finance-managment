using System.Diagnostics;

namespace MauiLivecharts2;

public partial class MainPage : ContentPage
{
    int count = 0;
    private bool isExpense;
    private List<Transaction> transactions = new List<Transaction>();
    private List<Transaction> push_dates{ get; set; } = new List<Transaction>();
    private  List<Grouped_list> new_list { get; set; } = new List<Grouped_list>();
    public List<Transaction> Transactions
    {
        get { return transactions; }
        set { transactions = value; }
    }

    public MainPage()
    {
        InitializeComponent();
        DBtransaction.Verbinden();
        //new_list = DBtransaction.SelectByDate();
        ListView.ItemsSource = DBtransaction.SelectTransactions();
        
    }
    
    private async void OnImageButtonClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("", "Which Piechart do you want to know?", "Incomes", "Expenses");
        
        Debug.WriteLine("Answer: " + answer);
        var chartModel = (Piechart)PieChart.BindingContext;
        if(answer){
            Console.WriteLine(answer);
            chartModel.update(answer);
            PieChart.IsVisible = true;
            PieChart.Series = chartModel.Series;
        }
        else
        {
            Console.WriteLine(answer);
            chartModel.update(answer);
            PieChart.IsVisible = true;
            PieChart.Series = chartModel.Series;
        }
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
        Navigation.PushAsync(new DetailPageExpenses(category, true, ListView));
        
    }
    private void OnIncomeButtonClicked(object? sender, EventArgs e)
    {
        ImageButton i = (ImageButton)sender;
        string category = i.CommandParameter.ToString();
        Console.WriteLine($"Catagory: {category} ");
        Navigation.PushAsync(new DetailPageExpenses(category, false, ListView));
    }  
    private void OnButtonDeleteClicked(object sender, EventArgs e)
    {
        var i = (ImageButton)sender;
        var t = (Transaction)i.BindingContext;
        //string idString = i.CommandParameter.ToString();
        //int id = Convert.ToInt32(i.CommandParameter);
        Console.WriteLine(t.Id);
        DBtransaction.DeleteTransaction(t);
        ListView.ItemsSource = DBtransaction.SelectTransactions();
    }

    private async void OnButtonUpdateClicked(object sender, EventArgs args)
    {
        ImageButton i = (ImageButton)sender;
        Transaction t = (Transaction)i.BindingContext;
        
        await Navigation.PushAsync(new DetailPageExpenses(t, ListView));
        Console.WriteLine($"\n\n\nthe price is{t.Value}");
    }
}