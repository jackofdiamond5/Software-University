public class Song
{
    private string name;
    private double length;
    private int songMinutes;
    private int songSeconds;
    private Artist artist;

    public Song(string name, int songMinutes, int songSeconds, Artist artist)
    {
        this.Name = name;
        this.SongMinutes = songMinutes;
        this.SongSeconds = songSeconds;
        this.Artist = artist;
    }

    public string Name
    {
        get => this.name;
        private set
        {
            if (value.Length < 3 || value.Length > 20)
            {
                throw new InvalidSongNameException("Song name should be between 3 and 30 symbols.");
            }

            this.name = value;
        }
    }

    public double Length
    {
        get => this.length;
        private set
        {
            if (this.SongMinutes < 0 || this.SongMinutes > 14 ||
                this.SongSeconds < 0 || this.SongSeconds > 59)
            {
                throw new InvalidSongException("Invalid song.");
            }

            this.length = value;
        }
    }

    public int SongMinutes
    {
        get => this.songMinutes;
        private set
        {
            if (value < 0 || value > 14)
            {
                throw new InvalidSongMinutesException("Song minutes should be between 0 and 14.");
            }

            this.songMinutes = value;
        }
    }

    public int SongSeconds
    {
        get => this.songSeconds;
        private set
        {
            if (value < 0 || value > 59)
            {
                throw new InvalidSongSecondsException("Song seconds should be between 0 and 59.");
            }

            this.songSeconds = value;
        }
    }

    public Artist Artist { get => this.artist; private set => artist = value; }
}

