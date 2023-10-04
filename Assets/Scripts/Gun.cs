using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private AudioSource gunSound;

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
                hit.transform.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }
}
