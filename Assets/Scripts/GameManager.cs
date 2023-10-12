using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject[] weaponObjects;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateWeaponObject(int slot)
    {
        for (int i = 0; i < weaponObjects.Length; i++)
        {
            if (i == slot)
            {
                weaponObjects[i].SetActive(true);
            }
            else
            {
                weaponObjects[i].SetActive(false);
            }
        }
    }
}
