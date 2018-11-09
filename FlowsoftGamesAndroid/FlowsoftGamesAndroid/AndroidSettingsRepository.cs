namespace DaVinci_Android
{
    using Android.Content;
    using Android.Preferences;
    using FlowsoftGamesMonogame;
    using System;

    public class AndroidSettingsRepository : ISettingsRepository
    {
        private readonly ISharedPreferences _settings;

        public AndroidSettingsRepository(Context context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            _settings = PreferenceManager.GetDefaultSharedPreferences(context);
        }

        public bool GetOrSetBool(string key, bool defaultValue)
        {
            if (_settings.All.ContainsKey(key))
                return _settings.GetBoolean(key, defaultValue);

            SetBool(key, defaultValue);
            return defaultValue;
        }

        public int GetOrSetInt(string key, int defaultValue)
        {
            if (_settings.All.ContainsKey(key))
                return _settings.GetInt(key, defaultValue);

            SetInt(key, defaultValue);
            return defaultValue;
        }

        public long GetOrSetLong(string key, long defaultValue)
        {
            if (_settings.All.ContainsKey(key))
                return _settings.GetLong(key, defaultValue);

            SetLong(key, defaultValue);
            return defaultValue;
        }

        public string GetOrSetString(string key, string defaultValue)
        {
            if (_settings.All.ContainsKey(key))
                return _settings.GetString(key, defaultValue);

            SetString(key, defaultValue);
            return defaultValue;
        }

        public TimeSpan GetOrSetTimeSpan(string key, TimeSpan defaultValue)
        {
            long ticksValue = defaultValue.Ticks;

            if (_settings.All.ContainsKey(key))
                return TimeSpan.FromTicks(_settings.GetLong(key, ticksValue));

            SetTimeSpan(key, defaultValue);
            return defaultValue;
        }

        public DateTime GetOrSetDateTime(string key, DateTime defaultValue)
        {
            long ticksValue = defaultValue.ToBinary();

            if (_settings.All.ContainsKey(key))
                return DateTime.FromBinary(_settings.GetLong(key, ticksValue));

            SetDateTime(key, defaultValue);
            return defaultValue;
        }

        public void SetInt(string key, int value)
        {
            var editor = _settings.Edit();
            editor.PutInt(key, value);
            editor.Commit();
        }

        public void SetBool(string key, bool value)
        {
            var editor = _settings.Edit();
            editor.PutBoolean(key, value);
            editor.Commit();
        }

        public void SetLong(string key, long value)
        {
            var editor = _settings.Edit();
            editor.PutLong(key, value);
            editor.Commit();
        }

        public void SetString(string key, string value)
        {
            var editor = _settings.Edit();
            editor.PutString(key, value);
            editor.Commit();
        }

        public void SetTimeSpan(string key, TimeSpan value)
        {
            long ticksValue = value.Ticks;
            SetLong(key, ticksValue);
        }

        public void SetDateTime(string key, DateTime value)
        {
            long ticksValue = value.ToBinary();
            SetLong(key, ticksValue);
        }
    }
}
