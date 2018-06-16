using DatabaseAssembly;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project4App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouriteMainPage : ContentPage
    {
        public ObservableCollection<Grouping<string, FavouriteLine>> Items { get; set; } = new ObservableCollection<Grouping<string, FavouriteLine>>();
        FavouriteLine selectedLine;

        public FavouriteMainPage()
        {
            InitializeComponent();

            FavouriteView.ItemTapped += async (object sender, ItemTappedEventArgs e) => {
                if (e.Item == null)
                {
                    return;
                }

                bool selectedLineWillbeNull = false;

                if (selectedLine != null && selectedLine == ((FavouriteLine)e.Item))
                {
                    bool result = await DisplayAlert("Wil je deze zin verwijderen uit je favorieten?", selectedLine.Text, "Ja", "Nee");
                    if (result == true)
                    {
                        switch (selectedLine.FavouriteLineType)
                        {
                            case FavouriteLineType.PickUpLine:
                                var allPickupLines = await App.Database.GetPickupLinesAsync();
                                foreach (var pickupLine in allPickupLines)
                                {
                                    if (pickupLine.IsFavourited && pickupLine.Text.ToLower() == selectedLine.Text.ToLower())
                                    {
                                        pickupLine.IsFavourited = false;
                                        await App.Database.SavePickupLineAsync(pickupLine);
                                    }
                                }

                                await AddFavouriteLinesToListview();
                                break;
                            case FavouriteLineType.MotivationLine:
                                var allMotivationLines = await App.Database.GetMotivationLinesAsync();
                                foreach (var motivationLine in allMotivationLines)
                                {
                                    if (motivationLine.IsFavourited && motivationLine.Text.ToLower() == selectedLine.Text.ToLower())
                                    {
                                        motivationLine.IsFavourited = false;
                                        await App.Database.SaveMotivationLineAsync(motivationLine);
                                    }
                                }

                                await AddFavouriteLinesToListview();
                                break;
                            case FavouriteLineType.JokeLine:
                                var allJokeLines = await App.Database.GetJokeLinesAsync();
                                foreach (var jokeLine in allJokeLines)
                                {
                                    if (jokeLine.IsFavourited && jokeLine.Text.ToLower() == selectedLine.Text.ToLower())
                                    {
                                        jokeLine.IsFavourited = false;
                                        await App.Database.SaveJokeLineAsync(jokeLine);
                                    }
                                }

                                await AddFavouriteLinesToListview();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        ((ListView)sender).SelectedItem = null;
                        selectedLine = null;
                        selectedLineWillbeNull = true;
                    }
                }

                if (!selectedLineWillbeNull)
                {
                    selectedLine = ((FavouriteLine)e.Item);
                }
            };
        }

        public async Task AddFavouriteLinesToListview()
        {
            Items.Clear();

            List<PickupLine> pickupLines = (await App.Database.GetPickupLinesAsync()).Where(p => p.IsFavourited == true).ToList();
            List<MotivationLine> motivationLines = (await App.Database.GetMotivationLinesAsync()).Where(m => m.IsFavourited == true).ToList();
            List<JokeLine> jokeLines = (await App.Database.GetJokeLinesAsync()).Where(j => j.IsFavourited == true).ToList();
            List<FavouriteLine> favouriteLines = new List<FavouriteLine>();

            foreach (PickupLine pickupLine in pickupLines)
            {
                int totalSameLines = favouriteLines.Count(f => f.Text.ToLower() == pickupLine.Text.ToLower());

                if (totalSameLines > 0)
                {
                    continue;
                }

                FavouriteLine favouriteLine = new FavouriteLine();
                favouriteLine.Text = pickupLine.Text;
                favouriteLine.Heading = "OPENINGSZINNEN";
                favouriteLine.FavouriteLineType = FavouriteLineType.PickUpLine;

                favouriteLines.Add(favouriteLine);
            }

            foreach (MotivationLine motivationLine in motivationLines)
            {
                int totalSameLines = favouriteLines.Count(f => f.Text.ToLower() == motivationLine.Text.ToLower());

                if (totalSameLines > 0)
                {
                    continue;
                }

                FavouriteLine favouriteLine = new FavouriteLine();
                favouriteLine.Text = motivationLine.Text;
                favouriteLine.Heading = "MOTIVATIE";
                favouriteLine.FavouriteLineType = FavouriteLineType.MotivationLine;

                favouriteLines.Add(favouriteLine);
            }

            foreach (JokeLine jokeLine in jokeLines)
            {

                int totalSameLines = favouriteLines.Count(f => f.Text.ToLower() == jokeLine.Text.ToLower());

                if (totalSameLines > 0)
                {
                    continue;
                }

                FavouriteLine favouriteLine = new FavouriteLine();
                favouriteLine.Text = jokeLine.Text;
                favouriteLine.Heading = "GRAPPEN";
                favouriteLine.FavouriteLineType = FavouriteLineType.JokeLine;

                favouriteLines.Add(favouriteLine);
            }

            var sorted = from favouriteLine in favouriteLines
                         orderby favouriteLine.Heading
                         group favouriteLine by favouriteLine.Heading into favouriteLineGroup
                         select new Grouping<string, FavouriteLine>(favouriteLineGroup.Key, favouriteLineGroup);

            

            foreach (var g in sorted)
                Items.Add(g);

            FavouriteView.HasUnevenRows = true;
            BindingContext = this;
        }

        public class FavouriteLine
        {
            public string Text { get; set; }
            public string Heading { get; set; }
            public FavouriteLineType FavouriteLineType { get; set; }
        }

        public enum FavouriteLineType
        {
            PickUpLine,
            MotivationLine,
            JokeLine,
        }
    }
}
