using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

namespace UnityPrefsEx.Runtime.prefs_ex.Scripts.Runtime.Utils
{
    public static class PlayerPrefsEx
    {
        private static readonly DateTimeFormat DateFormat = new DateTimeFormat("dd-MM-yyyy hh:mm:ss.SSS");

        #region Events

        public static event EventHandler<PlayerPrefsChangeEventArgs> OnChanged;

        #endregion

        private static readonly IDictionary<string, DynamicCache> _dynamicCache = new Dictionary<string, DynamicCache>();

        public static bool GetBool(string key, bool def, params string[] oldKeys) =>
            GetValue(key, def, oldKeys, (k, d) => PlayerPrefs.GetInt(k, d ? 1 : 0) != 0);

        public static bool[] GetBool(string key, bool[] def, params string[] oldKeys) =>
            GetArrayValue(key, def, oldKeys, (k, d) => PlayerPrefs.GetInt(k, d ? 1 : 0) != 0);

        public static void SetBool(string key, bool val, bool autoSave = false) =>
            SetValue(key, autoSave, PlayerPrefsDataType.Boolean, () => PlayerPrefs.SetInt(key, val ? 1 : 0));

        public static void SetBool(string key, bool[] val, bool autoSave = false) =>
            SetArrayValue(key, val.Length, autoSave, PlayerPrefsDataType.Boolean, (k, i) => PlayerPrefs.SetInt(k, val[i] ? 1 : 0));

        public static int GetInt(string key, int def, params string[] oldKeys) =>
            GetValue(key, def, oldKeys, PlayerPrefs.GetInt);

        public static int[] GetInt(string key, int[] def, params string[] oldKeys) =>
            GetArrayValue(key, def, oldKeys, PlayerPrefs.GetInt);

        public static void SetInt(string key, int val, bool autoSave = false) =>
            SetValue(key, autoSave, PlayerPrefsDataType.Int, () => PlayerPrefs.SetInt(key, val));

        public static void SetInt(string key, int[] val, bool autoSave = false) =>
            SetArrayValue(key, val.Length, autoSave, PlayerPrefsDataType.Int, (k, i) => PlayerPrefs.SetInt(k, val[i]));

        public static long GetLong(string key, long def, params string[] oldKeys) =>
            GetValue(key, def, oldKeys, (k, d) => PlayerPrefs.HasKey(k) ? long.Parse(PlayerPrefs.GetString(k)) : d);

        public static long[] GetLong(string key, long[] def, params string[] oldKeys) =>
            GetArrayValue(key, def, oldKeys, (k, d) => PlayerPrefs.HasKey(k) ? long.Parse(PlayerPrefs.GetString(k)) : d);

        public static void SetLong(string key, long val, bool autoSave = false) =>
            SetValue(key, autoSave, PlayerPrefsDataType.Long, () => PlayerPrefs.SetString(key, val.ToString()));

        public static void SetLong(string key, long[] val, bool autoSave = false) =>
            SetArrayValue(key, val.Length, autoSave, PlayerPrefsDataType.Long, (k, i) => PlayerPrefs.SetString(k, val[i].ToString()));

        public static float GetFloat(string key, float def, params string[] oldKeys) =>
            GetValue(key, def, oldKeys, PlayerPrefs.GetFloat);

        public static float[] GetFloat(string key, float[] def, params string[] oldKeys) =>
            GetArrayValue(key, def, oldKeys, PlayerPrefs.GetFloat);

        public static void SetFloat(string key, float val, bool autoSave = false) =>
            SetValue(key, autoSave, PlayerPrefsDataType.Float, () => PlayerPrefs.SetFloat(key, val));

        public static void SetFloat(string key, float[] val, bool autoSave = false) =>
            SetArrayValue(key, val.Length, autoSave, PlayerPrefsDataType.Float, (k, i) => PlayerPrefs.SetFloat(k, val[i]));

        public static double GetDouble(string key, double def, params string[] oldKeys) =>
            GetValue(key, def, oldKeys, (s, d) => PlayerPrefs.HasKey(s) ? double.Parse(PlayerPrefs.GetString(s), CultureInfo.InvariantCulture) : d);

        public static double[] GetDouble(string key, double[] def, params string[] oldKeys) =>
            GetArrayValue(key, def, oldKeys, (s, d) => PlayerPrefs.HasKey(s) ? double.Parse(PlayerPrefs.GetString(s), CultureInfo.InvariantCulture) : d);

