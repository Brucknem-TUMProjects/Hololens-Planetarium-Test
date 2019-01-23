using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MarkerDebug : MonoBehaviour {
    TrackableBehaviour trackable;
    public TextMesh debug;

	// Use this for initialization
	void Start () {
        trackable = GetComponent<TrackableBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        debug.text = IsTracked + "";
	}

    private bool IsTracked
    {
        get
        {
            var status = trackable.CurrentStatus;
            return status == TrackableBehaviour.Status.TRACKED;
        }
    }
}
