using UnityEngine;

public interface IItemParams
{
    public int Id { get; set; }
    public string ItemName { get; set; }
    public int SlotIndex { get; set; }
    public int Count { get; set; }
    public ItemType Type { get; set; }

    public bool Stackable { get; set; }
    public bool IsEquipped { get; set; }
    public Sprite Icon { get; set; }

    
    public byte[] ImageData { get; set; }
}
