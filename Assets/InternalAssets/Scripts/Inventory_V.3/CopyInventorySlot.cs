using System;
using UnityEngine;

[Serializable]
public class CopyInventorySlot
{
    public string ColorHex { get; set; } // Цвет в формате HEX

    public string CachedColorSlotHex { get; set; }

    public void CopySlot(InventorySlot slot)
    {
        ColorHex = ColorToHex(ColorSlot(slot.Image.color));
        CachedColorSlotHex = ColorToHex(ColorSlot(slot.CachedImageColor));
    }

    private Color32 ColorSlot(Color color)
    {
        Color newColor = color;
        Color32 color32 = new Color32(
            (byte)Mathf.Round(newColor.r * 255),
            (byte)Mathf.Round(newColor.g * 255),
            (byte)Mathf.Round(newColor.b * 255),
            (byte)Mathf.Round(newColor.a * 255));

        return color32;
    }

    public Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Failed to parse color from HEX string: " + hex);
            return Color.white; // Вернуть какой-то значения по умолчанию в случае ошибки
        }
    }

    private string ColorToHex(Color32 color32)
    {
        return "#" + color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2") + color32.a.ToString("X2");
    }
}
