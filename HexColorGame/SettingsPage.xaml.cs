using Microsoft.Maui.Storage;
using Plugin.Maui.Audio;

namespace HexColorGame;

public partial class SettingsPage : ContentPage
{
    private bool bgmEnabled;
    private bool sfxEnabled;
    private readonly IAudioManager audioManager = AudioManager.Current;

    public SettingsPage()
    {
        InitializeComponent();

        bgmEnabled = Preferences.Get("bgm_enabled", true);
        sfxEnabled = Preferences.Get("sfx_enabled", true);

        UpdateToggles();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (bgmEnabled && !MusicManager.IsPlaying)
        {
            await MusicManager.PlayMusic("bgm");
        }
        else if (!bgmEnabled)
        {
            MusicManager.StopMusic();
        }
    }

    private void UpdateToggles()
    {
        BgmIconToggle.Opacity = bgmEnabled ? 1 : 0.3;
        SfxIconToggle.Opacity = sfxEnabled ? 1 : 0.3;
    }

    private async Task PlayTapSound(double volume = 0.2)
    {
        if (!sfxEnabled) return;

        try
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("tap.mp3");
            var player = audioManager.CreatePlayer(stream);
            player.Volume = volume;
            player.Play();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Tap sound error: {ex.Message}");
        }
    }

    private async void OnBgmIconClicked(object sender, EventArgs e)
    {
        bgmEnabled = !bgmEnabled;
        UpdateToggles();
        await PlayTapSound(0.15);
    }

    private async void OnSfxIconClicked(object sender, EventArgs e)
    {
        sfxEnabled = !sfxEnabled;
        UpdateToggles();
        await PlayTapSound(0.15);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        Preferences.Set("bgm_enabled", bgmEnabled);
        Preferences.Set("sfx_enabled", sfxEnabled);

        if (bgmEnabled)
            await MusicManager.PlayMusic("bgm");
        else
            MusicManager.StopMusic();

        await DisplayAlert("Saved", "Settings updated.", "OK");
    }
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }


}
