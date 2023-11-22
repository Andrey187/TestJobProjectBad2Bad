using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public interface IInventoryManager : INotifyPropertyChanged
{
    public InventorySlot[] InventorySlots { get; }

    public bool AddItemInSlot(ItemData item);
    public bool LoadItemToSlot(ItemData item, int slotIndex);
    public List<IInventoryItemView> AmmoSlots { get; set; }

    public List<IInventoryItemView> WeaponSlots { get; set; }

    public void RemoveItemFromInventory(IInventoryItemView item);
}
