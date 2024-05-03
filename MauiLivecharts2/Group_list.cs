namespace MauiLivecharts2;

public class Grouped_list : List<Transaction>
{ 
    public Grouped_list(string date, List<Transaction> push_dates) : base(push_dates) {}
}