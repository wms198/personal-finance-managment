using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiLivecharts2;
[ObservableObject]
public partial class PiechartViewModel
{
    [ObservableProperty] public ISeries[] _series;
    [ObservableProperty] public string _headline;
    private string incomeHeadline = "Income Pie Chart";
    private string expenseHeadline = "Expense Pie Chart";

    public PiechartViewModel(bool isIncome)
    {
        Update(isIncome);
    }

    public string HeadlineIncomes
    {
        get => incomeHeadline;
        set => incomeHeadline = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string HeadlineExoenses
    {
        get => expenseHeadline;
        set => expenseHeadline = value ?? throw new ArgumentNullException(nameof(value));
    }

    public void Update(bool isIncome)
    {
        List<Categoryresult> trans;
        if (isIncome)
        {
            trans = DBtransaction.CatgoriesIncomes();
            Headline = incomeHeadline;
        }
        else{
            trans = DBtransaction.CatgoriesExpenses();
            Headline  = expenseHeadline; 
        }
        
        var points = new ISeries[trans.Count];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new PieSeries<double>
            {
                Values = new double[] { Decimal.ToDouble(trans[i].value) },
                Name = trans[i].name,
            } ;
        }
        Series = points;
    }

    public void BackToMainPage()
    {
        
    }
    
}

