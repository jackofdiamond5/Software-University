using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var songsCount = int.Parse(Console.ReadLine());

        var songs = new List<Song>();

        for (var i = 0; i < songsCount; i++)
        {
            try
            {
                var inputArgs = Console.ReadLine().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var artistName = inputArgs[0];

                var songName = inputArgs[1];
                var songDurationArgs = inputArgs[2].Split(':');
                var songMinutes = int.Parse(songDurationArgs[0]);
                var songSeconds = int.Parse(songDurationArgs[1]);

                var artist = new Artist(artistName);
                var song = new Song(songName, songMinutes, songSeconds, artist);

                songs.Add(song);
                Console.WriteLine("Song added.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Console.WriteLine($"Songs added: {songs.Count}");

        var playlistLength = CalculatePlaylistLength(songs);

        Console.WriteLine($"Playlist length: {playlistLength[0]}h {playlistLength[1]}m {playlistLength[2]}s");
    }

    private static int[] CalculatePlaylistLength(List<Song> songs)
    {
        var allSeconds = songs.Sum(s => s.SongSeconds) + songs.Sum(s => s.SongMinutes) * 60m;

        var totalMinutes = allSeconds / 60m;

        var hours = totalMinutes / 60m;
        var minutesRemaining = totalMinutes % 60m;

        var minutesToSeconds = minutesRemaining * 60m;
        var secondsRemaining = minutesToSeconds % 60m;

        return new[] { (int)hours, (int)minutesRemaining, (int)secondsRemaining };
    }
}

