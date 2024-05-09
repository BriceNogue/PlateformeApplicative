using System.Data;

namespace Mobile.Views;

public partial class vRegister : ContentPage
{
	public vRegister()
	{
		InitializeComponent();

    }

	private async void ToggleDisplayBorder(object sender, EventArgs e)
	{
		if(border_two.HeightRequest == 0)
		{
            border_one.HeightRequest = 0;
            border_two.HeightRequest = 400;

            indicator_one.Background = Colors.White;
            indicator_two.Background = Color.Parse("#6074C6");

            await border_two.ScaleTo(-0.1, 200);
            await border_two.ScaleTo(1, 200);
        }
        else
        {
            border_two.HeightRequest = 0;
            border_one.HeightRequest = 400;

            indicator_one.Background = Color.Parse("#6074C6");
            indicator_two.Background = Colors.White;

            await border_one.ScaleTo(-0.1, 200);
            await border_one.ScaleTo(1, 200);
        }


    }
}