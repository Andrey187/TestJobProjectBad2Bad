using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySlot : MonoBehaviour, IInventorySlot, IDropHandler
{
    [SerializeField] private Image _image;
    private Color _imageColor;

    public Image Image { get => _image; set => _image = value; }
    public Color CachedImageColor { get => _imageColor; set => _imageColor = value; }

    private void Start()
    {
        _imageColor = _image.color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.ParentAfterDrag = transform;
        } 
    }
}
