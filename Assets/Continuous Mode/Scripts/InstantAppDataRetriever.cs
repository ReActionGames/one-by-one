using System;
using UnityEngine;

namespace Continuous
{
    public class InstantAppDataRetriever : MonoBehaviour
    {
        public static event Action<InstantToInstalledData> OnInstantDataRevtrieved;

        private void Start()
        {
            if (Application.isEditor) return;

#if UNITY_ANDROID
            byte[] cookieBytes = GooglePlayInstant.CookieApi.GetInstantAppCookieBytes();
            string dataJson = System.Text.Encoding.UTF8.GetString(cookieBytes);
            if (!string.IsNullOrEmpty(dataJson))
            {
                InstantToInstalledData data = JsonUtility.FromJson<InstantToInstalledData>(dataJson);
                OnInstantDataRevtrieved?.Invoke(data);
            }
#endif
        }
    }
}