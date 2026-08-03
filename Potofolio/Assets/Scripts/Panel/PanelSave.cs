using Newtonsoft.Json;
using System;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

[System.Serializable]
class LoadBtn
{
    public Button LoadButton;
    public TMP_Text LoadText;
    public string key;
    public Button Delete;
}
public class PanelSave : PanelBase
{
    [SerializeField] Button SaveButton;
    [SerializeField] Button NewGameButton;
    [SerializeField] Button DeleteAllButton;

    [SerializeField] LoadBtn[] LoadBtns;
    [SerializeField] private Button Exit;
    PlayerMovement Player;


    public override void Init()
    {
        SaveButton.onClick.AddListener(() => { SaveData(); GM.GetSoundManager().PlaySFX(AudioType.Button); });
        NewGameButton.onClick.AddListener(() => { NewGame(); GM.GetSoundManager().PlaySFX(AudioType.Button); });
        DeleteAllButton.onClick.AddListener(() => { DeleteAll(); GM.GetSoundManager().PlaySFX(AudioType.Button); });
        Exit.onClick.AddListener(() => { GM.GetAssetManager().Release("Save_Panel"); MoveAgain(); GM.GetSoundManager().PlaySFX(AudioType.Button); });

        foreach (var btn in LoadBtns)
        {
            if (PlayerPrefs.HasKey(btn.key))
            {
                string json = PlayerPrefs.GetString(btn.key);
                SD_User data = JsonConvert.DeserializeObject<SD_User>(json);
                btn.LoadText.text = data.date;
            }
            btn.LoadButton.onClick.AddListener(()=>
            {
                GM.GetSaveManager().LoadData(btn.key);
                GM.GetSoundManager().PlaySFX(AudioType.Unlock);
            });

            btn.Delete.onClick.AddListener(() =>
            {
                GM.GetSaveManager().DeleteData(btn.key);
                GM.GetSoundManager().PlaySFX(AudioType.SpecialBtn);
                btn.LoadText.text = $"Load{btn.key}";
            });
        }
    }

    void SaveData()
    {
        Debug.Log("SaveData");
        foreach (var btn in LoadBtns)
        {
            if (!PlayerPrefs.HasKey(btn.key))
            {
                GM.GetSaveManager().SaveData(btn.key);
                string json = PlayerPrefs.GetString(btn.key);
                SD_User data = JsonConvert.DeserializeObject<SD_User>(json);
                btn.LoadText.text = data.date;
                return;
            }
        }
    }

    void NewGame()
    {
        foreach (var btn in LoadBtns)
        {
            if (!PlayerPrefs.HasKey(btn.key))
            {
                GM.GetSaveManager().NewSave(btn.key);
                SaveManager.CurrentData.date = DateTime.Now.ToString();
                btn.LoadText.text = SaveManager.CurrentData.date;
                return;
            }
        }
    }

    void DeleteAll()
    {
        GM.GetSaveManager().DeleteAll();
        foreach (var btn in LoadBtns)
        {
            btn.LoadText.text = $"Load {btn.key}";
        }
    }

    public void GetPlayer(PlayerMovement player)
    {
        this.Player = player;
    }
    public void MoveAgain()
    {
        Player.canMove = true;
    }



}
