using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;

    public float maxSteerAngle = 30f;
    public float motorForce = 50f;

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;

    private CarInput _carInput;

    private void Awake()
    {
        _carInput = FindObjectOfType<CarInput>();
    }
    private void FixedUpdate()
    {
        horizontalInput = _carInput.horizontalInput;
        verticalInput = _carInput.verticalInput;

        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        frontLeftWheel.steerAngle = steeringAngle;
        frontRightWheel.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        frontLeftWheel.motorTorque = verticalInput * motorForce;
        frontRightWheel.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftWheel, frontLeftTransform);
        UpdateWheelPose(frontRightWheel, frontRightTransform);
        UpdateWheelPose(rearLeftWheel, rearLeftTransform);
        UpdateWheelPose(rearRightWheel, rearRightTransform);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos, out quat);
        transform.position = pos;
        transform.rotation = quat;
    }
}
