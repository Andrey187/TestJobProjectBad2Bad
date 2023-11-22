public interface IShootModel
{
    public int Ammo { get; set; }
    public void SetAmmo(int ammo);
    public void ChangeWeapon(IInventoryItemView inventoryItem);
}
