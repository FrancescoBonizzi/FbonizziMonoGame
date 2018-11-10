using System;

namespace FbonizziMonogame
{
    public interface ISettingsRepository
    {
        string GetOrSetString(string key, string defaultValue);
        void SetString(string key, string value);

        long GetOrSetLong(string key, long defaultValue);
        void SetLong(string key, long value);

        int GetOrSetInt(string key, int defaultValue);
        void SetInt(string key, int value);

        bool GetOrSetBool(string key, bool defaultValue);
        void SetBool(string key, bool value);

        TimeSpan GetOrSetTimeSpan(string key, TimeSpan defaultValue);
        void SetTimeSpan(string key, TimeSpan value);

        DateTime GetOrSetDateTime(string key, DateTime defaultValue);
        void SetDateTime(string key, DateTime value);
    }
}