        public static void SetDouble(string key, double val, bool autoSave = false) =>
            SetValue(key, autoSave, PlayerPrefsDataType.Double, () => PlayerPrefs.SetString(key, val.ToString(CultureInfo.InvariantCulture)));

        public static void SetDouble(string key, double[] val, bool autoSave = false) =>
            SetArrayValue(key, val.Length, autoSave, PlayerPrefsDataType.Double, (k, i) => PlayerPrefs.SetString(k, val[i].ToString(CultureInfo.InvariantCulture)));

        public static string GetString(string key, string def, params string[] oldKeys) =>
            GetValue(key, def, oldKeys, PlayerPrefs.GetString);

        public static string[] GetString(string key, string[] def, params string[] oldKeys) =>
            GetArrayValue(key, def, oldKeys, PlayerPrefs.GetString);

        public static void SetString(string key, string val, bool autoSave = false) =>
            SetValue(key, autoSave, PlayerPrefsDataType.String, () => PlayerPrefs.SetString(key, val));

        public static void SetString(string key, string[] val, bool autoSave = false) =>
            SetArrayValue(key, val.Length, autoSave, PlayerPrefsDataType.String, (k, i) => PlayerPrefs.SetString(k, val[i]));

        public static char GetCharacter(string key, char def, params string[] oldKeys) =>
            GetValue(key, def, oldKeys, (s, c) => PlayerPrefs.HasKey(s) ? PlayerPrefs.GetString(s)[0] : c);

        public static char[] GetCharacter(string key, char[] def, params string[] oldKeys) =>
            GetArrayValue(key, def, oldKeys, (s, c) => PlayerPrefs.HasKey(s) ? PlayerPrefs.GetString(s)[0] : c);

        public static void SetCharacter(string key, char val, bool autoSave = false) =>
            SetValue(key, autoSave, PlayerPrefsDataType.Character, () => PlayerPrefs.SetString(key, val.ToString()));

        public static void SetCharacter(string key, char[] val, bool autoSave = false) =>
            SetArrayValue(key, val.Length, autoSave, PlayerPrefsDataType.Character, (k, i) => PlayerPrefs.SetString(k, val[i].ToString()));

        public static byte[] GetBytes(string key, byte[] def, params string[] oldKeys) =>
            Convert.FromBase64String(GetValue(key, Convert.ToBase64String(def ?? Array.Empty<byte>()), oldKeys, PlayerPrefs.GetString));

        public static byte[][] GetBytes(string key, byte[][] def, params string[] oldKeys) =>
            GetArrayValue(key, def.Select(Convert.ToBase64String).ToArray(), oldKeys, PlayerPrefs.GetString)
                .Select(Convert.FromBase64String)
                .ToArray();

        public static void SetBytes(string key, byte[] val, bool autoSave = false) =>
            SetValue(key, autoSave, PlayerPrefsDataType.Binary, () => PlayerPrefs.SetString(key, Convert.ToBase64String(val)));

        public static void SetBytes(string key, byte[][] val, bool autoSave = false) =>
            SetArrayValue(key, val.Length, autoSave, PlayerPrefsDataType.Binary, (k, i) => PlayerPrefs.SetString(k, Convert.ToBase64String(val[i])));

        public static DateTime GetDateTime(string key, DateTime def, params string[] oldKeys) =>
            DateTime.Parse(GetValue(key, def.ToString(DateFormat.FormatProvider), oldKeys, PlayerPrefs.GetString), DateFormat.FormatProvider);

        public static DateTime[] GetDateTime(string key, DateTime[] def, params string[] oldKeys) =>
            GetArrayValue(key, def.Select(x => x.ToString(DateFormat.FormatProvider)).ToArray(), oldKeys, PlayerPrefs.GetString)
                .Select(x => DateTime.Parse(x, DateFormat.FormatProvider))
                .ToArray();

        public static void SetDateTime(string key, DateTime val, bool autoSave = false) =>
            SetValue(key, autoSave, PlayerPrefsDataType.DateTime, () => PlayerPrefs.SetString(key, val.ToString(DateFormat.FormatProvider)));

        public static void SetDateTime(string key, DateTime[] val, bool autoSave = false) =>
            SetArrayValue(key, val.Length, autoSave, PlayerPrefsDataType.DateTime, (k, i) => PlayerPrefs.SetString(k, val[i].ToString(DateFormat.FormatProvider)));

