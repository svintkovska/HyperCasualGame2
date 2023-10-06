using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private Ammunition[] ammo;
    [SerializeField] private TMP_Text _timeToLose;
    [SerializeField] private int _timeToLoseValue;
    [SerializeField] private TMP_Text _enemiesToShootText;
    [SerializeField] private int _enemiesToShootValue;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text clipText;
    [SerializeField] private Image gunImage;
    [SerializeField] private Sprite[] gunSprites;


    private float _gameTime;
    private int _enemiesToShoot;
    private int currentGunIndex;

    private void Awake()
    {
        InputManager inputManager = new InputManager();
        inputManager.Player.Enable();
        inputManager.Player.SwapWeapon.performed += SwapWeapon;
    }

    private void Update()
    {
        _gameTime += Time.deltaTime;
        var loseTimeDiffGameTime = _timeToLoseValue - _gameTime;
        _timeToLose.text = $"Time : {((int)loseTimeDiffGameTime)} sec";

        ammoText.text = $"{ammo[currentGunIndex].GetCurrentAmmo()}/{ammo[currentGunIndex].GetMaxAmmo()}";
        clipText.text = $"{ammo[currentGunIndex].GetCurrentClips()}";
    }

    private void SwapWeapon(InputAction.CallbackContext context)
    {
        if (context.control.displayName == "1")
        {
            currentGunIndex = 0;
            gunImage.sprite = gunSprites[currentGunIndex];
            GameManager.Instance.ActivateWeaponObject(currentGunIndex);
        }
        else if (context.control.displayName == "2")
        {

            currentGunIndex = 1;
            gunImage.sprite = gunSprites[currentGunIndex];
            GameManager.Instance.ActivateWeaponObject(currentGunIndex);
        }
    }
}