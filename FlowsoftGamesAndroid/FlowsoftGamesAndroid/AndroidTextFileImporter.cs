namespace DaVinci_Android
{
    using Android.Content.Res;
    using FlowsoftGamesMonogame.Assets;
    using System.IO;

    public class AndroidTextFileImporter : ITextFileLoader
    {
        private AssetManager _assets;

        public AndroidTextFileImporter(AssetManager assets)
        {
            _assets = assets ?? throw new System.ArgumentNullException(nameof(assets));
        }

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
