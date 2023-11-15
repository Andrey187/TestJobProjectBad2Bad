using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InventoryManager_V3 : MonoBehaviour, IInventoryManager, INotifyPropertyChanged
{
    [SerializeField] private int MaxStackedItems = 128;
    [SerializeField] private InventorySlot[] _inventorySlots;
    [SerializeField] private GameObject InventoryItemPrefab;
    [SerializeField] private List<IInventoryItemView> _ammoSlots = new List<IInventoryItemView>();

    private List<IInventoryItemView> _weaponSlots = new List<IInventoryItemView>();

    public event PropertyChangedEventHandler PropertyChanged;

    public InventorySlot[] InventorySlots => _inventorySlots;
    public List<IInventoryItemView> AmmoSlots { get => _ammoSlots; set => _ammoSlots = value; }

    public List<IInventoryItemView> WeaponSlots { get => _weaponSlots; set => _weaponSlots = value; }

    public bool AddItem(ItemData item)
    {
        // ѕытаемс€ стакать существующие предметы
        MergeExistingItems(item);

        return AddRemainingItems(item, item.Count);
    }

    private void MergeExistingItems(ItemData item)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            IInventoryItemView itemInSlot = slot.GetComponentInChildren<IInventoryItemView>();

            if (itemInSlot != null &&
                itemInSlot.Item.Id == item.Id &&
                itemInSlot.Item.Count < MaxStackedItems &&
                itemInSlot.Item.Stackable == true)
            {
                int spaceLeft = MaxStackedItems - itemInSlot.Item.Count;

                if (item.Count <= spaceLeft)
                {
                    itemInSlot.Item.Count += item.Count;
                    item.Count = 0;
                    itemInSlot.RefreshCount();
                    break;
                }
                else
                {
                    itemInSlot.Item.Count = MaxStackedItems;
                    item.Count -= spaceLeft;
                    itemInSlot.RefreshCount();
                }
            }
        }
    }

    private bool AddRemainingItems(ItemData item, int itemCount)
    {
        // Add any remaining items using the new implementation

        int numItems = (int)Math.Ceiling((double)itemCount / MaxStackedItems);

        for (int i = 0; i < numItems; i++)
        {
            int stackSize = Math.Min(itemCount, MaxStackedItems);

            // —оздайте новый экземпл€р CopyItem дл€ каждого нового итема
            ItemData newItem = new ItemData();
            newItem.CopyItemGeneric(item);
            newItem.Count = stackSize;

            if (!AddNewItem(newItem))
            {
                return false; // The inventory is full
            }

            itemCount -= stackSize;
        }
        return true;
    }

    private bool AddNewItem(ItemData item)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            IInventoryItemView itemInSlot = slot.GetComponentInChildren<IInventoryItemView>();
            
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false; // The inventory is full
    }

    private void SpawnNewItem(ItemData item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(InventoryItemPrefab, slot.transform);
        newItemGo.TryGetComponent(out IInventoryItemView inventoryItem);
        inventoryItem.InitialiseItem(item);

        if (inventoryItem.Item.Type == ItemType.Ammo)
        {
            _ammoSlots.Add(inventoryItem);
        }
    }

    public void RemoveItemFromInventory(IInventoryItemView item)
    {
        GameObject itemGameObject = (item as MonoBehaviour)?.gameObject;

        if (itemGameObject != null)
        {
            Destroy(itemGameObject);
        }

        if (item.Item.Type == ItemType.Ammo)
        {
            _ammoSlots.Remove(item);
        }

        if (item.Item.Type == ItemType.Weapon)
        {
            _weaponSlots.Remove(item);
            OnPropertyChanged(nameof(WeaponSlots));
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}