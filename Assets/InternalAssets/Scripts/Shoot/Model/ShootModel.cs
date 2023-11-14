using System.ComponentModel;
using UnityEngine;
using Zenject;

public class ShootModel : MonoBehaviour, IShootModel
{
    [Inject] private IInventoryManager _inventoryManager;
    
    private CopyItem _item;

    public CopyItem Item
    {
        get { return _item; }
        set { _item = value; }
    }

    public IInventoryManager InventoryManager
    {
        get { return _inventoryManager; }
        set { _inventoryManager = value; }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void Start()
    {
        _inventoryManager.PropertyChanged += HandlePropertyChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {

            foreach (var slot in _inventoryManager.InventorySlots)
            {
                Debug.Log(slot.CachedImageColor);
            }
        }
    }

    public void ChangeWeapon(CopyItem item, IInventoryItemView inventoryItemView)
    {
        if (_inventoryManager.WeaponSlots != null && !_inventoryManager.WeaponSlots.Contains(inventoryItemView) )
        {
            foreach(var cacheInventory in _inventoryManager.WeaponSlots)
            {
                if(cacheInventory != null)
                {
                    cacheInventory.IsEquipped = false;
                    InventorySlot inventorySlot = (cacheInventory as MonoBehaviour)?.gameObject.GetComponentInParent<InventorySlot>();

                    if (inventorySlot != null)
                    {
                        inventorySlot.Image.color = inventorySlot.CachedImageColor;
                    }
                }
            }
            _inventoryManager.WeaponSlots.Clear();
            
            _item = item;
            _inventoryManager.WeaponSlots.Add(inventoryItemView);
            OnPropertyChanged(nameof(Item));
        }
    }

    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_inventoryManager.WeaponSlots):
                Item = null;
                OnPropertyChanged(nameof(Item));
                break;
        }
    }
}
