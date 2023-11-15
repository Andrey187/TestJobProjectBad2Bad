using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.ComponentModel;

public class ShootAndReloadView : MonoBehaviour
{
    [SerializeField] private Button _reloadButton;
    [SerializeField] private Button _shootButton;
    [SerializeField] private TextMeshProUGUI _countAmmo;

    [SerializeField] private ShootModel shootModel;
    private ShootingViewModel _shootingViewModel;

    private void Start()
    {
        _countAmmo.text = "0";

        _shootingViewModel = new ShootingViewModel(shootModel);

        _shootingViewModel.PropertyChanged += HandlePropertyChanged;

        _reloadButton.onClick.AddListener(() => _shootingViewModel.OnButtonReloadClick());

        _shootButton.onClick.AddListener(() => _shootingViewModel.OnButtonShootClick());

        if (_shootingViewModel.Item == null)
        {
            _reloadButton.interactable = false;
            _shootButton.interactable = false;
        }
    }

    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_shootingViewModel.Ammo) || e.PropertyName == nameof(_shootingViewModel.Item))
        {
            if (_shootingViewModel.Item != null)
            {
                _countAmmo.text = _shootingViewModel.Ammo.ToString();
                _reloadButton.interactable = _shootingViewModel.Ammo < _shootingViewModel.Item.MagazineSize;
                _shootButton.interactable = _shootingViewModel.Ammo > 0;
            }
            else
            {
                _countAmmo.text = _shootingViewModel.Ammo.ToString();
                _reloadButton.interactable = false;
                _shootButton.interactable = false;
            }
        }
    }
}
