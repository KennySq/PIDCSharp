using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drone : MonoBehaviour
{
    Rigidbody mRigid;
    PID32 mAlttitudeController;
    PID32 mPitchController, mRollController, mYawController;

    Camera mMainCamera;
    public float mAlttitude = 10;

    public float mRoll = 0.0f;
    public float mPitch = 0.0f;
    public float mYaw = 0.0f;

    public Text mTextError;
    public Text mTextOutput;
    public Text mTextAlttitude;

    public float mMass;

    void Start()
    {

        mMainCamera = Camera.main;
        mAlttitudeController = new PID32(mAlttitude, 0.4f, 0.1f, 0.5f, 5, -2.5f);
        mRollController = new PID32(mRoll, 0.6f, 0.6f, 0.25f, 5, -3.0f);
        mPitchController = new PID32(mPitch, 0.6f, 0.6f, 0.25f, 5, -3.0f);
        mYawController = new PID32(mYaw, 0.6f, 0.6f, 0.25f, 5, -3.0f);
        


        mRigid = GetComponent<Rigidbody>();
        mMass = mRigid.mass;
    }
    void Update()
    {

        float yOutput = mAlttitudeController.Calibrate(Time.deltaTime, transform.position.y);
        float rollOutput = mRollController.Calibrate(Time.deltaTime, transform.rotation.z);
        float pitchOutput  = mPitchController.Calibrate(Time.deltaTime, transform.rotation.x);
        float yawOutput  = mYawController.Calibrate(Time.deltaTime, transform.rotation.y);
        
        
        float yError = mAlttitudeController.GetError();
        float pitchError = mPitchController.GetError();
        float rollError = mRollController.GetError();
        float yawError = mRollController.GetError();

        mRigid.AddRelativeForce(new Vector3(0, yOutput, 0.0f) * mMass, ForceMode.Acceleration);

        mRigid.AddTorque(new Vector3(pitchOutput, yawOutput, rollOutput) * mMass, ForceMode.Acceleration);


        mTextError.text = yError.ToString();
        mTextOutput.text = yOutput.ToString();

        mTextAlttitude.text = transform.position.y.ToString("N2");

        Vector3 point = mMainCamera.WorldToScreenPoint(transform.position);
        mTextAlttitude.rectTransform.position = point + new Vector3(2, 0, 0);

    }
}
