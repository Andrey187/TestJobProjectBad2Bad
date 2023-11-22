using UnityEngine;
using UnityEngine.UI;

public class BackPackButton : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.onClick.AddListener(ToggleInventory);
    }

    private void ToggleInventory()
    {
        _inventory.SetActive(!_inventory.activeSelf);
    }
}
