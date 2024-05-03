namespace MauiLivecharts2;

public class Grouped_list : List<Transaction>
{ 
    public DateTime Date { get; set; }

    public Grouped_list(DateTime date, List<Transaction> transactions) : base(transactions)
    {
        Date = date;
    }
}