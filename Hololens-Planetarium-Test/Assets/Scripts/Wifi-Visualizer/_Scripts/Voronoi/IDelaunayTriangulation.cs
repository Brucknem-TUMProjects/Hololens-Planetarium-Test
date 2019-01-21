using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class IDelaunayTriangulation
{
    public List<Tetrahedron> Triangulation { get; protected set; }
    public List<Measurement3D> Measurements { get; protected set; }
    public bool IsUpdated { get; set; }
    public bool IsBusy { get; protected set; }

    public IEnumerable<Tetrahedron> Tetrahedrons
    {
        get
        {
            return Triangulation.Where(t => !t.IsArtificial);
        }
    }

    public IDelaunayTriangulation()
    {
        Init();
    }
    
    private Tetrahedron SuperTetrahedron
    {
        get
        {
            return new Tetrahedron(
                new Measurement3D(0f, 0f, -100f, true),
                new Measurement3D(-1000, -1000, 1000, true),
                new Measurement3D(1000, -1000, 1000, true),
                new Measurement3D(0, 1000, 1000, true)
                );
        }
    }

    protected void Init()
    {
        Measurements = new List<Measurement3D>();
        InitTriangulation();
    }

    private void InitTriangulation()
    {
        Triangulation = new List<Tetrahedron>
        {
            SuperTetrahedron
        };
    }


    public abstract void Generate(List<Measurement3D> measurements);
    public abstract void Add(Measurement3D measurement);
    public abstract void AddAll(List<Measurement3D> measurement);

    public float AverageDistance
    {
        get
        {
            Vector3 all = Vector3.one;
            Measurements.ForEach(m => all += m);
            return (all / Measurements.Count).magnitude;
        }
    }
}
