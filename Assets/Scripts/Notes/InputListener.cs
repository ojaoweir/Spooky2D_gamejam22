using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsakUtils;

public class InputListener : MonoBehaviour
{
    public KeyCode[] laneInput;
    [ComponentInChildren]
    private NoteDetectionActivator noteDetectionActivator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<int> activatedLanes = new List<int>();
        for (int i = 0; i < laneInput.Length; i++)
        {
            if(Input.GetKeyDown(laneInput[i]))
            {
                activatedLanes.Add(i);
            }
        }
        noteDetectionActivator.ActivateDetectors(activatedLanes.ToArray());
    }

    public KeyCode GetKeyCodeForLane(int laneId)
    {
        return laneInput[laneId];
    }
}
