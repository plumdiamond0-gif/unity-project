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
        if (CurrentData == null)
        {
            Debug.Log("커렌트데이터 널");
            CurrentData = new SD_User();
        }
    }
    public void NewSave(string keyVal)
    {
        Debug.Log($"저장 시작 {keyVal}");
        if (PlayerPrefs.HasKey(keyVal))
        {
            Debug.LogWarning($"저장 데이어{keyVal} 있음");
            return;
        }
        CurrentData = new SD_User();
        CurrentData.date = DateTime.Now.ToString();
        string jsonData = JsonConvert.SerializeObject(CurrentData);
        PlayerPrefs.SetString(keyVal, jsonData);

        PlayerPrefs.Save();

        Debug.Log($"저장 완료 {keyVal}, jsonData: {jsonData} ");
    }

    public void SaveData( string keyVal) 
    {
        CurrentData.date = DateTime.Now.ToString();

        Debug.Log($"저장 시작 {keyVal}");
        if(PlayerPrefs.HasKey(keyVal) )
        {
            Debug.LogWarning($"저장 데이어{keyVal} 있음");
            return;
        }
        string jsonData = JsonConvert.SerializeObject(CurrentData);
        PlayerPrefs.SetString(keyVal, jsonData);

        PlayerPrefs.Save();

        Debug.Log($"저장 완료 {keyVal}, jsonData: {jsonData} ");
    }

    public void LoadData(string keyVal)
    {
   
        SD_User data = new SD_User();
        //string key = data.GetSaveKey();

        Debug.Log($"불러오기 시작 {keyVal}");

        if (!PlayerPrefs.HasKey(keyVal))
        {
            Debug.Log($"저장 데이터 없음: {keyVal} (처음 실행)");
            return;
        }

        string loadData = PlayerPrefs.GetString(keyVal);

        if (string.IsNullOrEmpty(loadData))
        {
            Debug.LogWarning($"저장 데이터 비어있음: {keyVal}");
            return;
        }

        data = JsonConvert.DeserializeObject<SD_User>(loadData);

        Debug.Log($"불러오기 완료 {keyVal}");
        CurrentData = data; 

    }

    public void DeleteData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
            Debug.Log($"삭제 완료 key: {key}");
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
