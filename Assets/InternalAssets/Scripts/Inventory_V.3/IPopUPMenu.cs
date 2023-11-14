using System;

public interface IPopUPMenu
{
    public event Action<int> RemoveItem;
    public event Action<int> SelectItem;
    public void SetUniqueId(int uniqueId);
}
