using UnityEngine;
using Zenject;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private Item Item;

    [Inject] private IInventoryManager _inventoryManager;

    private void PickUp()
    {
        ItemData copyItem = new ItemData();

        copyItem.CopyItemGeneric(Item);

        bool result = _inventoryManager.AddItem(copyItem);
    }


    private void OnMouseDown()
    {
        PickUp();
    }
}
