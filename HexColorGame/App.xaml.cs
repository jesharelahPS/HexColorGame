namespace HexColorGame
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new HomePage());

            // Load user preferences for background music
            bool bgmEnabled = Preferences.Get("bgm_enabled", true);

            if (bgmEnabled)
            {
                // Fire and forget – let the music start in the background
                _ = MusicManager.PlayMusic("bgm");
            }
        }

    }
}