using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private Camera mainCamera; //camera for mouse position

    private float shotLoadTime = 0f;

    //Gameplay
    public bool shootingInput { get; protected set; }
    public bool shootingInputDown { get; protected set; }
    public bool shootingInputUp { get; protected set; }

    protected bool canShoot, canShootDown;
    public bool reloadInput { get; protected set; }
    public Vector2 mousePosition { get; protected set; }


    public enum InputSet
    {
        gameplay,
    }

    private InputSet currentSet;

    private void Awake()
    {
        SearchCamera();
    }

    private void SearchCamera()
    {
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (cameraObject != null)
        {
            mainCamera = cameraObject.GetComponent<Camera>();
            if (mainCamera != null)
            {
                return;
            }
        }

        Invoke("SearchCamera", 0.2f);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        switch (currentSet)
        {
            case InputSet.gameplay:

                GetGameplayInput();

                CheckCanShoot();

                break;
        }
    }

    private void GetGameplayInput()
    {
        shootingInput = Input.GetMouseButton(0); //left click hold
        shootingInputDown = Input.GetMouseButtonDown(0); //left click
        shootingInputUp = Input.GetMouseButtonUp(0);

        reloadInput = Input.GetKeyDown(KeyCode.R);

        if (mainCamera != null)
        {
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    protected void CheckCanShoot()
    {
        if (shotLoadTime == 0f)
        {
            canShoot = shootingInput;
            canShootDown = shootingInputDown;
        }
        else
        { //if shot has to be loaded
            if (shootingInputDown)
            { //start hold timer
                StartLoad();
            }
            else if (shootingInputUp)
            {
                CancelLoad();
            }
        }
    }

    //output shooting input depending on the parameters
    //if allowHold is false, reset canShootDown (not automatically when loadTime > 0)
    public virtual bool GetShootingInput(bool allowHold, float loadTime)
    {
        SetShotLoadTime(loadTime);
        bool output = canShoot;
        if (!allowHold)
        {
            output = canShootDown;
            canShootDown = false;
        }

        return output;
    }

    //called onPlayerDashEnd
    public void StartLoadAfterDash()
    {
        if (shootingInput)
        {
            StartLoad();
        }
    }

    private void StartLoad()
    {
        Invoke("ShotLoaded", shotLoadTime);
    }

    //called when shootingButton is up or when dashing
    public void CancelLoad()
    {
        if (!canShoot)
        {
            CancelInvoke();
        }
        canShoot = false;
    }

    private void ShotLoaded()
    {
        canShoot = true;
        canShootDown = true;
    }

    public void EnableInputSet(InputSet inputSet)
    {
        ResetValues();
        currentSet = inputSet;
    }


    //reste values when swapping InputSet
    private void ResetValues()
    {
        shootingInput = false;
        shootingInputDown = false;
        reloadInput = false;
        //mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetShotLoadTime(float shotLoadTime)
    {
        if (shotLoadTime >= 0f)
        {
            this.shotLoadTime = shotLoadTime;
        }
        else
        {
            this.shotLoadTime = 0f;
        }
    }

}
