using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fire : MonoBehaviour
{
    bool isReloading;
    [SerializeField] float reloadTime = 0.25f;
    [SerializeField] AudioClip laserSound;

    //Components
    [SerializeField] GameObject BlueLaser;
    AudioSource audioSource;
    Settings settings;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        settings = FindObjectOfType<Settings>();
    }

    private void Start()
    {
        audioSource.clip = laserSound;
    }

    public void FireInput(InputAction.CallbackContext context) // Press Space
    {
        if (context.started) { FireLaser(); }
    }

    void FireLaser()
    {
        if (isReloading) { return;  }

        Vector3 laserPosition = new Vector2(0.345f, 0.005f);
        Vector2 newPosition = transform.position + laserPosition;

        Instantiate(BlueLaser, newPosition, Quaternion.identity);
        audioSource.volume = settings.GetSoundEffectSetting();
        audioSource.Play();

        isReloading = true;
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }
}
