using Plugin.Maui.Audio;
using Microsoft.Maui.Storage;

namespace HexColorGame;

public partial class HomePage : ContentPage
{
    private readonly IAudioManager audioManager = AudioManager.Current;

    public HomePage()
    {
        InitializeComponent();
    }

    private async Task PlayTapSound(double volume = 0.3)
    {
        bool sfxEnabled = Preferences.Get("sfx_enabled", true);
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

    private async void OnPlayClicked(object sender, EventArgs e)
    {
        await PlayTapSound();
        await Navigation.PushAsync(new MainPage());
    }

    private async void OnExitClicked(object sender, EventArgs e)
    {
        await PlayTapSound(0.2);

#if WINDOWS
        System.Environment.Exit(0);
#elif ANDROID
        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#elif IOS
        Console.WriteLine("Exit not supported on iOS");
#endif
    }
}
