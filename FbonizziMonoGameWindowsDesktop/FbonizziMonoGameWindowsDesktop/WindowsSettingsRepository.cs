using FbonizziMonogame;
using System;
using System.Collections.Generic;
using System.IO;

namespace FbonizziMonoGameWindowsDesktop
{
    public class WindowsSettingsRepository : ISettingsRepository
    {
        private const string _fileName = "gameSettings.txt";
        private Dictionary<string, string> _storage;

        public WindowsSettingsRepository()
        {
            if (File.Exists(_fileName))
            {
                Deserialize(File.ReadAllLines(_fileName));
            }
            else
            {
                _storage = new Dictionary<string, string>();
            }
        }

        private string Serialize()
        {
            string serializedString = string.Empty;

            foreach (var setting in _storage)
            {
                serializedString += $"{setting.Key}:{setting.Value}{Environment.NewLine}";
            }

            return serializedString;
        }

        private void Deserialize(string[] data)
        {
            _storage = new Dictionary<string, string>();

            foreach (var setting in data)
            {
                var splittedSetting = setting.Split(':');
                var key = splittedSetting[0];
                var value = splittedSetting[1];

                _storage.Add(key, value);
            }
        }

        private void Save()
            => File.WriteAllText(_fileName, Serialize());

        public bool GetOrSetBool(string key, bool defaultValue)
        {
            if (_storage.ContainsKey(key))
                return Convert.ToBoolean(_storage[key] == "1" ? true : false);

            _storage[key] = FromBool(defaultValue);
            Save();
            return defaultValue;
        }

        public DateTime GetOrSetDateTime(string key, DateTime defaultValue)
        {
            if (_storage.ContainsKey(key))
                return DateTime.FromBinary(Convert.ToInt64(_storage[key]));

            _storage[key] = defaultValue.ToBinary().ToString();
            Save();
            return defaultValue;
        }

        public int GetOrSetInt(string key, int defaultValue)
        {
            if (_storage.ContainsKey(key))
                return Convert.ToInt32(_storage[key]);

            _storage[key] = defaultValue.ToString();
            Save();
            return defaultValue;
        }

        public long GetOrSetLong(string key, long defaultValue)
        {
            if (_storage.ContainsKey(key))
                return Convert.ToInt64(_storage[key]);

            _storage[key] = defaultValue.ToString();
            Save();
            return defaultValue;
        }

        public string GetOrSetString(string key, string defaultValue)
        {
            if (_storage.ContainsKey(key))
                return _storage[key];

            _storage[key] = defaultValue;
            Save();
            return defaultValue;
        }

        public TimeSpan GetOrSetTimeSpan(string key, TimeSpan defaultValue)
        {
            if (_storage.ContainsKey(key))
                return TimeSpan.FromTicks(GetOrSetLong(key, defaultValue.Ticks));

            SetTimeSpan(key, defaultValue);
            Save();
            return defaultValue;
        }

        private string FromBool(bool value)
            => value ? "1" : "0";

        public void SetBool(string key, bool value)
        {
            _storage[key] = FromBool(value);
            Save();
        }

        public void SetDateTime(string key, DateTime value)
        {
            SetDateTime(key, value);
            Save();
        }

        public void SetInt(string key, int value)
        {
            _storage[key] = value.ToString();
            Save();
        }

        public void SetLong(string key, long value)
        {
            _storage[key] = value.ToString();
            Save();
        }

        public void SetString(string key, string value)
        {
            _storage[key] = value;
            Save();
        }

        public void SetTimeSpan(string key, TimeSpan value)
        {
            SetLong(key, value.Ticks);
            Save();
        }
    }
}
