using FbonizziMonoGame.PlatformAbstractions;
using System;
using Windows.Storage;

namespace FbonizziMonoGameUWP
{
    /// <summary>
    /// Windows Universal implementation of <see cref="ISettingsRepository"/> using LocalSettings UWP API
    /// </summary>
    public class UwpSettingsRepository : ISettingsRepository
    {
        private T GetOrSetValue<T>(string key, T defaultValue)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(key))
            {
                return (T)localSettings.Values[key];
            }

            SetValue(key, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public bool GetOrSetBool(string key, bool defaultValue)
            => GetOrSetValue(key, defaultValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public DateTime GetOrSetDateTime(string key, DateTime defaultValue)
        {
            long ticksValue = defaultValue.ToBinary();
            return DateTime.FromBinary(GetOrSetValue(key, ticksValue));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetOrSetInt(string key, int defaultValue)
            => GetOrSetValue(key, defaultValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public long GetOrSetLong(string key, long defaultValue)
            => GetOrSetValue(key, defaultValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string GetOrSetString(string key, string defaultValue)
            => GetOrSetValue(key, defaultValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetBool(string key, bool value)
            => SetValue(key, value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetDateTime(string key, DateTime value)
        {
            long ticksValue = value.ToBinary();
            SetValue(key, ticksValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetInt(string key, int value)
            => SetValue(key, value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetLong(string key, long value)
            => SetValue(key, value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetString(string key, string value)
            => SetValue(key, value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetTimeSpan(string key, TimeSpan value)
        {
            long ticksValue = value.Ticks;
            SetValue(key, ticksValue);
        }
    }
}
