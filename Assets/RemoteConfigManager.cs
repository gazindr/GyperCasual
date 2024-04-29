using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
#if UNITY_WEBGL
using InstantGamesBridge.Modules.RemoteConfig;
using InstantGamesBridge;
#endif

public class RemoteConfigManager : MonoBehaviour
{
    public static RemoteConfigManager Instance;

    

    IEnumerator Start()
    {
        yield return null;
        yield return null;
        LoadRemoteData();
    }
    void LoadRemoteData()
    {
#if UNITY_WEBGL
        if (Bridge.remoteConfig.isSupported)
        {
            Bridge.remoteConfig.Get(OnRemoteConfigGetCompleted);
        }
#endif
    }
#if UNITY_WEBGL
    void OnRemoteConfigGetCompleted(bool success, List<RemoteConfigValue> data)
    {
        if (success)
        {
            foreach (var remoteConfigItem in data)
            {
                if(remoteConfigItem.name == "InterAdDelay")
                {
                    if(Int32.TryParse(remoteConfigItem.value,out int newValue))
                    {
                        if(newValue>0 && newValue<10000)
                        {
                            AdsManager.Instance?.UpdateInterValue(newValue);
                        }
                    }
                }
                if (remoteConfigItem.name == "AvailableMoves")
                {
                    if (Int32.TryParse(remoteConfigItem.value, out int newValue))
                    {
                        if (newValue > 0 && newValue < 10)
                        {
                            UpdateMoves(newValue);
                        }
                    }
                }
                Debug.Log($"name: {remoteConfigItem.name}, value: {remoteConfigItem.value}");
            }
        }
    }


    public int moves_value;
    void UpdateMoves(int i)
    {
        moves_value = i;
        PlayerPrefs.SetInt("Moves", i);
    }
#endif
}
