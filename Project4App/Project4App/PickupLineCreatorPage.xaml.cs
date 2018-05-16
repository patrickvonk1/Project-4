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
	public partial class PickupLineCreatorPage : ContentPage
	{
		public PickupLineCreatorPage ()
		{
			InitializeComponent ();
		}
        
        private async void Save_Clicked(object sender, EventArgs e)
        {
            PickupLine pickupLineItem = (PickupLine)BindingContext;

            await App.Database.SavePickupLineAsync(pickupLineItem);
            await Navigation.PopAsync();
        }

        private async void Cancle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}