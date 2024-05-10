using Mobile.ViewModels;
using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace Mobile.Views;

public partial class vRegister : ContentPage
{
    RegisterViewModel rvm;
	public vRegister()
	{
		InitializeComponent();

        rvm = (RegisterViewModel)BindingContext;
        rvm.IsBorderOneVisible = true;
        rvm.IsBorderTwoVisible = false;
    }

	private async void ToggleDisplayBorder(object sender, EventArgs e)
	{
		if(!rvm.IsBorderTwoVisible)
		{
            rvm.IsBorderOneVisible = false;
            rvm.IsBorderTwoVisible = true;

            indicator_one.Background = Colors.White;
            indicator_two.Background = Color.Parse("#6074C6");

            await border_two.TranslateTo(TranslationY = 100, 0, 0);
            await border_two.TranslateTo(TranslationY = 1, 0, 200);
        }
        else
        {
            rvm.IsBorderTwoVisible = false;
            rvm.IsBorderOneVisible = true;

            indicator_one.Background = Color.Parse("#6074C6");
            indicator_two.Background = Colors.White;

            await border_one.TranslateTo(TranslationY = -100, 0, 0);
            await border_one.TranslateTo(TranslationY = 1, 0, 200);
        }

    }
}