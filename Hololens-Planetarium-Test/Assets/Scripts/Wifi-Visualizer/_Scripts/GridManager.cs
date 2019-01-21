using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int xx = 10;
    public int yy = 10;
    public int zz = 10;

    public float dx = 0.2f;
    public float dy = 0.2f;
    public float dz = 0.2f;

    // Use this for initialization
    void Start()
    {
        Camera.main.GetComponent<SphereCollider>().radius = (Mathf.Min(dx, dy, dz) - 0.05f) / 2f - 0.001f;
        for (float x = 0; x <= xx; x++)
        {
            for (float y = 0; y <= yy; y++)
            {
                for (float z = 0; z <= zz; z++)
                {
                    GameObject obj = new GameObject("Mono Measurement (" + x + "," + y + "," + z + ")");
                    MonoMeasurement3D mono = obj.AddComponent<MonoMeasurement3D>();
                    mono.SetMeasurement(new Measurement3D(
                        ((x / xx) - 0.5f) * (xx * dx),
                        ((y / yy) - 0.5f) * (yy * dy),
                        ((z / zz) - 0.5f) * (zz * dz),
                        true));
                    obj.transform.parent = transform;
                }
            }
        }
    }
}