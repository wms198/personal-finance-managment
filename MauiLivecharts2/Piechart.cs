using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiLivecharts2;
[ObservableObject]
public partial class Piechart
{
    private bool answer = true;
    [ObservableProperty] public ISeries[] _series;

    public bool Answer
    {
        get => answer;
        set => answer = value;
    }

    public void update(bool answer)
    {
        if (!answer)
        {
            var trans = DBtransaction.CatgoriesExpenses();
            var points = new ISeries[trans.Count];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new PieSeries<double>
                {
                    Values = new double[] { Decimal.ToDouble(trans[i].value) },
                    Name = trans[i].name,
                    //ToolTipLabelFormatter = (chartPoint) => $"{chartPoint.Context.Series.Name}: {chartPoint.PrimaryValue}"
                } ;
            }
            _series = points;
        }
        else
        {
            Console.WriteLine($"In update method {this.answer}");
            var trans = DBtransaction.CatgoriesIncomes();
            var points = new ISeries[trans.Count];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new PieSeries<double>
                {
                    Values = new double[] { Decimal.ToDouble(trans[i].value) },
                    Name = trans[i].name,
                    //ToolTipLabelFormatter = (chartPoint) => $"{chartPoint.Context.Series.Name}: {chartPoint.PrimaryValue}"
                };
            }
            _series = points;
        }
    }
}