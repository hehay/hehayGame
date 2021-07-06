/* Brief: IDConfig.json reader, support Editor Mode and Game Mode
 * Author: Komal
 * Date: "2019-07-10"
 */

#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections.Generic;

namespace komal
{
    public partial class Config
    {
#if UNITY_ANDROID
        public static readonly string IDConfigFile = "IDConfig_Android.json";
#else
        public static readonly string IDConfigFile = "IDConfig_iOS.json";
#endif
        public static readonly IDConfig ID = IDConfig.Create(IDConfigFile);

        [Serializable]
        public class IDConfig
        {
            public List<Toggle> toggle;
            public List<KV> kv;
            public List<PurchaseItem> iap;
            public List<PushItem> push;

            public static IDConfig Create(string jsonFileName)
            {
                var ret = KomalUtil.Instance.GetConfig<IDConfig>(jsonFileName, "IDConfig");
#if UNITY_EDITOR
                UnityEngine.Debug.Log(UnityEngine.JsonUtility.ToJson(ret));
#endif
                return ret;
            }

            public string GetString(string key)
            {
                string value = null;
                this.kv.ForEach(it =>
                {
                    if (string.Equals(key, it.key))
                    {
                        value = it.value;
                    }
                });
                return value;
            }

            public bool GetToggle(string key)
            {
                bool value = false;
                this.toggle.ForEach(it =>
                {
                    if (string.Equals(key, it.name))
                    {
                        value = it.on;
                    }
                });
                return value;
            }

            public int GetInt(string key)
            {
                int value = 0;
                this.kv.ForEach(it =>
                {
                    if (string.Equals(key, it.key))
                    {
                        value = Convert.ToInt32(it.value);
                    }
                });
                return value;
            }

            public PurchaseItem GetPurchaseItem(string productKey)
            {
                PurchaseItem ret = null;
                this.iap.ForEach(it =>
                {
                    if (string.Equals(productKey, it.Key))
                    {
                        ret = it;
                    }
                });
                return ret;
            }
        }

        [Serializable]
        public class Toggle
        {
            public string name;
            public bool on;
        }

        [Serializable]
        public class KV
        {
            public string key;
            public string value;
        }

        [Serializable]
        public class PurchaseItem
        {
            public string Key;
            public string ID;
            public string Name;
            public string Type;
            public float Price;
            public string Currency;
        }

        [Serializable]
        public class PushItem
        {
            public string Name;
            public bool Toggle;
            public string Type;
            public string Date;
            public string Text;
        }


        public static class PurchaseType
        {
            public const string NonConsumable = "NonConsumable";
            public const string Consumable = "Consumable";
            public const string Subscription = "Subscription";
        }
    }
}