using UnityEngine.EventSystems;
using UnityEngine;
using Zenject;

public class InventoryItem : MonoBehaviour, IPointerExitHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Vector3 _offsetPopUpMenu;

    [HideInInspector] public Transform ParentAfterDrag;

    [Inject] private IShootModel _shootModel;
    [Inject] private IInventoryManager _inventoryManager;
    private bool isDragAllowed = false; // Flag allowing or disabling dragging
    private bool isDragging = false; // Flag indicating that the object is currently being dragged
    private bool isSelected  = false;
    private IInventoryItemView _inventoryItemView;

    private void Start()
    {
        _inventoryItemView = gameObject.GetComponent<IInventoryItemView>();

        _inventoryItemView.OnEquipItem += EquipItem;
        _inventoryItemView.OnSelectItem += SelectItem;
        _inventoryItemView.OnRemoveItem += RemoveItem;
    }

    private void SelectItem(IInventoryItemView inventoryItemView)
    {
        isDragAllowed = true;
        isSelected = true;
        inventoryItemView.CanChangedColor = true;
        inventoryItemView.PopUpMenuActivate(false);
    }

    private void EquipItem(IInventoryItemView inventoryItemView)
    {
        inventoryItemView.PopUpMenuActivate(false);
        inventoryItemView.IsEquipped = true;
        InventorySlot inventorySlot = gameObject.GetComponentInParent<InventorySlot>();
        inventorySlot.Image.color = Color.green;
        
        _shootModel.ChangeWeapon(inventoryItemView);
    }

    private void RemoveItem(IInventoryItemView inventoryItemView)
    {
        InventorySlot inventorySlot = gameObject.GetComponentInParent<InventorySlot>();
        inventorySlot.Image.color = inventorySlot.CachedImageColor;
        _inventoryManager.RemoveItemFromInventory(inventoryItemView);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDragging && !isSelected)
        {
            _inventoryItemView.PopUpMenuActivate(true);
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        _inventoryItemView.PopUpMenuActivate(false);
        isSelected = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDragAllowed)
        {
            isDragging = true;
            _inventoryItemView.Image.raycastTarget = false;
            ParentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            _inventoryItemView.PopUpMenuActivate(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = transform.GetComponent<RectTransform>().position.z;
            transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            _inventoryItemView.Image.raycastTarget = true;
            transform.SetParent(ParentAfterDrag);
            isDragAllowed = false;
            isDragging = false;
            isSelected = false;
        }
    }
}
