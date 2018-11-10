using FbonizziMonogame;
using System;
using System.Collections.Generic;

namespace FbonizziMonoGameWindowsDesktop
{
    public class TransientWindowsSettingsRepository : ISettingsRepository
    {
        private Dictionary<string, object> _storage = new Dictionary<string, object>();

        public bool GetOrSetBool(string key, bool defaultValue)
        {
            if (_storage.ContainsKey(key))
                return (bool)_storage[key];

            _storage.Add(key, defaultValue);
            return defaultValue;
        }

        public int GetOrSetInt(string key, int defaultValue)
        {
            if (_storage.ContainsKey(key))
                return (int)_storage[key];

            _storage.Add(key, defaultValue);
            return defaultValue;
        }

        public long GetOrSetLong(string key, long defaultValue)
        {
            if (_storage.ContainsKey(key))
                return (long)_storage[key];

            _storage.Add(key, defaultValue);
            return defaultValue;
        }

        public string GetOrSetString(string key, string defaultValue)
        {
            if (_storage.ContainsKey(key))
                return (string)_storage[key];

            _storage.Add(key, defaultValue);
            return defaultValue;
        }

        public TimeSpan GetOrSetTimeSpan(string key, TimeSpan defaultValue)
        {
            if (_storage.ContainsKey(key))
                return (TimeSpan)_storage[key];

            _storage.Add(key, defaultValue);
            return defaultValue;
        }

        public DateTime GetOrSetDateTime(string key, DateTime defaultValue)
        {
            if (_storage.ContainsKey(key))
                return (DateTime)_storage[key];

            _storage.Add(key, defaultValue);
            return defaultValue;
        }

        public void SetBool(string key, bool value)
        {
            if (_storage.ContainsKey(key))
                _storage[key] = value;
            else
                _storage.Add(key, value);
        }

        public void SetInt(string key, int value)
        {
            if (_storage.ContainsKey(key))
                _storage[key] = value;
            else
                _storage.Add(key, value);
        }

        public void SetLong(string key, long value)
        {
            if (_storage.ContainsKey(key))
                _storage[key] = value;
            else
                _storage.Add(key, value);
        }

        public void SetString(string key, string value)
        {
            if (_storage.ContainsKey(key))
                _storage[key] = value;
            else
                _storage.Add(key, value);
        }

        public void SetTimeSpan(string key, TimeSpan value)
        {
            if (_storage.ContainsKey(key))
                _storage[key] = value;
            else
                _storage.Add(key, value);
        }

        public void SetDateTime(string key, DateTime value)
        {
            if (_storage.ContainsKey(key))
                _storage[key] = value;
            else
                _storage.Add(key, value);
        }
    }
}
