using System;

namespace FbonizziMonoGame
{
    /// <summary>
    /// Abstraction to provide methods to open a web page
    /// </summary>
    public interface IWebPageOpener
    {
        /// <summary>
        /// Opens a web page
        /// </summary>
        /// <param name="uri"></param>
        void OpenWebpage(Uri uri);
    }
}
