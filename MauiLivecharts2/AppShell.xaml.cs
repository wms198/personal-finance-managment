namespace MauiLivecharts2;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(DetailPageExpenses), typeof(DetailPageExpenses));

    }
}