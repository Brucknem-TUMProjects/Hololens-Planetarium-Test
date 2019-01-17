using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaunayTriangulatorTest : MonoBehaviour
{

    float delay = 0.5f;
    float size = 2;
    float lastUpdate;

    void Start()
    {
        //StartCoroutine(IterativeRandomTest());
    }

    private void Update()
    {
        if (Time.time - lastUpdate > 0.5)
        {
            lastUpdate = Time.time;
            DelaunayTriangulator.Instance.Add(new Measurement3D(Random.Range(-size / 2, size / 2), Random.Range(-size / 2, size / 2), Random.Range(1, size + 1), "", "", Random.Range(-80, -30), false));
        }
    }

    private IEnumerator IterativeRandomTest()
    {
        for (int i = 0; i < 100; i++)
        {
            DelaunayTriangulator.Instance.Add(new Measurement3D(Random.Range(-size / 2, size / 2), Random.Range(-size / 2, size / 2), Random.Range(1, size + 1), "", "", Random.Range(-80, -30), false));
            yield return new WaitForSeconds(delay);
        }
    }

    private void ManyRandomTest()
    {
        List<Measurement3D> measurements = new List<Measurement3D>();
        for (int i = 0; i < 100; i++)
        {
            measurements.Add(new Measurement3D(Random.Range(-size / 2, size / 2), Random.Range(-size / 2, size / 2), Random.Range(1, size + 1), "", "", Random.Range(-80, -30), false));
        }
        DelaunayTriangulator.Instance.Generate(measurements);
    }

    private IEnumerator CubeTest()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    DelaunayTriangulator.Instance.Add(new Measurement3D(i * 10, j * 10, k * 10, "", "", Random.Range(-80, -30), false));
                    yield return new WaitForSeconds(delay);
                }
            }
        }
    }
}
