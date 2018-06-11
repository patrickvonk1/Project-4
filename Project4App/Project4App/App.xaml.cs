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

        public async void PickupLinesFromFile(List<string> pickupLines)
        {
            return;

            foreach (var line in pickupLines)
            {
                string[] splittedLine = line.Split('*');
                string text = splittedLine[0];
                string type = splittedLine[1];

                ////Converting string to int
                //string numberString = "1";
                //int numberInt = int.Parse(numberString);//Gebruik parse (type wat het moet worden) (variableName) = (type wat het moet worden).parse(value)

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

                await Database.SavePickupLineAsync(newPickupLine);

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

                await Database.SaveMotivationLineAsync(newMotivationLine);

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
            preferencesNavPage.Title = "Preferences";

            //PickupLine page
            NavigationPage pickupLineNavPage = new NavigationPage(new PickupLineMainPage());
            pickupLineNavPage.Title = "Pick-up Lines";

            //MotivationLine Page
            NavigationPage motivationLineNavPage = new NavigationPage(new MotivationLineMainPage());
            motivationLineNavPage.Title = "Motivations";

            // JokeLine Page
            NavigationPage jokeLineNavPage = new NavigationPage(new JokeLineMainPage());
            jokeLineNavPage.Title = "Jokes";

            // Favourite Page
            NavigationPage FavouriteNavPage = new NavigationPage(new FavouriteMainPage());
            FavouriteNavPage.Title = "Favourites";

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
                newPreferences.AppTheme = AppTheme.Light;
                newPreferences.AttractedGender = AttractedGender.Both;
                newPreferences.IsProfanityFilterEnabled = true;

                await Database.SavePreferencesAsync(newPreferences);
                Preferences = newPreferences;
            }
            else
            {
                Preferences = preferences;
            }


            PickupLinesFromFile(pickupLines);
            //Handle when your app starts
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
