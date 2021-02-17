using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    private WheelCollider[] wheels;

    public float maxAngle = 30;
    public float maxTorque = 300;
    public GameObject wheelMesh;

    // Start is called before the first frame update
    void Start()
    {
        wheels = GetComponentsInChildren<WheelCollider>();

        for (int i = 0; i < wheels.Length; ++i)
        {
            WheelCollider wheel = wheels[i];
            if (wheelMesh != null)
            {
                GameObject ws = GameObject.Instantiate(wheelMesh);
                ws.transform.parent = wheel.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float angle = maxAngle * Input.GetAxis("Horizontal");
        float torque = maxTorque * Input.GetAxis("Vertical");

        foreach (WheelCollider wheel in wheels)
        {
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = angle;

            if (wheel.transform.localPosition.z < 0)
                wheel.motorTorque = torque;

            if (wheelMesh)
            {
                Quaternion q;
                Vector3 p;
                wheel.GetWorldPose(out p, out q);

                Transform mesh = wheel.transform.GetChild(0);
                mesh.position = p;
                mesh.rotation = q;
            }

        }
    }
}
