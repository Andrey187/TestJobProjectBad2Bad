using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SaveLoadAmmoData : SaveLoadSystem<AmmoData>
{
    private void Start()
    {
        fileName = "SaveFileAmmoData";
    }

    public override void SaveData(SaveData saveData)
    {
        saveData.AmmoData = new List<AmmoData>();

        AmmoData newCopySlot = new AmmoData();
        newCopySlot.CopyAmmo(_shootModel);
        saveData.AmmoData.Add(newCopySlot);

        SaveDataToJson(saveData.AmmoData, fileName);
    }

    public async override Task LoadData()
    {
        await LoadDataFromJson(LoadAmmoData, fileName);
    }

    private async Task LoadAmmoData()
    {
        List<AmmoData> loadedData = JsonConvert.DeserializeObject<List<AmmoData>>(jsonData);

        if (loadedData.Count > 0)
        {
            _shootModel.SetAmmo(loadedData[0].Ammo);
        }
        await Task.Yield();
    }
}
