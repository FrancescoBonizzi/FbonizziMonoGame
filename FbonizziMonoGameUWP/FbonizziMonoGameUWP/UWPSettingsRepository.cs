using FbonizziMonogame;
using System;
using Windows.Storage;

namespace FbonizziMonoGameUWP
{
    public class UWPSettingsRepository : ISettingsRepository
    {
        private T GetOrSetValue<T>(string key, T defaultValue)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(key))
                return (T)localSettings.Values[key];

            SetValue(key, defaultValue);
            return defaultValue;
        }

        public bool GetOrSetBool(string key, bool defaultValue)
            => GetOrSetValue(key, defaultValue);

        public DateTime GetOrSetDateTime(string key, DateTime defaultValue)
        {
            long ticksValue = defaultValue.ToBinary();
            return DateTime.FromBinary(GetOrSetValue(key, ticksValue));
        }

        public int GetOrSetInt(string key, int defaultValue)
            => GetOrSetValue(key, defaultValue);

        public long GetOrSetLong(string key, long defaultValue)
            => GetOrSetValue(key, defaultValue);

        public string GetOrSetString(string key, string defaultValue)
            => GetOrSetValue(key, defaultValue);

        public TimeSpan GetOrSetTimeSpan(string key, TimeSpan defaultValue)
        {
            long ticksValue = defaultValue.Ticks;
            return TimeSpan.FromTicks(GetOrSetValue(key, ticksValue));
        }

        private void SetValue<T>(string key, T value)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[key] = value;
        }

        public void SetBool(string key, bool value)
            => SetValue(key, value);

        public void SetDateTime(string key, DateTime value)
        {
            long ticksValue = value.ToBinary();
            SetValue(key, ticksValue);
        }

        public void SetInt(string key, int value)
            => SetValue(key, value);

        public void SetLong(string key, long value)
            => SetValue(key, value);

        public void SetString(string key, string value)
            => SetValue(key, value);

        public void SetTimeSpan(string key, TimeSpan value)
        {
            long ticksValue = value.Ticks;
            SetValue(key, ticksValue);
        }
    }
}
