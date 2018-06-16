using DatabaseAssembly;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Project4App
{
	public partial class App : Application
	{
        public static Preferences Preferences { get; set; }

        public static List<string> pickupLines = new List<string>();
        public static List<string> motivationLines = new List<string>();
        public static List<string> jokeLines = new List<string>();

        public async void PickupLinesFromFile(List<string> pickupLines)
        {
            foreach (var line in pickupLines)
            {
                string[] splittedLine = line.Split('*');
                string text = splittedLine[0];
                string type = splittedLine[1];
                string gender = splittedLine[2];

                PickupLine newPickupLine = new PickupLine();
                newPickupLine.Text = text;

                PickupLineType pickupLineType;
                if (Enum.TryParse(type, out pickupLineType))
                {
                    newPickupLine.PickupLineType = pickupLineType;
                }
                else
                {
                    throw new Exception("A pickupLineType in the textfile is not correct. maybe somewhere its club instead of Club");
                }

                AttractedGender pickupLineGender;
                if (Enum.TryParse(gender, out pickupLineGender))
                {
                    newPickupLine.AttractedGender = pickupLineGender;
                }
                else
                {
                    throw new Exception("A pickupLineGender in the textfile is not correct. maybe somewhere its club instead of Club");
                }

                await Database.SavePickupLineAsync(newPickupLine);
            }
        }

        public async void motivationLinesFromFile(List<string> motivationLines)
        {
            foreach (var line in motivationLines)
            {
                string[] splittedLine = line.Split('*');
                string text = splittedLine[0];
                string type = splittedLine[1];
                string gender = splittedLine[2];

                MotivationLine newMotivationLine = new MotivationLine();
                newMotivationLine.Text = text;

                MotivationLineType motivationLineType;
                if (Enum.TryParse(type, out motivationLineType))
                {
                    newMotivationLine.MotivationLineType = motivationLineType;
                }
                else
                {
                    throw new Exception("A motivationLineType in the textfile is not correct. maybe somewhere its club instead of Club");
                }

                AttractedGender motivationLineGender;
                if (Enum.TryParse(gender, out motivationLineGender))
                {
                    newMotivationLine.AttractedGender = motivationLineGender;
                }
                else
                {
                    throw new Exception("A motivationLineGender in the textfile is not correct. maybe somewhere its club instead of Club");
                }

                await Database.SaveMotivationLineAsync(newMotivationLine);
            }
        }

        public async void jokeLinesFromFile(List<string> jokeLines)
        {
            foreach (var line in jokeLines)
            {
                string[] splittedLine = line.Split('*');
                string text = splittedLine[0];
                string type = splittedLine[1];
                string gender = splittedLine[2];

                JokeLine newJokeLine = new JokeLine();
                newJokeLine.Text = text;

                JokeLineType jokeLineType;
                if (Enum.TryParse(type, out jokeLineType))
                {
                    newJokeLine.JokeLineType = jokeLineType;
                }
                else
                {
                    throw new Exception("A jokeLineType in the textfile is not correct. maybe somewhere its club instead of Club");
                }

                AttractedGender jokeLineGender;
                if (Enum.TryParse(gender, out jokeLineGender))
                {
                    newJokeLine.AttractedGender = jokeLineGender;
                }
                else
                {
                    throw new Exception("A jokeLineGender in the textfile is not correct. maybe somewhere its club instead of Club");
                }

                await Database.SaveJokeLineAsync(newJokeLine);
            }
        }

        private static Database database;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(DependencyService.Get<ILocalFileHelper>().GetLocalFilePath("Project4DB.db3"));// Dont change the string!, this is the name of the Database
                }

                return database;
            }
        }

        public App ()
		{
            InitializeComponent();
            MainTabbedPage mainTabbedPage = new MainTabbedPage();

            //Preferences page
            NavigationPage preferencesNavPage = new NavigationPage(new PreferencesMainPage());
            preferencesNavPage.Icon = "settingsicon.png";

            //PickupLine page
            NavigationPage pickupLineNavPage = new NavigationPage(new PickupLineMainPage());
            pickupLineNavPage.Icon = "pickupicon.png";
            pickupLineNavPage.BarBackgroundColor = Color.FromHex("#96133a");
            pickupLineNavPage.BackgroundColor = Color.FromHex("#96133a");

            //MotivationLine Page
            NavigationPage motivationLineNavPage = new NavigationPage(new MotivationLineMainPage());
            motivationLineNavPage.Icon = "motivationicon.png";
            motivationLineNavPage.BarBackgroundColor = Color.FromHex("#094f0a");
            motivationLineNavPage.BackgroundColor = Color.FromHex("#094f0a");

            // JokeLine Page
            NavigationPage jokeLineNavPage = new NavigationPage(new JokeLineMainPage());
            jokeLineNavPage.Icon = "jokeicon.png";
            jokeLineNavPage.BarBackgroundColor = Color.FromHex("#c6a70f");
            jokeLineNavPage.BackgroundColor = Color.FromHex("#c6a70f");

            // Favourite Page
            NavigationPage FavouriteNavPage = new NavigationPage(new FavouriteMainPage());
            FavouriteNavPage.Icon = "favouriteicon.png";

            mainTabbedPage.Children.Add(preferencesNavPage);
            mainTabbedPage.Children.Add(pickupLineNavPage);
            mainTabbedPage.Children.Add(motivationLineNavPage);
            mainTabbedPage.Children.Add(jokeLineNavPage);
            mainTabbedPage.Children.Add(FavouriteNavPage);

            MainPage = mainTabbedPage;
		}

		protected override async void OnStart ()
		{
            Preferences preferences = await Database.GetPreferenceAsync();
            if (preferences == null)
            {
                Preferences newPreferences = new Preferences();
                newPreferences.AppTheme = AppTheme.Licht;
                newPreferences.AttractedGender = AttractedGender.Beide;

                await Database.SavePreferencesAsync(newPreferences);
                Preferences = newPreferences;
            }
            else
            {
                Preferences = preferences;
            }

            PreferencesMainPage.ChangeTheme(Preferences.AppTheme);
            PreferencesMainPage.ChangeAttractedGender(Preferences.AttractedGender);

            return;// wil je shit in de database haal deze return weg, zet je app aan, daarna zet de return precies hier terug boios
            PickupLinesFromFile(pickupLines);
            motivationLinesFromFile(motivationLines);
            jokeLinesFromFile(jokeLines);
            //Handle when your app starts
        }

		protected override void OnSleep ()
		{
            // Handle when your app sleeps
        }
	}
}
