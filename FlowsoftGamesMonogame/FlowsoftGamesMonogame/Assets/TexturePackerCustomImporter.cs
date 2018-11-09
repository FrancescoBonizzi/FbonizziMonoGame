using System;
using System.Collections.Generic;
using System.Linq;

namespace FbonizziGames.Assets
{
    public class TexturePackerCustomImporter
    {
        private ITextFileLoader _txtFileImporter;

        public TexturePackerCustomImporter(ITextFileLoader txtFileImporter)
        {
            _txtFileImporter = txtFileImporter ?? throw new ArgumentNullException(nameof(txtFileImporter));
        }

        public IDictionary<string, SpriteDescription> Import(string spriteSheetDescriptionFilePath)
        {
            if (string.IsNullOrWhiteSpace(spriteSheetDescriptionFilePath))
                throw new ArgumentNullException(nameof(spriteSheetDescriptionFilePath));

            var spriteSheetDescription = _txtFileImporter.LoadFile(spriteSheetDescriptionFilePath);

            return _txtFileImporter
                .LoadFile(spriteSheetDescriptionFilePath)
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => ParseLine(s))
                .ToDictionary(
                    s => s.Name,
                    s => s);
        }

        private static SpriteDescription ParseLine(string spriteInfoLine)
        {
            // FlowosoftGamesExporter
            // nome|x|y|w|h

            var splittedRow = spriteInfoLine.Split('|');
            return new SpriteDescription()
            {
                Name = splittedRow[0],
                X = Convert.ToInt32(splittedRow[1]),
                Y = Convert.ToInt32(splittedRow[2]),
                Width = Convert.ToInt32(splittedRow[3]),
                Height = Convert.ToInt32(splittedRow[4]),
            };
        }
    }
}
