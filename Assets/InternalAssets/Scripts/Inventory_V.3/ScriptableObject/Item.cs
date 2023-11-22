using System;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Item/Create New Item")]
[Serializable]
public class Item : ScriptableObject, IItemParams, IWeaponParams
{
    [Header("Only gameplay")]
    [SerializeField] private int _id;
    [SerializeField] private string _itemName;
    [SerializeField] private int _count;
    [SerializeField] private ItemType _type;
    [HideInInspector] [SerializeField] public int SlotIndex { get; set; }


    [Header("Only UI")]
    [SerializeField] private bool _stackable = true;

    [Header("Both")]
    [SerializeField] private Sprite _icon;

    [HideInInspector]
    [SerializeField] private int _magazineSize;

    public int Id { get => _id; set => _id = value; }
    public string ItemName { get => _itemName; set => _itemName = value; }
    public int Count { get => _count; set => _count = value; }
    public ItemType Type { get => _type; set => _type = value; }
    public bool Stackable { get => _stackable; set => _stackable = value; }
    public bool IsEquipped { get; set; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public byte[] ImageData { get; set; }
    public int MagazineSize { get => _magazineSize; set => _magazineSize = value; }
}

public enum ItemType
{
    Ammo,
    Weapon
}
