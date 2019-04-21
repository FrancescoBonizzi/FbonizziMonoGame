using FbonizziMonoGameWindowsDesktop;
using System;

namespace Playground
{
    /// <summary>
    /// A simple application to manually try some library features
    /// </summary>
    internal static class Program
    {
        private static void Main()
        {
            var settingsRepository = new FileWindowsSettingsRepository("test.txt");

            settingsRepository.GetOrSetBool("test", true);
            Console.WriteLine(settingsRepository.GetOrSetBool("test", true));
            settingsRepository.GetOrSetBool("test2", false);
            Console.WriteLine(settingsRepository.GetOrSetBool("test2", false));

            settingsRepository.GetOrSetTimeSpan("timespan1", TimeSpan.FromSeconds(123));
            Console.WriteLine(settingsRepository.GetOrSetTimeSpan("timespan1", TimeSpan.FromSeconds(123)));

            settingsRepository.GetOrSetDateTime("data1", DateTime.Now);
            Console.WriteLine(settingsRepository.GetOrSetDateTime("data1", DateTime.Now));

            Console.Read();
        }
    }
}
