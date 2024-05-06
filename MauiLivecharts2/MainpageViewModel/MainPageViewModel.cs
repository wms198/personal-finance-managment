using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiLivecharts2.Viewmodel;

public partial class MainPageViewModel : ObservableObject
{
    public MainPageViewModel()
    {
        Console.WriteLine("foo");
    }

    [ObservableProperty] private List<Grouped_list> _transactions;

    public void Update()
    {
        this.Transactions = DBtransaction.SelectByDate();
    }
    
}
