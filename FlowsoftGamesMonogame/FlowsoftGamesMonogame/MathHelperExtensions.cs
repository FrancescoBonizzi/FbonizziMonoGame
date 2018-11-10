using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace FbonizziMonogame
{
    /// <summary>
    /// Funzioni utili a livello matematico
    /// </summary>
    public static class MathHelperExtensions
    {
        private static readonly Random Rand = new Random();

        /// <summary>
        /// Interpolazione lineare tra due colori
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static Color Lerp(this Color value1, Color value2, float amount)
        {
            amount = MathHelper.Clamp(amount, 0, 1);
            return new Color()
            {
                R = (byte)MathHelper.Lerp(value1.R, value2.R, amount),
                G = (byte)MathHelper.Lerp(value1.G, value2.G, amount),
                B = (byte)MathHelper.Lerp(value1.B, value2.B, amount),
                A = (byte)MathHelper.Lerp(value1.A, value2.A, amount)
            };
        }

        /// <summary>
        /// Mappa un valore da un intervallo ad un altro, dati l'intervallo di input e quello di output
        /// </summary>
        /// <param name="value">Valore da mappare</param>
        /// <param name="A">Minimo dell'intervallo di input</param>
        /// <param name="B">Massimo dell'intervallo di input</param>
        /// <param name="newA">Minimo dell'intervallo di output</param>
        /// <param name="newB">Massimo dell'intervallo di output</param>
        /// <returns></returns>
        public static float MapValueFromIntervalToInterval(float value, float A, float B, float newA, float newB)
        {
            // Divisione per zero!
            // (B - A) == 0

            return newA + ((newB - newA) / (B - A)) * (value - A);
        }

        /// <summary>
        /// Mappa un valore da un intervallo ad un altro, dati l'intervallo di input e la pendenza.
        /// </summary>
        /// <param name="value">Valore da mappare</param>
        /// <param name="A">Minimo dell'intervallo di input</param>
        /// <param name="newA">Minimo dell'intervallo di output</param>
        /// <param name="slope">Pendenza: (newB - newA) / (B - A)</param>
        /// <returns></returns>
        public static float MapValueFromIntervalToInterval(float value, float A, float newA, float slope)
            => newA + slope * (value - A);

        /// <summary>
        /// Estensione di IList che permette di mescolare nel modo più efficiente possibile una lista.
        /// Implementa Fisher-Yeats shuffle
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Genera un numero decimale casuale
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float RandomBetween(float min, float max)
            => min + (float)Rand.NextDouble() * (max - min);
        
        /// <summary>
        /// Genera un numero decimale casuale
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double RandomBetween(double min, double max)
            => min + Rand.NextDouble() * (max - min);

        /// <summary>
        /// Genera un numero intero casuale
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int RandomBetween(int min, int max)
            => Rand.Next(min, max);

        public static TimeSpan RandomBetween(TimeSpan min, TimeSpan max)
            => TimeSpan.FromSeconds(RandomBetween(min.TotalSeconds, max.TotalSeconds));

        /// <summary>
        /// Calcola un valore secondo la funzione Seno che varia nel tempo
        /// </summary>
        /// <param name="overTimeValue"></param>
        /// <param name="minDelta"></param>
        /// <param name="maxDelta"></param>
        /// <returns></returns>
        public static float GenerateDeltaOverTimeSin(
            float overTimeValue, float minDelta, float maxDelta)
            => MapValueFromIntervalToInterval(
                (float)Math.Sin(overTimeValue),
                -1.0f, +1.0f,
                minDelta, maxDelta);

        /// <summary>
        /// Calcola un valore secondo la funzione Coseno che varia nel tempo
        /// </summary>
        /// <param name="overTimeValue"></param>
        /// <param name="minDelta"></param>
        /// <param name="maxDelta"></param>
        /// <returns></returns>
        public static float GenerateDeltaOverTimeCos(
            float overTimeValue, float minDelta, float maxDelta)
            => MapValueFromIntervalToInterval(
                (float)Math.Cos(overTimeValue),
                -1.0f, +1.0f,
                minDelta, maxDelta);
    }
}
