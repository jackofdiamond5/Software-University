public class Artist
{
    private string name;

    public Artist(string name)
    {
        this.Name = name;
    }

    public string Name
    {
        get => this.name;
        private set
        {
            if (value.Length < 3 || value.Length > 20)
            {
                throw new InvalidArtistNameException("Artist name should be between 3 and 20 symbols.");
            }

            this.name = value;
        }
    }
}

