using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBooster : MonoBehaviour {

    [SerializeField]
    private float boosterForce = 1.0f;
    private bool boosting = false;

    public Rigidbody CameraRigRigidbody;    

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {

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
            Debug.Log(transform.forward * boosterForce * Time.deltaTime);
        }

    }
}
