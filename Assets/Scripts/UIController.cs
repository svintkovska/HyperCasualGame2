using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }      
    }

    private void Start()
    {
        InputManager inputManager = new InputManager();
        inputManager.Player.Enable();
        inputManager.Player.SwapWeapon.performed += SwapWeapon;
        _enemiesToShoot = _enemiesToShootValue;
    }
    private void Update()
    {
        CheckWin();

        _gameTime += Time.deltaTime;
        var loseTimeDiffGameTime = _timeToLoseValue - _gameTime;
        _timeToLose.text = $"Time : {((int)loseTimeDiffGameTime)} sec";
        _enemiesToShootText.text = $"Enemies: {_enemiesToShoot}";
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

    public void DecreaseEnemiesToKill()
    {
        _enemiesToShoot--;
        _enemiesToShootText.text = $"Enemies: {_enemiesToShoot}";
    }

    private void CheckWin()
    {
        if (_enemiesToShoot <= 0 && _gameTime < _timeToLoseValue)
        {
            Debug.Log("You win!");
        }
    }
}