using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        await LoadDataFromJson(ClearAndLoadInventorySlots, fileName);
    }

    private async Task ClearAndLoadInventorySlots()
    {
        List<AmmoData> loadedData = JsonConvert.DeserializeObject<List<AmmoData>>(jsonData);

        for (int i = 0; i < loadedData.Count; i++)
        {
            _shootModel.Ammo = loadedData[i].Ammo;
        }
        await Task.Yield();
    }
}
