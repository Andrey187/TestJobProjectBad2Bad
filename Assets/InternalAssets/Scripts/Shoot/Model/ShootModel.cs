using System.ComponentModel;
using UnityEngine;
using Zenject;

public class ShootModel : MonoBehaviour, IShootModel
{
    [Inject] private IInventoryManager _inventoryManager;
    
    private ItemData _item;
    private int _ammo;
    public ItemData Item
    {
        get { return _item; }
        set { _item = value; }
    }

    public int Ammo 
    {
        get { return _ammo; }
        set { _ammo = value; }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(Ammo);
        }
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

    public void ChangeWeapon(IInventoryItemView inventoryItemView)
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
            
            _item = inventoryItemView.Item;
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
