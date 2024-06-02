using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Wheels Collider")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider backLeftWheelCollider;
    public WheelCollider backRightWheelCollider;

    [Header("Wheels Transform")]
    public Transform frontLeftWheelTransform;
    public Transform frontRighttWheelTransform;
    public Transform backLeftWheelTransform;
    public Transform backRightWheelTransform;

    [Header("Car Engine")]
    public float accelerationForce = 300f;
    public float breakingForce = 3000f;
    private float presentBreakingForce = 0f;
    private float presentAccelerationForce = 0f;

    [Header("Car Steering")]
    public float wheelsTorque = 35f;
    private float presentTurnAngle = 0f;

    private void Update()
    {
        MoveCar();
        CarSteering();
        ApplyBreak();
    }
    private void MoveCar()
    {
        frontLeftWheelCollider.motorTorque = presentAccelerationForce;
        frontRightWheelCollider.motorTorque = presentAccelerationForce;
        backLeftWheelCollider.motorTorque = presentAccelerationForce;
        backRightWheelCollider.motorTorque = presentAccelerationForce;

        presentAccelerationForce = accelerationForce * Input.GetAxis("Vertical");
    }

    private void CarSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        frontLeftWheelCollider.steerAngle = presentTurnAngle;
        frontRightWheelCollider.steerAngle = presentTurnAngle;

        SteeringWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        SteeringWheels(frontRightWheelCollider, frontRighttWheelTransform);
        SteeringWheels(backRightWheelCollider, backRightWheelTransform);
        SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);
    }

    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position, out rotation);

        WT.position = position;
        WT.rotation = rotation;
    }

    public void ApplyBreak()
    {
        if (Input.GetKey(KeyCode.Space))
            presentBreakingForce = breakingForce;
        else
            presentBreakingForce = 0;

        frontLeftWheelCollider.brakeTorque = presentBreakingForce;
        frontRightWheelCollider.brakeTorque = presentBreakingForce;
        backRightWheelCollider.brakeTorque = presentBreakingForce;
        backLeftWheelCollider.brakeTorque = presentBreakingForce;
    }
}
