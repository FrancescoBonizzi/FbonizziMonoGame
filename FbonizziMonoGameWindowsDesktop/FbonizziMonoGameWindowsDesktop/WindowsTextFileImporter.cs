using FbonizziMonogame.Assets;
using System.IO;

namespace FbonizziMonoGameWindowsDesktop
{
    public class WindowsTextFileImporter : ITextFileLoader
    {
        public string LoadFile(string filePath)
          => File.ReadAllText(filePath);
    }
}
