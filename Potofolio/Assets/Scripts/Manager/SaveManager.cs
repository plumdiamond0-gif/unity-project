using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Android;

public class SaveManager : MonoBehaviour
{
    public static SD_User CurrentData { get; private set; }

    public void Init()
    {
        //TODO : 세이브매니저 나중에 수정 
        //CurrentData = LoadData<SD_User>();
        if(CurrentData == null )
        {
            CurrentData = new SD_User();
            SaveCurrentData();
        }
    }


    public void SaveCurrentData()
    {
        SaveData( CurrentData );
    }
   public void SaveData<T>(T data) where T : ISaveData
    {
        string key = data.GetSaveKey();
        Debug.Log($"저장 시작 {key}");
        if(PlayerPrefs.HasKey(key) )
        {
            Debug.LogWarning($"저장 데이어{key} 있음");
        }
        string jsonData = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(key, jsonData);

        PlayerPrefs.Save();

        Debug.Log($"저장 완료 {key}, jsonData: {jsonData} ");
    }

    public T LoadData<T>() where T : ISaveData, new()
    {
        T data = new T();
        string key = data.GetSaveKey();

        Debug.Log($"불러오기 시작 {key}");

        if (!PlayerPrefs.HasKey(key))
        {
            Debug.Log($"저장 데이터 없음: {key} (처음 실행)");
            return default;
        }

        string loadData = PlayerPrefs.GetString(key);

        if (string.IsNullOrEmpty(loadData))
        {
            Debug.LogWarning($"저장 데이터 비어있음: {key}");
            return default;
        }

        data = JsonConvert.DeserializeObject<T>(loadData);

        Debug.Log($"불러오기 완료 {key}");

        return data;
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
