
namespace MauiLivecharts2;
public partial class PiechartModel : ContentPage
{
    private bool isIncome;
 
 
    public PiechartModel(PiechartViewModel vm, bool isIncome)
    {
        InitializeComponent();
        BindingContext = vm;
        this.isIncome = isIncome;
    }
    private void OnToggle(object sender, EventArgs e)
    {
        isIncome = !isIncome;
        ((PiechartViewModel)BindingContext).Update(isIncome);
    }

    private void BackToMainPage(object sender, EventArgs e)
    {
        Navigation.PopToRootAsync();
    }
}
