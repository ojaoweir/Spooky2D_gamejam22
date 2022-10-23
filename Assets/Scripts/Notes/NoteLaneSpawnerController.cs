using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsakUtils;

public class NoteLaneSpawnerController : MonoBehaviour
{
    public Transform topLeftTransform;
    public Transform bottomRightTransform;
    public Transform noteLanesParent;
    public int numberOfLanes;
    public GameObject noteLane;
    public float gizmoHeight;
    public float heightForDectors;

    [ComponentInParent]
    private GamePlayManagerController gamePlayManagerController;

    private Vector3 bottomLeft { get { return new Vector3(topLeftTransform.position.x, bottomRightTransform.position.y); } }
    private Vector3 bottomRight { get { return bottomRightTransform.position; } }
    private Vector3 topLeft { get { return topLeftTransform.position; } }
    private Vector3 topRight { get { return new Vector3(bottomRightTransform.position.x, topLeftTransform.position.y); } }

    private float width { get { return (topRight - topLeft).x; } }
    private float height { get { return (topLeft - bottomLeft).y;  } }
    // Start is called before the first frame update
    void Start()
    {
        SpawnLanes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnLanes()
    {
        List<NoteLaneController> spawnedNoteLanes = new List<NoteLaneController>();
        for(int i = 0; i < numberOfLanes -1; i++)
        {
            var spawnPosition = bottomLeft + Vector3.right * (width/ (numberOfLanes-1)) * i;
            spawnedNoteLanes.Add(spawnLane(spawnPosition, i));
        }
        spawnedNoteLanes.Add(spawnLane(bottomRight, numberOfLanes - 1));
        gamePlayManagerController.SetLanes(spawnedNoteLanes);
    }

    private NoteLaneController spawnLane(Vector3 spawnPosition, int laneId)
    {
        var spawnedNoteLane = Instantiate(noteLane, spawnPosition, Quaternion.identity, noteLanesParent);
        var spawnedNoteLaneController = spawnedNoteLane.GetComponent<NoteLaneController>();
        spawnedNoteLaneController.SetStartData(heightForDectors, height, gamePlayManagerController.GetKeyForLane(laneId), gamePlayManagerController.GetColor(laneId));
        return spawnedNoteLaneController;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        //bottom indicator where delete missed notes
        Gizmos.DrawCube((bottomRight + bottomLeft)/2, new Vector3(width, gizmoHeight));
        // middle indicator where dectors spawn
        Gizmos.DrawCube((bottomRight + bottomLeft)/2 + Vector3.up * heightForDectors, new Vector3(width, gizmoHeight));
        // top indicator where notes spawn
        Gizmos.DrawCube((topRight + topLeft)/2, new Vector3(width, gizmoHeight));
    }
}
