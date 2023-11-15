using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Zenject;
using System;
using System.Threading.Tasks;

[Serializable]
public class SaveData
{
    public List<ItemData> Items;
    public List<InventorySlotData> InventorySlots;
    public List<AmmoData> AmmoData;
}

public abstract class SaveLoadSystem<T> : MonoBehaviour, ISaveLoad
{
    [Inject] protected IInventoryManager inventoryManager;
    [Inject] protected IShootModel _shootModel;
    protected string fileName;
    protected string jsonData;

    public abstract void SaveData(SaveData saveData);

    public abstract Task LoadData();

    protected void SaveDataToJson(List<T> items, string fileName)
    {
        string json = JsonConvert.SerializeObject(items, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", json);
    }

    protected async Task LoadDataFromJson(Func<Task> method, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        if (File.Exists(path))
        {
            jsonData = File.ReadAllText(path);

            if (!string.IsNullOrEmpty(jsonData))
            {
                await method();
            }
            else
            {
                Debug.Log("JSON data is empty.");
            }
        }
        else
        {
            Debug.Log("No save file found.");
        }
    }
}