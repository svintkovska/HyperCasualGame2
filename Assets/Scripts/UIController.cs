using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private Button _nextLvlBtn;
    [SerializeField] private TMP_Text _loseText;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Sprite[] gunSprites;

    private float _gameTime;
    private int _enemiesToShoot;
    private int currentGunIndex;

    private void Awake()
    {
        Time.timeScale = 1f;
        Instance = this;     
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
        LoseCheck();

        CheckWin();

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
            _background.gameObject.SetActive(true);
            _winText.gameObject.SetActive(true);
            _nextLvlBtn.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    private void LoseCheck()
    {
        _gameTime += Time.deltaTime;
        var loseTimeDiffGameTime = _timeToLoseValue - _gameTime;
        _timeToLose.text = $"Time to lose: {((int)loseTimeDiffGameTime)}";

        if (_gameTime >= _timeToLoseValue && _enemiesToShoot > 0)
        {
            _background.gameObject.SetActive(true);
            _loseText.gameObject.SetActive(true);
            _restartBtn.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}