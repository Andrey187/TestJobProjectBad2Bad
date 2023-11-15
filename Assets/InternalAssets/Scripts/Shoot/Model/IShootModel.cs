public interface IShootModel
{
    public int Ammo { get; set; }
    public void ChangeWeapon(IInventoryItemView inventoryItem);
}
