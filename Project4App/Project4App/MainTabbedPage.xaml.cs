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
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage ()
        {
            InitializeComponent();
        }

        private async void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            if (CurrentPage.Title == "Favourites")
            {
                FavouriteMainPage favouriteMainPage = (FavouriteMainPage)(((NavigationPage)CurrentPage).CurrentPage);
                await favouriteMainPage.AddFavouriteLinesToListview();
            }
        }
    }
}