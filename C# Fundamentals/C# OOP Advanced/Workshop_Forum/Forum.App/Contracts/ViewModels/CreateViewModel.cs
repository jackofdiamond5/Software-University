namespace Forum.App.Contracts.ViewModels
{
    using System.Linq;
    using System.Collections.Generic;

    public class CreateViewModel
    {
        private const int lineLength = 37;

        public CreateViewModel(string content)
        {
            this.Content = this.GetLines(content);
        }

        public string[] Content { get; }

        private string[] GetLines(string content)
        {
            var contentChars = content.ToCharArray();

            var lines = new List<string>();

            for(var i = 0; i < content.Length; i += lineLength)
            {
                var row = contentChars.Skip(i).Take(lineLength).ToArray();
                var rowString = string.Join("", row);
                lines.Add(rowString);
            }

            return lines.ToArray();
        }
    }
}
