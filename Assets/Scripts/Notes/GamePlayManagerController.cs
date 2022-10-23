using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsakUtils;

public class GamePlayManagerController : Singleton<GamePlayManagerController>
{
    [Component]
    private InputListener inputListener;
    [ComponentInChildren]
    private NoteSpawner noteSpawner;
    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLanes(List<NoteLaneController> lanesList)
    {
        var lanes = lanesList.ToArray();
        noteSpawner.SetLanes(lanes);
    }

    public string GetKeyForLane(int laneId)
    {
        return inputListener.GetKeyCodeForLane(laneId).ToString();
    }

    public Color GetColor(int color)
    {
        return colors[color];
    }
}
