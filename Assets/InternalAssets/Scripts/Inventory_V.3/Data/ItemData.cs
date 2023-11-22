using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class ItemData : IItemParams, IWeaponParams
{
    public int Id { get; set; }
    public string ItemName { get; set; }
    public int SlotIndex { get; set; }
    public int Count { get; set; }
    public ItemType Type { get; set; }
    public bool Stackable { get; set; }

    public bool IsEquipped { get; set; }

    [JsonIgnore] public Sprite Icon { get; set; }
    public byte[] ImageData { get; set; }
    public int MagazineSize { get; set; }

    public void CopyItemGeneric<T>(T item) where T : IItemParams, IWeaponParams
    {
        Id = item.Id;
        ItemName = item.ItemName;
        SlotIndex = item.SlotIndex;
        Count = item.Count;
        Type = item.Type;
        Stackable = item.Stackable;
        IsEquipped = item.IsEquipped;
        Icon = item.Icon;
        MagazineSize = item.MagazineSize;
    }
}
