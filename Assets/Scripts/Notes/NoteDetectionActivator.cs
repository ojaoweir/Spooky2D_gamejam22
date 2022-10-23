using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsakUtils;

public class NoteDetectionActivator : MonoBehaviour
{
    [Component]
    NoteSpawner noteSpawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateDetectors(int[] detectors)
    {
        foreach(int laneId in detectors)
        {
            noteSpawner.GetLane(laneId).ActivateDetector();
        }
    }
}
