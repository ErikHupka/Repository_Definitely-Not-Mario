using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayGun : MonoBehaviour
{
    float minRotation = -30f;
    float maxRotation = 30f;
    float rotateGunAngle = 0;

    float rotateGunSpeed = 1f;
    bool isRotatingUp = false;
    bool isRotatingDown = false;
    bool isRotatedLeft;


    Camera mainCamera;
    Vector3 mousePosition;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        //AimGunWithMouse();
        //AimGunWithKeyUp();
        //AimGunWithKeyDown();
    }

    public void AimMouseInput(InputAction.CallbackContext context) // Mouse position
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void AimKeysInputUp(InputAction.CallbackContext context) // "K"
    {
        if (context.started)
        {
            isRotatingUp = true;
        }
        else if (context.canceled)
        {
            isRotatingUp = false;
        }
    }

    public void AimKeysInputDown(InputAction.CallbackContext context) // "J"
    {
        if (context.started)
        {
            isRotatingDown = true;
        }
        else if (context.canceled)
        {
            isRotatingDown = false;
        }
    }

    private void AimGunWithMouse()
    {
        Vector3 dirrection = mousePosition - mainCamera.WorldToScreenPoint(transform.position);
        dirrection.x = Mathf.Abs(dirrection.x);

        if (transform.localScale.x < 0) //Facing left
        {
            dirrection.y *= -1f;
        }

        float angleZ = Mathf.Atan2(dirrection.y, dirrection.x) * Mathf.Rad2Deg;

        angleZ = Mathf.Clamp(angleZ, minRotation, maxRotation);

        Quaternion rotationZ = Quaternion.AngleAxis(angleZ, Vector3.forward);
        Quaternion rotationY = Quaternion.Euler(0f, rotateGunAngle, 0f);

        transform.rotation = rotationZ * rotationY;
    }

    private void AimGunWithKeyUp()
    {
        if (isRotatingDown && !isRotatingUp) { return; }

        transform.Rotate(new Vector3(0, 0, rotateGunSpeed));

        float zRotation = transform.eulerAngles.z;

        if (zRotation > 180f)
        {
            zRotation -= 360f;
        }

        float clampedZRotation = Mathf.Clamp(zRotation, minRotation, maxRotation);

        if (clampedZRotation < 0f)
        {
            clampedZRotation += 360f;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, clampedZRotation);
    }

    private void AimGunWithKeyDown()
    {
        if (isRotatingUp && !isRotatingDown) { return; }

        transform.Rotate(new Vector3(0, 0, -rotateGunSpeed));

        float zRotation = transform.eulerAngles.z;

        if (zRotation > 180f)
        {
            zRotation -= 360f;
        }

        float clampedZRotation = Mathf.Clamp(zRotation, minRotation, maxRotation);

        if (clampedZRotation < 0f)
        {
            clampedZRotation += 360f;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, clampedZRotation);
    }

    public void RotateGunLeft()
    {
        if (!isRotatedLeft)
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, 180f, -currentRotation.eulerAngles.z);
            transform.rotation = newRotation;

            transform.localScale = new Vector2(-1, transform.localScale.y);
            isRotatedLeft = true;
        }
    }

    public void RotateGunRight()
    {
        if (isRotatedLeft)
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, 0f, -currentRotation.eulerAngles.z);
            transform.rotation = newRotation;

            transform.localScale = new Vector2(1, transform.localScale.y);
            isRotatedLeft = false;
        }
    }
}
