using System.Globalization;

namespace MauiLivecharts2;

public class Transaction
{
    private string category;
    private decimal value;
    private DateTime date;
    private int id;
    private string imageUrl;
  

    public Transaction(int id)
    {
        this.id = id;
    }
    
    public Transaction(string category, decimal value, DateTime date, string imageUrl)
    {
        this.category = category;
        this.value = value;
        this.date = date;
        this.imageUrl = imageUrl;
   
    }

    public Transaction(int id,string category, decimal value, DateTime date)
    {
        this.id = id;
        this.category = category;
        this.value = value;
        this.date = date;
    }

    public Transaction(int id, string category, decimal value, DateTime date, string imageUrl)
    {
        this.id = id;
        this.category = category;
        this.value = value;
        this.date = date;
        this.imageUrl = imageUrl;
    }

    #region Properties
    public int Id
    {
        get => id;
        set => id = value;
    }

    public string Category
    {
        get => category;
        set => category = value;
    }

    public decimal Value
    {
        get => value;
        set => this.value = value;
    }

    public DateTime Date
    {
        get => date;
        set => date = value;
    }

    public string ImageUrl
    {
        get => imageUrl;
        set => imageUrl = value;
    }
    
    #endregion
    
}
public class IntToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Decimal v = (Decimal)value;
        if (v <= 0)
            return Color.FromRgb(255, 0, 0);
        return Color.FromRgb(10, 200, 10);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return 0;
    }
}