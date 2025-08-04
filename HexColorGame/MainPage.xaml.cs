using Plugin.Maui.Audio;

namespace HexColorGame;

public partial class MainPage : ContentPage
{
    private readonly IAudioManager audioManager = AudioManager.Current;

    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        bool bgmEnabled = Preferences.Get("bgm_enabled", true);
        double bgmVol = 0.8;

        if (bgmEnabled && !MusicManager.IsPlaying)
        {
            await MusicManager.PlayMusic("bgm", bgmVol);
        }
    }

    private async void PlayTapSound()
    {
        bool sfxEnabled = Preferences.Get("sfx_enabled", true);
        if (!sfxEnabled) return;

        var stream = await FileSystem.OpenAppPackageFileAsync("tap.mp3");
        var player = audioManager.CreatePlayer(stream);
        player.Volume = 0.3; 
        player.Play();
    }

    private async void GoToKnight(object sender, EventArgs e)
    {
        PlayTapSound();
        await Navigation.PushAsync(new PageKnight());
    }

    private async void GoToDragon(object sender, EventArgs e)
    {
        PlayTapSound();
        await Navigation.PushAsync(new PageDragon());
    }

    private async void GoToKing(object sender, EventArgs e)
    {
        PlayTapSound();
        await Navigation.PushAsync(new PageKing());
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        PlayTapSound();
        await Navigation.PushAsync(new SettingsPage());
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        PlayTapSound();
    }

}
