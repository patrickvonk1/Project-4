using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Project4App
{
	public partial class App : Application
	{
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

            //PickupLine page
            NavigationPage pickupLineNavPage = new NavigationPage(new PickupLineMainPage());
            pickupLineNavPage.Title = "PickupLines";

            //MotivationLine Page
            NavigationPage motivationLineNavPage = new NavigationPage(new MotivationLineMainPage());
            motivationLineNavPage.Title = "Motivation";

            // JokeLine Page
            NavigationPage jokeLineNavPage = new NavigationPage(new JokeLineMainPage());
            jokeLineNavPage.Title = "Jokes";

            mainTabbedPage.Children.Add(pickupLineNavPage);
            mainTabbedPage.Children.Add(motivationLineNavPage);
            mainTabbedPage.Children.Add(jokeLineNavPage);

            MainPage = mainTabbedPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
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
