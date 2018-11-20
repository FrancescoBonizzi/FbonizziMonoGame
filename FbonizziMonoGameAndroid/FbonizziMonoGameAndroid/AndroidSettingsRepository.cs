using Android.Content;
using Android.Preferences;
using FbonizziMonoGame.PlatformAbstractions;
using System;

namespace FbonizziMonoGameAndroid
{
    /// <summary>
    /// Android Xamarin implementation of <see cref="ISettingsRepository"/>
    /// </summary>
    public class AndroidSettingsRepository : ISettingsRepository
    {
        private readonly ISharedPreferences _settings;

        /// <summary>
        /// Android Xamarin implementation of <see cref="ISettingsRepository"/>
        /// </summary>
        /// <param name="context"></param>
        public AndroidSettingsRepository(Context context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            _settings = PreferenceManager.GetDefaultSharedPreferences(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public bool GetOrSetBool(string key, bool defaultValue)
        {
            if (_settings.All.ContainsKey(key))
                return _settings.GetBoolean(key, defaultValue);

            SetBool(key, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetOrSetInt(string key, int defaultValue)
        {
            if (_settings.All.ContainsKey(key))
                return _settings.GetInt(key, defaultValue);

            SetInt(key, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public long GetOrSetLong(string key, long defaultValue)
        {
            if (_settings.All.ContainsKey(key))
                return _settings.GetLong(key, defaultValue);

            SetLong(key, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string GetOrSetString(string key, string defaultValue)
        {
            if (_settings.All.ContainsKey(key))
                return _settings.GetString(key, defaultValue);

            SetString(key, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public TimeSpan GetOrSetTimeSpan(string key, TimeSpan defaultValue)
        {
            long ticksValue = defaultValue.Ticks;

            if (_settings.All.ContainsKey(key))
                return TimeSpan.FromTicks(_settings.GetLong(key, ticksValue));

            SetTimeSpan(key, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public DateTime GetOrSetDateTime(string key, DateTime defaultValue)
        {
            long ticksValue = defaultValue.ToBinary();

            if (_settings.All.ContainsKey(key))
                return DateTime.FromBinary(_settings.GetLong(key, ticksValue));

            SetDateTime(key, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetInt(string key, int value)
        {
            var editor = _settings.Edit();
            editor.PutInt(key, value);
            editor.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetBool(string key, bool value)
        {
            var editor = _settings.Edit();
            editor.PutBoolean(key, value);
            editor.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetLong(string key, long value)
        {
            var editor = _settings.Edit();
            editor.PutLong(key, value);
            editor.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetString(string key, string value)
        {
            var editor = _settings.Edit();
            editor.PutString(key, value);
            editor.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetTimeSpan(string key, TimeSpan value)
        {
            long ticksValue = value.Ticks;
            SetLong(key, ticksValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetDateTime(string key, DateTime value)
        {
            long ticksValue = value.ToBinary();
            SetLong(key, ticksValue);
        }
    }
}
