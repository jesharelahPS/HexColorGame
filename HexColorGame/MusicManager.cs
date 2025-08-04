using Plugin.Maui.Audio;
using Microsoft.Maui.Storage;

namespace HexColorGame;

public static class MusicManager
{
    private static IAudioPlayer? _player;
    private static string? _currentTrack;

    public static bool IsPlaying => _player?.IsPlaying ?? false;
    public static string? CurrentTrack => _currentTrack;

    public static async Task PlayMusic(string trackName = "bgm", double volume = 0.8)
    {
        if (_player != null && _currentTrack == trackName && _player.IsPlaying)
            return;

        _player?.Stop();
        _player?.Dispose();

        try
        {
            var file = await FileSystem.OpenAppPackageFileAsync($"{trackName}.mp3");
            _player = AudioManager.Current.CreatePlayer(file);
            _player.Volume = volume;
            _player.Loop = true;
            _player.Play();

            _currentTrack = trackName;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to play music: {ex.Message}");
        }
    }

    public static void StopMusic()
    {
        _player?.Stop();
    }
}
