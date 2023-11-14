using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SaveLoadSlotsData : SaveLoadSystem<CopyInventorySlot>
{
    private void Start()
    {
        fileName = "SaveFileSlotsData";
    }


    public override void SaveData(SaveData saveData)
    {
        saveData.InventorySlots = new List<CopyInventorySlot>();

        foreach (var slot in inventoryManager.InventorySlots)
        {
            CopyInventorySlot newCopySlot = new CopyInventorySlot();
            newCopySlot.CopySlot(slot);
            saveData.InventorySlots.Add(newCopySlot);
            
        }
        SaveDataToJson(saveData.InventorySlots, fileName);
    }

    public async override Task LoadData()
    {
        await LoadDataFromJson(ClearAndLoadInventorySlots, fileName);
    }

    private async Task ClearAndLoadInventorySlots()
    {
        List<CopyInventorySlot> loadedItems = JsonConvert.DeserializeObject<List<CopyInventorySlot>>(jsonData);

        for (int i = 0; i < loadedItems.Count; i++)
        {
            if (i < inventoryManager.InventorySlots.Length)
            {
                Color color = loadedItems[i].HexToColor(loadedItems[i].ColorHex);
                Color cachedColor = loadedItems[i].HexToColor(loadedItems[i].CachedColorSlotHex);
                inventoryManager.InventorySlots[i].Image.color = color;
                inventoryManager.InventorySlots[i].CachedImageColor = cachedColor;
            }
            await Task.Yield();
        }
    }
}

