using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private bool isAutomatic;
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private Camera fpsCam;

    private float range = 100f;
    private float attackTime;
    private float recoilSpeed = -2f;
    private bool isFiring;

    private AudioSource gunSound;
    private Ammunition ammo;
    private InputManager inputManager;

    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.Player.Enable();

        gunSound = GetComponent<AudioSource>();
        ammo = GetComponent<Ammunition>();
    }

    private void OnEnable()
    {
        inputManager.Player.Shoot.performed += Shoot;
        inputManager.Player.Shoot.canceled += Shoot_canceled;
    }
    private void OnDisable()
    {
        inputManager.Player.Shoot.performed -= Shoot;
        inputManager.Player.Shoot.canceled -= Shoot_canceled;
    }

    private void Update()
    {
        if (isFiring && ammo.GetCurrentAmmo() > 0)
        {
            attackTime += Time.deltaTime;

            if (attackTime >= fireRate)
            {
                attackTime = 0f;
                IsShooting();
            }
        }
        else
        {
            isFiring = false;
        }
    }
    private void IsShooting()
    {
        ammo.ReduceCurrentAmmo();
        gunSound.Play();
        CameraShake.instance.ShakeCamera(1f, 0.25f);
        GunUpWhenShooting();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Component damageable = hit.transform.GetComponent(typeof(IDamageable));
                if (damageable)
                {
                    GameFunctions.Attack(damageable, damage);
                    hit.transform.gameObject.GetComponentInChildren<ParticleSystem>().Play();
                    hit.transform.gameObject.GetComponent<EnemyAI>().OnDamageTaken();
                }
            }
        }
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if(isAutomatic && ammo.GetCurrentAmmo() > 0)
        {
            isFiring= true;
        }
        else if(!isAutomatic && ammo.GetCurrentAmmo() > 0)
        {
            IsShooting();
        }
        
    }
    private void GunUpWhenShooting()
    {
        cameraRoot.Rotate(recoilSpeed, 0, 0);
    }
    private void Shoot_canceled(InputAction.CallbackContext obj)
    {
        isFiring = false;
    }
}
