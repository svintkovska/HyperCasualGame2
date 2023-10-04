using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private AudioSource gunSound;
    [SerializeField] private float damage = 20f;

    private void Awake()
    {
        InputManager inputManager = new InputManager();
        inputManager.Player.Enable();
        inputManager.Player.Shoot.performed += Shoot;
        gunSound = GetComponent<AudioSource>();
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        gunSound.Play();
        CameraShake.instance.ShakeCamera(1f, 0.25f);
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if(hit.transform.CompareTag("Enemy"))
            {
                Component damageable = hit.transform.GetComponent(typeof(IDamageable));
                if(damageable)
                {
                    GameFunctions.Attack(damageable, damage);
                    hit.transform.gameObject.GetComponentInChildren<ParticleSystem>().Play();
                    hit.transform.gameObject.GetComponent<EnemyAI>().OnDamageTaken();
                }
            }
        }
    }
}
