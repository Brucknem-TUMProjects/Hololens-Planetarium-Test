using System.Collections.Generic;
using UnityEngine;

public class MonoMeasurement3D : MonoBehaviour {

    public Measurement3D Measurement { get; private set; }
    private bool initialized = false;
    private MeshRenderer rend;

    public void SetMeasurement(Measurement3D measurement, float size)
    {
        if (!initialized)
        {
            Initialize();
        }
        Measurement = measurement;        
        transform.position = measurement;
        transform.localScale = Vector3.one * size;
        rend.material.color = measurement.Color;
        rend.material.SetFloat("_Falloff", measurement.Fallout);
    }

    private void Initialize()
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        gameObject.AddComponent<MeshFilter>().mesh = quad.GetComponent<MeshFilter>().mesh;
        Destroy(quad);
        rend = gameObject.AddComponent<MeshRenderer>();
        rend.material = new Material(Shader.Find("Custom/Measurement"));
        Texture2D texture = (Texture2D)Resources.Load("Images/FadeOutBillboard");
        rend.material.SetTexture("_MainTex", texture);
        initialized = true;
    }

    public void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
