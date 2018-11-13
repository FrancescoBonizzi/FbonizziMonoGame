﻿using FbonizziMonoGame.Assets.Abstractions;
using FbonizziMonoGame.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FbonizziMonoGame.Assets
{
    /// <summary>
    /// Service that generates a dictionary of SpriteName, SpriteDescription
    /// given a text file where each row defines a single sprite with format: name|x|y|w|h
    /// </summary>
    public class CustomSpriteImporter : ISpriteImporter
    {
        private readonly ITextFileLoader _txtFileImporter;

        /// <summary>
        /// An implementation of a simple sprite importer given a file
        /// </summary>
        /// <param name="txtFileImporter">A text file loader</param>
        public CustomSpriteImporter(ITextFileLoader txtFileImporter)
        {
            _txtFileImporter = txtFileImporter ?? throw new ArgumentNullException(nameof(txtFileImporter));
        }

        /// <summary>
        /// Loads synchronously the file and returns a dictionary of sprites
        /// </summary>
        /// <param name="spriteSheetDescriptionFilePath"></param>
        /// <returns></returns>
        public IDictionary<string, SpriteDescription> Import(string spriteSheetDescriptionFilePath)
        {
            if (string.IsNullOrWhiteSpace(spriteSheetDescriptionFilePath))
                throw new ArgumentNullException(nameof(spriteSheetDescriptionFilePath));

            return _txtFileImporter
                .LoadFile(spriteSheetDescriptionFilePath)
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => ParseLine(s))
                .ToDictionary(
                    s => s.Name,
                    s => s);
        }

        /// <summary>
        /// It needs a file where each row is: name|x|y|w|h
        /// </summary>
        /// <param name="spriteInfoLine"></param>
        /// <returns></returns>
        private static SpriteDescription ParseLine(string spriteInfoLine)
        { 
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
