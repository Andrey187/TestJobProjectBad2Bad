using System.Collections.Generic;
using System.ComponentModel;

public class ShootingViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public int Ammo
    {
        get { return _shootModel.Ammo; }
        set { _shootModel.Ammo = value; }
    }

    public ItemData Item
    {
        get { return _shootModel.Item; }
        set { _shootModel.Item = value; }
    }

    private ShootModel _shootModel;
    private int _returnAmmo;

    public ShootingViewModel(ShootModel shootModel)
    {
        _shootModel = shootModel;
        _shootModel.PropertyChanged += HandlePropertyChanged;
    }

    public void OnButtonReloadClick()
    {
        Reload();
    }

    public void OnButtonShootClick()
    {
        Shoot();
    }

    private void Shoot()
    {
        if(Ammo != 0)
        {
            Ammo -= 1;
            OnPropertyChanged(nameof(Ammo));
        }
    }

    private void Reload()
    {
        if(Item != null)
        {
            // Check if Ammo is greater than or equal to 16, then exit
            if (Ammo >= _shootModel.Item.MagazineSize)
            {
                return;
            }

            int neededAmmo = _shootModel.Item.MagazineSize - Ammo;

            // Create a copy of the list for iteration
            List<IInventoryItemView> ammoSlotsCopy = new List<IInventoryItemView>(_shootModel.InventoryManager.AmmoSlots);

            foreach (var itemInSlot in ammoSlotsCopy)
            {
                if (itemInSlot.Item.Count >= neededAmmo)
                {
                    itemInSlot.Item.Count -= neededAmmo;
                    AddAmmo(neededAmmo);
                    itemInSlot.RefreshCount();
                    break;
                }
                else
                {
                    int takenAmmo = itemInSlot.Item.Count;
                    itemInSlot.Item.Count = 0;
                    AddAmmo(takenAmmo);
                    neededAmmo -= takenAmmo;

                    itemInSlot.RefreshCount();
                }
            }
        }
    }

    private void AddAmmo(int amount)
    {
        Ammo += amount;
        OnPropertyChanged(nameof(Ammo));
    }

    private void RemoveAmmo(int amount)
    {
        Ammo -= amount;
        OnPropertyChanged(nameof(Ammo));
    }

   
    private void ChangeWeapon()
    {
        List<IInventoryItemView> ammoSlotsCopy = new List<IInventoryItemView>(_shootModel.InventoryManager.AmmoSlots);
        if (Ammo >= _shootModel.Item.MagazineSize)
        {
            _returnAmmo = Ammo - _shootModel.Item.MagazineSize;
            RemoveAmmo(_returnAmmo);

            foreach (var itemInSlot in ammoSlotsCopy)
            {
                itemInSlot.Item.Count += _returnAmmo;
                itemInSlot.RefreshCount();
                break;
            }
        }
    }

    private void ReturnAmmoToInventory()
    {
        List<IInventoryItemView> ammoSlotsCopy = new List<IInventoryItemView>(_shootModel.InventoryManager.AmmoSlots);

        foreach (var itemInSlot in ammoSlotsCopy)
        {
            itemInSlot.Item.Count += Ammo;
            itemInSlot.RefreshCount();
            RemoveAmmo(Ammo);
            break; // To add ammo to only the first matching slot
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Item):
                if(Item != null)
                {
                    OnPropertyChanged(nameof(Item));
                    ChangeWeapon();
                }
                else
                {
                    OnPropertyChanged(nameof(Item));
                    ReturnAmmoToInventory();
                }
                break;
            case nameof(Ammo):
                OnPropertyChanged(nameof(Ammo));
                break;
        }
    }
}
