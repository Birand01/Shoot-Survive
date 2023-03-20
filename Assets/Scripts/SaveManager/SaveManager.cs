using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }
    public int currentTurret;
    public int goldAmount;
    public bool[] turretsUnlocked = new bool[5] {true,false,false,false,false};
    private void OnEnable()
    {
        CharacterSelection.OnSelectedCarIndex += SelectedCarIndex;
        TurretBase.OnGoldTextCounter += GoldAmount;
       
    }
    private void OnDisable()
    {
        CharacterSelection.OnSelectedCarIndex -= SelectedCarIndex;
        TurretBase.OnGoldTextCounter -= GoldAmount;
      
    }
    private void Awake()
    {   
        if(instance!=null && instance!=this)
            Destroy(gameObject);
        else
        instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf=new BinaryFormatter();
            FileStream file=File.Open(Application.persistentDataPath +"/playerInfo.dat",FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);
            currentTurret = data.currentTurret;
            goldAmount = data.gold;
            turretsUnlocked=data.turretsUnlocked;
            if(data.turretsUnlocked==null)
            {
                turretsUnlocked = new bool[5] { true, false, false, false, false };
            }
            file.Close();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data=new PlayerData_Storage();
        data.currentTurret = currentTurret;
        data.gold = goldAmount;
        data.turretsUnlocked = turretsUnlocked;
        bf.Serialize(file, data);
        file.Close();
    }
    
    private void GoldAmount(int gold)
    {
        goldAmount = gold;
        Save();
    }
    private void SelectedCarIndex(int index)
    {
        currentTurret = index;
        Save();
    }
   

}

[Serializable]
public class PlayerData_Storage
{
    public int currentTurret;
    public int gold;
    public bool[] turretsUnlocked;
}