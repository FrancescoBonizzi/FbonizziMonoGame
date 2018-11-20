using Android.Content.Res;
using FbonizziMonoGame.PlatformAbstractions;
using System;
using System.IO;

namespace FbonizziMonoGameAndroid
{
    /// <summary>
    /// Android Xamarin implementation of <see cref="ITextFileLoader"/>
    /// </summary>
    public class AndroidTextFileImporter : ITextFileLoader
    {
        private readonly AssetManager _assets;

        /// <summary>
        /// Android Xamarin implementation of <see cref="ITextFileLoader"/>
        /// </summary>
        /// <param name="assets"></param>
        public AndroidTextFileImporter(AssetManager assets)
        {
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
        }

        /// <summary>
        /// Loads a text file from path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string LoadFile(string filePath)
        {
            using (var fileStream = _assets.Open(filePath))
            using (var streamReader = new StreamReader(fileStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
