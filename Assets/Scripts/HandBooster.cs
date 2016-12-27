using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBooster : MonoBehaviour {

    [SerializeField]
    private float boosterForce = 1.0f;
    [SerializeField]
    private float accelerationSpeed = 1.0f;
    private bool boosting = false;

    public Rigidbody CameraRigRigidbody;
    public TextMesh BoostPowerText;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        if (BoostPowerText) BoostPowerText.text = boosterForce.ToString();
    }

    void Update()
    {

        if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
            float y = Controller.GetAxis().y;
            if (y > 0) boosterForce += accelerationSpeed;
            else boosterForce -= accelerationSpeed;
            if (BoostPowerText) BoostPowerText.text = boosterForce.ToString();
        }
        
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Down");
            boosting = true;
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Up");
            boosting = false;
        }

        if (boosting)
        {
            CameraRigRigidbody.AddForce(transform.forward * boosterForce * Time.deltaTime);
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            transform.parent.position = Vector3.zero;
            CameraRigRigidbody.velocity = Vector3.zero;
        }

        if (Controller.GetHairTriggerDown())
        {
            CameraRigRigidbody.useGravity = !CameraRigRigidbody.useGravity;
        }

    }
}
