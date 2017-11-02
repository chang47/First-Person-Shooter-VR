using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public Text AmmoText;

    void Start()
    {
        {
            PlayerShootingController shootingController = Camera.main.GetComponentInChildren<PlayerShootingController>();
            UpdateAmmoText(shootingController.MaxAmmo, shootingController.MaxAmmo);
        }
    }

    public void UpdateAmmoText(float currentAmmo, float maxAmmo)
    {
        AmmoText.text = currentAmmo + "/" + maxAmmo;
    }
}
