using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CarWizard : EditorWindow
{
    private int axlesCount = 2;
    private float mass = 1000;
    private float axleStep = 2;
    private float axleWidth = 2;
    private float axleShift = -0.5f;

    [MenuItem ("Vehicles/Create Car")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CarWizard));
    }

    void OnGUI()
    {
        axlesCount = EditorGUILayout.IntSlider("Axles: ", axlesCount, 2, 10);
        mass = EditorGUILayout.FloatField("Mass: ", mass);
        axleStep = EditorGUILayout.FloatField("Axle Step: ", axleStep);
        axleWidth = EditorGUILayout.FloatField("Axle Width: ", axleWidth);
        axleShift = EditorGUILayout.FloatField("Axle Shift: ", axleShift);

        if (GUILayout.Button("Generate"))
        {
            CreateCar();
        }

    }

    void CreateCar()
    {
        GameObject root = new GameObject("carRoot");
        Rigidbody rootBody = root.AddComponent<Rigidbody>();
        rootBody.mass = mass;

        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cube);

        body.transform.parent = root.transform;

        float length = (axlesCount - 1) * axleStep;
        float firstOffset = length / 2;

        body.transform.localScale = new Vector3(axleWidth, 1, length);

        for (int i = 0; i < axlesCount; i++)
        {
            GameObject leftWheel = new GameObject(string.Format("LeftWheel", i));
            GameObject rightWheel = new GameObject(string.Format("RightWheel", i));

            leftWheel.AddComponent<WheelCollider>();
            rightWheel.AddComponent<WheelCollider>();

            leftWheel.transform.parent = root.transform;
            rightWheel.transform.parent = root.transform;

            leftWheel.transform.localPosition = new Vector3(-axleWidth / 2, axleShift, firstOffset - axleStep * i);
            rightWheel.transform.localPosition = new Vector3(axleWidth / 2, axleShift, firstOffset - axleStep * i);
        }

        root.AddComponent<Suspension>();
        root.AddComponent<SimpleController>();

    }
}
