using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Android;

public class SaveManager : MonoBehaviour
{
    public static SD_User CurrentData { get; private set; }

    public void Init()
    {
        CurrentData = null;
        LoadData("1");
        if (CurrentData == null) CurrentData = new SD_User();
    }
    public void NewSave(string keyVal)
    {
        if (PlayerPrefs.HasKey(keyVal)) return;
        CurrentData = new SD_User();
        CurrentData.date = DateTime.Now.ToString();
        string jsonData = JsonConvert.SerializeObject(CurrentData);
        PlayerPrefs.SetString(keyVal, jsonData);
        PlayerPrefs.Save();
    }

    public void SaveData( string keyVal) 
    {
        CurrentData.date = DateTime.Now.ToString();
        if(PlayerPrefs.HasKey(keyVal) ) return;
        string jsonData = JsonConvert.SerializeObject(CurrentData);
        PlayerPrefs.SetString(keyVal, jsonData);
        PlayerPrefs.Save();
    }

    public void LoadData(string keyVal)
    {
        if (!PlayerPrefs.HasKey(keyVal)) return;
        SD_User data = new SD_User();
        string loadData = PlayerPrefs.GetString(keyVal);
        if (string.IsNullOrEmpty(loadData)) return;
        data = JsonConvert.DeserializeObject<SD_User>(loadData);
        CurrentData = data; 
    }

    public void DeleteData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
        }
    }

    public void DeleteData<T>() where T : ISaveData, new()
    {
        T data = new T();
        string key = data.GetSaveKey();

        Debug.Log($"삭제 시작 {key}");

        if (!PlayerPrefs.HasKey(key))
        {
            Debug.LogError($"저장 키{key} 없음");
            return;
        }

        DeleteData(key);

        Debug.Log($"삭제 완료 {key}");

    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();

        Debug.Log($"모든 데이터 삭제 완료");
    }


}
