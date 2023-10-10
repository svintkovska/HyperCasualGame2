using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Button _startBtn;
    [SerializeField] Button _rulesBtn;
    [SerializeField] Button _controlsBtn;
    [SerializeField] Button _exitBtn;
    [SerializeField] Image _infoBox;
    [SerializeField] TMP_Text _rulesText;
    [SerializeField] TMP_Text _controlsText;


    public void StartGame(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void ShowRules()
    {
        _controlsText.gameObject.SetActive(false);

        _infoBox.gameObject.SetActive(true);
        _rulesText.gameObject.SetActive(true);
    }

    public void ShowControls()
    {
        _rulesText.gameObject.SetActive(false);

        _infoBox.gameObject.SetActive(true);
        _controlsText.gameObject.SetActive(true);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

}
