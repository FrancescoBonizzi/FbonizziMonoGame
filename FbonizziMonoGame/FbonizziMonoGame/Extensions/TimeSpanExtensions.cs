﻿using System;

namespace FbonizziMonogame.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToMinuteSecondsFormat(this TimeSpan timeSpan)
            => timeSpan.ToString(@"mm\:ss");
    }
}