using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityPrefsEx.Runtime.Projects.unity_prefs_ex.Scripts.Runtime.Utils;

namespace UnityPrefsEx.Test.prefs_ex.Scripts.Test
{
    public class PlayerPrefExSimpleTest
    {
        private const string Key = "test.key";
        
        [Test]
        public void TestBoolean()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.IsFalse(PlayerPrefsEx.GetBool(Key, false));
            
            PlayerPrefsEx.SetBool(Key, true);
            Assert.IsTrue(PlayerPrefsEx.GetBool(Key, false));
        }
        
        [Test]
        public void TestDouble()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(0d, PlayerPrefsEx.GetDouble(Key, 0d));
            
            PlayerPrefsEx.SetDouble(Key, 10d);
            Assert.AreEqual(10d, PlayerPrefsEx.GetDouble(Key, 0d));
        }
        
        [Test]
        public void TestFloat()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(0f, PlayerPrefsEx.GetFloat(Key, 0f));
            
            PlayerPrefsEx.SetFloat(Key, 10f);
            Assert.AreEqual(10f, PlayerPrefsEx.GetFloat(Key, 0f));
        }
        
        [Test]
        public void TestInt()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(0, PlayerPrefsEx.GetInt(Key, 0));
            
            PlayerPrefsEx.SetInt(Key, 10);
            Assert.AreEqual(10, PlayerPrefsEx.GetInt(Key, 0));
        }
        
        [Test]
        public void TestLong()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(0L, PlayerPrefsEx.GetLong(Key, 0L));
            
            PlayerPrefsEx.SetLong(Key, 10L);
            Assert.AreEqual(10L, PlayerPrefsEx.GetLong(Key, 0L));
        }
        
        [Test]
        public void TestString()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual("", PlayerPrefsEx.GetString(Key, ""));
            
            PlayerPrefsEx.SetString(Key, "abc");
            Assert.AreEqual("abc", PlayerPrefsEx.GetString(Key, ""));
        }
        
        [Test]
        public void TestChar()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(' ', PlayerPrefsEx.GetCharacter(Key, ' '));
            
            PlayerPrefsEx.SetCharacter(Key, '*');
            Assert.AreEqual('*', PlayerPrefsEx.GetCharacter(Key, ' '));
        }
        
        [Test]
        public void TestEnum()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(PlayMode.StopAll, PlayerPrefsEx.GetEnum(Key, PlayMode.StopAll));
            
            PlayerPrefsEx.SetEnum(Key, PlayMode.StopSameLayer);
            Assert.AreEqual(PlayMode.StopSameLayer, PlayerPrefsEx.GetEnum(Key, PlayMode.StopAll));
        }
        
        [Test]
        public void TestBinary()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(Array.Empty<byte>(), PlayerPrefsEx.GetBytes(Key, Array.Empty<byte>()));
            
            PlayerPrefsEx.SetBytes(Key, new byte[] {0x00, 0x80, 0xFF});
            Assert.AreEqual(new byte[] {0x00, 0x80, 0xFF}, PlayerPrefsEx.GetBytes(Key, Array.Empty<byte>()));
        }
        
        [Test]
        public void TestDateTime()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(DateTime.MinValue, PlayerPrefsEx.GetDateTime(Key, DateTime.MinValue));

            var now = DateTime.Now;
            PlayerPrefsEx.SetDateTime(Key, now);
            Assert.IsTrue((now - PlayerPrefsEx.GetDateTime(Key, DateTime.MinValue)).TotalMilliseconds < 1000);
        }
        
        [Test]
        public void TestTimeSpan()
        {
            PlayerPrefsEx.DeleteAll();
            Assert.AreEqual(TimeSpan.Zero, PlayerPrefsEx.GetTimeSpan(Key, TimeSpan.Zero));
            
            PlayerPrefsEx.SetTimeSpan(Key, TimeSpan.FromMilliseconds(100));
            Assert.AreEqual(TimeSpan.FromMilliseconds(100), PlayerPrefsEx.GetTimeSpan(Key, TimeSpan.Zero));
        }
    }
}
