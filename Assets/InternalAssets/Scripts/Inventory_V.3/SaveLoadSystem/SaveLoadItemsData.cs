using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SaveLoadItemsData : SaveLoadSystem<ItemData>
{
    private void Start()
    {
        fileName = "SaveFileItemsData";
    }

    public override void SaveData(SaveData saveData)
    {
        saveData.Items = new List<ItemData>();

        foreach (var slot in inventoryManager.InventorySlots)
        {
            InventoryItemView itemInSlot = slot.GetComponentInChildren<InventoryItemView>();
            if (itemInSlot != null)
            {
                ItemData copyItem = new ItemData();

                copyItem.CopyItemGeneric(itemInSlot.Item);

                copyItem.ImageData = itemInSlot.Item.Icon.texture.EncodeToPNG();

                saveData.Items.Add(copyItem);
            }
        }
        SaveDataToJson(saveData.Items, fileName);
    }

    public override async Task LoadData()
    {
        await LoadDataFromJson(ClearAndLoadInventory, fileName);
    }

    private async Task ClearAndLoadInventory()
    {
        // Пройдитесь по всем ячейкам инвентаря и удалите итемы
        foreach (var slot in inventoryManager.InventorySlots)
        {
            IInventoryItemView itemInSlot = slot.GetComponentInChildren<IInventoryItemView>();
            if (itemInSlot != null)
            {
                inventoryManager.RemoveItemFromInventory(itemInSlot);
                await Task.Yield(); // Дайте Unity время на уничтожение объекта
            }
        }

        List<ItemData> loadedItems = JsonConvert.DeserializeObject<List<ItemData>>(jsonData);

        foreach (var itemData in loadedItems)
        {
            ItemData item = new ItemData();
            item.CopyItemGeneric(itemData);

            Texture2D texture = new Texture2D(1, 1);
            if (texture.LoadImage(itemData.ImageData))
            {
                Sprite icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                item.Icon = icon;
            }
            else
            {
                Debug.LogError("Failed to load image for item.");
            }
            inventoryManager.AddItem(item);
            await Task.Yield();
        }
    }
}