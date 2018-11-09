using FlowsoftGamesMonogame.Assets;
using System.IO;

namespace FlowsoftGamesUWP
{
    public class UWPTextFileImporter : ITextFileLoader
    {
        public string LoadFile(string filePath)
            => File.ReadAllText(filePath);
    }
}
