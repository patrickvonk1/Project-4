using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project4App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MotivationLineMainPage : ContentPage
	{
        MotivationLine currentMotivationLine;
        public MotivationLineMainPage ()
		{
			InitializeComponent ();

            ToolbarItem toolbarItem = new ToolbarItem() { Text = "+" };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MotivationLineCreatorPage() { BindingContext = new MotivationLine() });
            };

            ToolbarItems.Add(toolbarItem);
        }

        private async void BtnGetRandomMotivationLine_Clicked(object sender, EventArgs e)
        {
            MotivationLine randomMotivationLine = await App.Database.GetRandomMotivationLineAsync();

            if (randomMotivationLine != null)
            {
                LblCurrentMotivationLine.Text = "Random MotivationLine: " + randomMotivationLine.Text;
                currentMotivationLine = randomMotivationLine;
            }
            else
            {
                LblCurrentMotivationLine.Text = "There are no motivationlines available in the database!";
            }
        }
    }
}