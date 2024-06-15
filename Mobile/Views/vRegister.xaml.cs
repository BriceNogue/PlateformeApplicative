using Mobile.ViewModels;
using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace Mobile.Views;

public partial class vRegister : ContentPage
{
    RegisterViewModel RegisterVM;
	public vRegister()
	{
		InitializeComponent();

        RegisterVM = (RegisterViewModel)BindingContext;
        RegisterVM.IsBorderOneVisible = true;
        RegisterVM.IsBorderTwoVisible = false;
    }

	private async void ToggleDisplayBorder(object sender, EventArgs e)
	{
		if(!RegisterVM.IsBorderTwoVisible)
		{
            RegisterVM.IsBorderOneVisible = false;
            RegisterVM.IsBorderTwoVisible = true;

            indicator_one.Background = Colors.White;
            indicator_two.Background = Color.Parse("#1b6ec2");

            await border_two.TranslateTo(TranslationY = 100, 0, 0);
            await border_two.TranslateTo(TranslationY = 1, 0, 200);
        }
        else
        {
            RegisterVM.IsBorderTwoVisible = false;
            RegisterVM.IsBorderOneVisible = true;

            indicator_one.Background = Color.Parse("#1b6ec2");
            indicator_two.Background = Colors.White;

            await border_one.TranslateTo(TranslationY = -100, 0, 0);
            await border_one.TranslateTo(TranslationY = 1, 0, 200);
        }

    }
}