        public static TimeSpan GetTimeSpan(string key, TimeSpan def, params string[] oldKeys) =>
            TimeSpan.Parse(GetValue(key, def.ToString(), oldKeys, PlayerPrefs.GetString));

        public static TimeSpan[] GetTimeSpan(string key, TimeSpan[] def, params string[] oldKeys) =>
            GetArrayValue(key, def.Select(x => x.ToString()).ToArray(), oldKeys, PlayerPrefs.GetString)
                .Select(TimeSpan.Parse)
                .ToArray();

        public static void SetTimeSpan(string key, TimeSpan val, bool autoSave = false) =>
            SetValue(key, autoSave, PlayerPrefsDataType.TimeSpan, () => PlayerPrefs.SetString(key, val.ToString()));

        public static void SetTimeSpan(string key, TimeSpan[] val, bool autoSave = false) =>
            SetArrayValue(key, val.Length, autoSave, PlayerPrefsDataType.TimeSpan, (k, i) => PlayerPrefs.SetString(k, val[i].ToString()));

        public static T GetEnum<T>(string key, T def, params string[] oldKeys) where T : Enum
        {
            var s = GetValue(key, def.ToString(), oldKeys, PlayerPrefs.GetString);
            return (T)Enum.Parse(typeof(T), s);
        }

        public static T[] GetEnum<T>(string key, T[] def, params string[] oldKeys) where T : Enum
        {
            var s = GetArrayValue(key, def.Select(x => x.ToString()).ToArray(), oldKeys, PlayerPrefs.GetString);
            return s.Select(x => (T)Enum.Parse(typeof(T), x)).ToArray();
        }

        public static void SetEnum<T>(string key, T val, bool autoSave = false) where T : Enum =>
            SetValue(key, autoSave, PlayerPrefsDataType.Enum, () => PlayerPrefs.SetString(key, val.ToString()));

        public static void SetEnum<T>(string key, T[] val, bool autoSave = false) where T : Enum =>
            SetArrayValue(key, val.Length, autoSave, PlayerPrefsDataType.Enum, (k, i) => PlayerPrefs.SetString(k, val[i].ToString()));

        private static T GetValue<T>(string key, T def, string[] oldKeys, Func<string, T, T> getter)
        {
            if (oldKeys == null || oldKeys.Length <= 0)
                return getter(key, def);

            var currentDefault = getter(oldKeys.Last(), def);
            for (var i = oldKeys.Length - 2; i >= 0; i--)
            {
                currentDefault = getter(oldKeys[i], currentDefault);
            }

            return getter(key, currentDefault);
        }

        private static T GetValueCached<T>(string key, T def, bool refresh, string[] oldKeys, Func<string, T, T> getter)
        {
            if (!_dynamicCache.ContainsKey(key))
            {
                _dynamicCache.Add(key, new DynamicCache<T>(key, GetValue(key, def, oldKeys, getter)));
            }

            var cache =(DynamicCache<T>) _dynamicCache[key];
            if (refresh)
            {
                cache.Value = GetValue(key, def, oldKeys, getter);
            }

            return cache.Value;
        }

        private static T[] GetArrayValue<T>(string key, T[] def, string[] oldKeys, Func<string, T, T> getter)
        {
            return GetValue(key, def, oldKeys, (key, def) =>
            {
                if (!PlayerPrefs.HasKey(key))
                    return def;

                var size = PlayerPrefs.GetInt(key, 0);
                if (size <= 0)
                    return Array.Empty<T>();

                var result = new T[size];
                for (var i = 0; i < size; i++)
                {
                    result[i] = getter(key + "." + i, default);
                }

                return result;
            });
        }

        public static T[] GetArrayValueCached<T>(string key, T[] def, bool refresh, string[] oldKeys, Func<string, T, T> getter)
        {
            if (!_dynamicCache.ContainsKey(key))
            {
                _dynamicCache.Add(key, new DynamicCache<T[]>(key, GetArrayValue(key, def, oldKeys, getter)));
            }

            var cache =(DynamicCache<T[]>) _dynamicCache[key];
            if (refresh)
            {
                cache.Value = GetArrayValue(key, def, oldKeys, getter);
            }

            return cache.Value;
        }

        private static void SetValue(string key, bool autoSave, PlayerPrefsDataType type, Action setter)
        {
            setter();
            if (autoSave)
            {
                PlayerPrefs.Save();
            }

            RaiseChange(PlayerPrefsChangeType.AddOrUpdate, type, key);
        }

