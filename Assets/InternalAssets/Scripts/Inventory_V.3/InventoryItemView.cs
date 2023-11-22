using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryItemView : MonoBehaviour, IInventoryItemView, IBeginDragHandler, IEndDragHandler
{
    [Header("UI")]
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Button _removeItemButton;
    [SerializeField] private Button _selectItemButton;
    [SerializeField] private Button _equipItemButton;
    [SerializeField] private Vector3 _offsetPopUpMenu;

    public ItemData Item { get; set; }

    public Image Image { get => _image; set => _image = value; }

    public bool IsEquipped { get => Item.IsEquipped; set => Item.IsEquipped = value; }
    public bool CanChangedColor { get; set; } = false;

    public event Action<InventoryItemView> OnEquipItem;
    public event Action<InventoryItemView> OnSelectItem;
    public event Action<InventoryItemView> OnRemoveItem;

    private void Start()
    {
        _removeItemButton.onClick.AddListener(() => OnRemoveItem?.Invoke(this));
        _selectItemButton.onClick.AddListener(() => OnSelectItem?.Invoke(this));
        _equipItemButton.onClick.AddListener(() => OnEquipItem?.Invoke(this));
    }

    public void InitialiseItem(ItemData newItem)
    {
        Item = newItem;
        _image.sprite = Item.Icon;
        _name.text = Item.ItemName;

        RefreshCount();
    }

    public void RefreshCount()
    {
        _countText.text = Item.Count.ToString();
        bool textActive = Item.Count > 1;
        _countText.gameObject.SetActive(textActive);

        if (Item.Count <= 0)
        {
            OnRemoveItem?.Invoke(this);
        }
    }

    public void PopUpMenuActivate(bool isActive)
    {
        _selectItemButton.gameObject.SetActive(isActive);
        _removeItemButton.gameObject.SetActive(isActive);

        if (IsEquipped)
        {
            return;
        }

        if (Item.Type == ItemType.Weapon)
        {
            _equipItemButton.gameObject.SetActive(isActive);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsEquipped && CanChangedColor)
        {
            InventorySlot inventorySlot = gameObject.GetComponentInParent<InventorySlot>();
            inventorySlot.Image.color = inventorySlot.CachedImageColor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsEquipped && CanChangedColor && eventData.pointerDrag != null)
        {
            InventorySlot inventorySlot = eventData.pointerEnter.GetComponent<InventorySlot>();
            if (inventorySlot != null)
            {
                inventorySlot.Image.color = Color.green;
                CanChangedColor = false;
            }
        }
    }
}
