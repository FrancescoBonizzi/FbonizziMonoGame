using FbonizziMonogame.Assets;
using System.IO;

namespace FbonizziMonoGameUWP
{
    public class UWPTextFileImporter : ITextFileLoader
    {
        public string LoadFile(string filePath)
            => File.ReadAllText(filePath);
    }
}
