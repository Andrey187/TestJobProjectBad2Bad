using System;
using UnityEngine.UI;

public interface IInventoryItemView
{
    public CopyItem Item { get; set; }

    public Image Image { get; set; }

    public bool IsEquipped { get; set; }

    public bool CanChangedColor { get; set; }

    public event Action<InventoryItemView> OnEquipItem;
    public event Action<InventoryItemView> OnSelectItem;
    public event Action<InventoryItemView> OnRemoveItem;

    public void InitialiseItem(CopyItem newItem);

    public void RefreshCount();

    public void PopUpMenuActivate(bool isActive);
}
