using FbonizziMonoGame.PlatformAbstractions;
using System.IO;

namespace FbonizziMonoGameUWP
{
    /// <summary>
    /// UWP implementation of <see cref="ITextFileLoader"/>
    /// </summary>
    public class UWPTextFileImporter : ITextFileLoader
    {
        /// <summary>
        /// Loads a text file from path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string LoadFile(string filePath)
            => File.ReadAllText(filePath);
    }
}
