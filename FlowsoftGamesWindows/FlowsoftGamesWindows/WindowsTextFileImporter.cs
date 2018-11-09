using FbonizziGames.Assets;
using System.IO;

namespace FlowsoftGamesWindows
{
    public class WindowsTextFileImporter : ITextFileLoader
    {
        public string LoadFile(string filePath)
          => File.ReadAllText(filePath);
    }
}
