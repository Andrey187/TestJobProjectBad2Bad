public class AmmoData
{
    public int Ammo { get; set; }

    public void CopyAmmo(IShootModel shootModel)
    {
        Ammo = shootModel.Ammo;
    }
}