        private static void SetArrayValue(string key, int size, bool autoSave, PlayerPrefsDataType type, Action<string, int> setter)
        {
            PlayerPrefs.SetInt(key, size);
            for (var i = 0; i < size; i++)
            {
                setter(key + "." + i, i);
            }

            if (autoSave)
            {
                PlayerPrefs.Save();
            }

            RaiseArrayChange(PlayerPrefsChangeType.AddOrUpdate, type, size, key);
        }

        public static bool HasKey(string key) => PlayerPrefs.HasKey(key);

        public static bool HasKeys(KeySearchType type, params string[] keys)
        {
            foreach (var key in keys)
            {
                switch (type)
                {
                    case KeySearchType.All:
                        if (!PlayerPrefs.HasKey(key))
                            return false;
                        break;
                    case KeySearchType.Any:
                        if (PlayerPrefs.HasKey(key))
                            return true;
                        break;
                    case KeySearchType.None:
                        if (PlayerPrefs.HasKey(key))
                            return false;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            switch (type)
            {
                case KeySearchType.All:
                    return true;
                case KeySearchType.Any:
                    return false;
                case KeySearchType.None:
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }

        public static void DeleteKey(string key, bool autoSave = false)
        {
            PlayerPrefs.DeleteKey(key);
            if (autoSave)
            {
                PlayerPrefs.Save();
            }

            RaiseChange(PlayerPrefsChangeType.Delete, PlayerPrefsDataType.Unspecific, key);
        }

        public static void DeleteKeys(params string[] keys)
        {
            DeleteKeys(false, keys);
        }

        public static void DeleteKeys(bool autoSave, params string[] keys)
        {
            foreach (var key in keys)
            {
                PlayerPrefs.DeleteKey(key);
            }

            if (autoSave)
            {
                PlayerPrefs.Save();
            }

            RaiseChange(PlayerPrefsChangeType.Delete, PlayerPrefsDataType.Unspecific, keys);
        }

        public static void DeleteArrayKey(string key, bool autoSave = false)
        {
            var size = PlayerPrefs.GetInt(key);
            PlayerPrefs.DeleteKey(key);
            for (var i = 0; i < size; i++)
            {
                PlayerPrefs.DeleteKey(key + "." + i);
            }

            if (autoSave)
            {
                PlayerPrefs.Save();
            }
        }

        public static void DeleteAll(bool autoSave = true)
        {
            PlayerPrefs.DeleteAll();
            if (autoSave)
            {
                PlayerPrefs.Save();
            }
            
            RaiseChange(PlayerPrefsChangeType.DeleteAll, PlayerPrefsDataType.Unspecific);
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }

        private static void RaiseChange(PlayerPrefsChangeType changeType, PlayerPrefsDataType dataType, params string[] keys)
        {
            OnChanged?.Invoke(null, new PlayerPrefsChangeEventArgs(keys, changeType, dataType));
        }

        private static void RaiseArrayChange(PlayerPrefsChangeType changeType, PlayerPrefsDataType dataType, int arraySize, params string[] keys)
        {
            OnChanged?.Invoke(null, new PlayerPrefsArrayChangeEventArgs(keys, changeType, dataType, arraySize));
        }
    }

    public enum KeySearchType
    {
        All,
        Any,
        None
    }

    public class PlayerPrefsChangeEventArgs : EventArgs
    {
        public string[] Keys { get; }
        public PlayerPrefsChangeType ChangeType { get; }
        public PlayerPrefsDataType DataType { get; }

        public PlayerPrefsChangeEventArgs(string[] keys, PlayerPrefsChangeType changeType, PlayerPrefsDataType dataType)
        {
            Keys = keys;
            ChangeType = changeType;
            DataType = dataType;
        }
    }

    public class PlayerPrefsArrayChangeEventArgs : PlayerPrefsChangeEventArgs
    {
        public int ArraySize { get; }

        public PlayerPrefsArrayChangeEventArgs(string[] keys, PlayerPrefsChangeType changeType, PlayerPrefsDataType dataType, int arraySize) : base(keys, changeType, dataType)
        {
            ArraySize = arraySize;
        }
    }

    public enum PlayerPrefsChangeType
    {
        AddOrUpdate,
        Delete,
        DeleteAll
    }

    public enum PlayerPrefsDataType
    {
        Unspecific,
        String,
        Character,
        Int,
        Long,
        Float,
        Double,
        Boolean,
        Binary,
        DateTime,
        TimeSpan,
        Enum
    }
}