using System;
using UnityEngine;
using UnityEngine.UI;

public interface IInventorySlot
{
    public Image Image { get; set; }
    public Color CachedImageColor { get; set; }
}
