using System;
using System.Collections.Generic;
using UnityEngine;

public class DelaunayTriangulator : MonoBehaviour
{
    private List<MonoTetrahedron> tetrahedrons;
    private List<MonoMeasurement3D> measurements;

    private GameObject measurementsContainer;
    private GameObject tetrahedronsContainer;

    private IDelaunayTriangulation triangulation;

    static DelaunayTriangulator mInstance;

    public static DelaunayTriangulator Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject go = new GameObject();
                mInstance = go.AddComponent<DelaunayTriangulator>();
                mInstance.Reset();
            }
            return mInstance;
        }
    }

    public void Reset()
    {
        triangulation = new DelaunayTriangulation();
        tetrahedrons = new List<MonoTetrahedron>();
        measurements = new List<MonoMeasurement3D>();
        measurementsContainer = new GameObject("Measurements");
        tetrahedronsContainer = new GameObject("Tetrahedrons");
        measurementsContainer.transform.parent = transform.parent;
        tetrahedronsContainer.transform.parent = transform.parent;
    }

    public void Add(Measurement3D measurement)
    {
        triangulation.Add(measurement);
        Render();
    }

    public void AddAll(List<Measurement3D> measurements)
    {
        triangulation.AddAll(measurements);
        Render();
    }

    public void Generate(List<Measurement3D> measurements)
    {
        triangulation.Generate(measurements);
        Render();
    }

    private void Render()
    {
        RenderTetrahedrons();
        RenderMeasurements();
    }

    private void RenderMeasurements()
    {
        float size = triangulation.AverageDistance * 0.1f;

        for (int i = 0; i < triangulation.Measurements.Count; i++)
        {
            while (i >= measurements.Count)
            {
                GameObject obj = new GameObject("Measurement " + i);
                measurements.Add(obj.AddComponent<MonoMeasurement3D>());
                obj.SetActive(true);
                obj.transform.parent = measurementsContainer.transform;
            }
            measurements[i].SetMeasurement(triangulation.Measurements[i], size);
        }
    }

    private void RenderTetrahedrons()
    {
        IEnumerator<Tetrahedron> toRender = triangulation.Tetrahedrons.GetEnumerator();

        int i = 0;
        while (toRender.MoveNext())
        {
            if (i >= tetrahedrons.Count)
            {
                GameObject obj = new GameObject("Tetrahedron " + i);
                tetrahedrons.Add(obj.AddComponent<MonoTetrahedron>());
                obj.SetActive(true);
                obj.transform.parent = tetrahedronsContainer.transform;
            }
            tetrahedrons[i].SetTetrahedron(toRender.Current);
            i++;
        }
    }
}