using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IInventorySlot, IDropHandler
{
    [SerializeField] private Image _image;

    public Image Image { get => _image; set => _image = value; }
    public Color CachedImageColor { get; set; }

    private void Start()
    {
        if (CachedImageColor == Color.clear)
        {
            CachedImageColor = _image.color;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(CachedImageColor);
            Debug.Log(Image.color);
        }
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
