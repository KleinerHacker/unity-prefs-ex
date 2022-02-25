using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityPrefsEx.Runtime.prefs_ex.Scripts.Runtime.Utils;

namespace UnityPrefsEx.Test.prefs_ex.Scripts.Test
{
    public class PlayerPrefExArrayTest
    {
        private const string Key = "test.key";

        [Test]
        public void TestBoolean()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<bool>(), PlayerPrefsEx.GetBool(Key, Array.Empty<bool>()));

            PlayerPrefsEx.SetBool(Key, new[] { true, false });
            Assert.AreEqual(new[] { true, false }, PlayerPrefsEx.GetBool(Key, Array.Empty<bool>()));
        }

        [Test]
        public void TestDouble()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<double>(), PlayerPrefsEx.GetDouble(Key, Array.Empty<double>()));

            PlayerPrefsEx.SetDouble(Key, new[] { 10d, 100d });
            Assert.AreEqual(new[] { 10d, 100d }, PlayerPrefsEx.GetDouble(Key, Array.Empty<double>()));
        }

        [Test]
        public void TestFloat()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<float>(), PlayerPrefsEx.GetFloat(Key, Array.Empty<float>()));

            PlayerPrefsEx.SetFloat(Key, new[] { 10f, 100f });
            Assert.AreEqual(new[] { 10f, 100f }, PlayerPrefsEx.GetFloat(Key, Array.Empty<float>()));
        }

        [Test]
        public void TestInt()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<int>(), PlayerPrefsEx.GetInt(Key, Array.Empty<int>()));

            PlayerPrefsEx.SetInt(Key, new[] { 10, 100 });
            Assert.AreEqual(new[] { 10, 100 }, PlayerPrefsEx.GetInt(Key, Array.Empty<int>()));
        }

        [Test]
        public void TestLong()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<long>(), PlayerPrefsEx.GetLong(Key, Array.Empty<long>()));

            PlayerPrefsEx.SetLong(Key, new[] { 10L, 100L });
            Assert.AreEqual(new[] { 10L, 100L }, PlayerPrefsEx.GetLong(Key, Array.Empty<long>()));
        }

        [Test]
        public void TestString()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<string>(), PlayerPrefsEx.GetString(Key, Array.Empty<string>()));

            PlayerPrefsEx.SetString(Key, new[] { "abc", "123" });
            Assert.AreEqual(new[] { "abc", "123" }, PlayerPrefsEx.GetString(Key, Array.Empty<string>()));
        }

        [Test]
        public void TestChar()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<char>(), PlayerPrefsEx.GetCharacter(Key, Array.Empty<char>()));

            PlayerPrefsEx.SetCharacter(Key, new[] { '*', '+', '-', '/' });
            Assert.AreEqual(new[] { '*', '+', '-', '/' }, PlayerPrefsEx.GetCharacter(Key, Array.Empty<char>()));
        }

        [Test]
        public void TestEnum()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<PlayMode>(), PlayerPrefsEx.GetEnum(Key, Array.Empty<PlayMode>()));

            PlayerPrefsEx.SetEnum(Key, new[] { PlayMode.StopSameLayer, PlayMode.StopAll });
            Assert.AreEqual(new[] { PlayMode.StopSameLayer, PlayMode.StopAll }, PlayerPrefsEx.GetEnum(Key, Array.Empty<PlayMode>()));
        }

        [Test]
        public void TestBinary()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<byte[]>(), PlayerPrefsEx.GetBytes(Key, Array.Empty<byte[]>()));

            PlayerPrefsEx.SetBytes(Key, new[] { new byte[] { 0x00, 0x80, 0xFF }, new byte[] { 0xFF, 0x80, 0x00 } });
            Assert.AreEqual(new[] { new byte[] { 0x00, 0x80, 0xFF }, new byte[] { 0xFF, 0x80, 0x00 } }, PlayerPrefsEx.GetBytes(Key, Array.Empty<byte[]>()));
        }

        [Test]
        public void TestDateTime()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<DateTime>(), PlayerPrefsEx.GetDateTime(Key, Array.Empty<DateTime>()));

            PlayerPrefsEx.SetDateTime(Key, new[] { new DateTime(2001, 01, 01, 0, 1, 0), new DateTime(2020, 01, 01, 0, 2, 0) });
            Assert.AreEqual(new[] { new DateTime(2001, 01, 01, 0, 1, 0), new DateTime(2020, 01, 01, 0, 2, 0) }, PlayerPrefsEx.GetDateTime(Key, Array.Empty<DateTime>()));
        }

        [Test]
        public void TestTimeSpan()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<TimeSpan>(), PlayerPrefsEx.GetTimeSpan(Key, Array.Empty<TimeSpan>()));

            PlayerPrefsEx.SetTimeSpan(Key, new[] { TimeSpan.FromMilliseconds(100), TimeSpan.FromDays(10) });
            Assert.AreEqual(new[] { TimeSpan.FromMilliseconds(100), TimeSpan.FromDays(10) }, PlayerPrefsEx.GetTimeSpan(Key, Array.Empty<TimeSpan>()));
        }
    }
}