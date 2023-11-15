using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;

    [Inject(Id = "ItemsData")]
    private ISaveLoad _itemsSaveLoadData;

    [Inject(Id = "SlotsData")]
    private ISaveLoad _slotsSaveLoadData;

    [Inject(Id = "AmmoData")]
    private ISaveLoad _ammoSaveLoadData;

    private void Start()
    {
        _saveButton.onClick.AddListener(() => SaveButton());
        _loadButton.onClick.AddListener(() => LoadButton());
    }

    private void SaveButton()
    {
        SaveData saveData = new SaveData();
        _itemsSaveLoadData.SaveData(saveData);
        _slotsSaveLoadData.SaveData(saveData);
        _ammoSaveLoadData.SaveData(saveData);
    }

    private void LoadButton()
    {
        _itemsSaveLoadData.LoadData();
        _slotsSaveLoadData.LoadData();
        _ammoSaveLoadData.LoadData();
    }
}
