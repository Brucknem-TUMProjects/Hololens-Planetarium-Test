using System.Collections.Generic;
using UnityEngine;

public class MonoMeasurement3D : MonoBehaviour {

    public Measurement3D Measurement { get; private set; }
    private bool initialized = false;
    private MeshRenderer rend;
    SphereCollider coll;

    public void SetMeasurement(Measurement3D measurement)
    {
        if (!initialized)
        {
            Initialize();
        }
        Measurement = measurement;        
        transform.position = measurement;
        transform.localScale = Vector3.one * 0.05f;
        rend.material.color = measurement.Color;
        rend.material.SetFloat("_Falloff", measurement.Falloff);
        rend.material.SetFloat("_Transparency", measurement.Transparency);
    }

    private void Initialize()
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        gameObject.AddComponent<MeshFilter>().mesh = quad.GetComponent<MeshFilter>().mesh;
        Destroy(quad);

        coll = gameObject.AddComponent<SphereCollider>();
        coll.radius = 0.5f;
        coll.isTrigger = true;
        rend = gameObject.AddComponent<MeshRenderer>();
        rend.material = new Material(Shader.Find("Custom/SphereSurf"));
        //Texture2D texture = (Texture2D)Resources.Load("Images/FadeOutBillboard");
        //rend.material.SetTexture("_MainTex", texture);
        initialized = true;
    }

    public void FixedUpdate()
    {
        //transform.LookAt(Camera.main.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.tag != "MainCamera")
        {
            return;
        }
        Destroy(coll);
        Measurement3D current = WiFiScanner.Instance.Scan(transform.position);
        SetMeasurement(current);
        DelaunayTriangulator.Instance.Add(current);
    }
}
