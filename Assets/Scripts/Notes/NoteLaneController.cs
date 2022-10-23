using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLaneController : MonoBehaviour
{
    public float spawnNoteHeight;
    public GameObject noteDetectorPrefab;
    public GameObject notePrefab;
    private NoteDectection noteDetector;
    private Color color;
    private int notes = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStartData(float detectorHeight, float spawnNoteHeight, string keyName, Color color)
    {
        this.color = color;
        this.spawnNoteHeight = spawnNoteHeight;
        var spawnedDetector = Instantiate(noteDetectorPrefab, transform.position + Vector3.up * detectorHeight, Quaternion.identity, transform);
        noteDetector = spawnedDetector.GetComponent<NoteDectection>();
        noteDetector.SetColor(color);
        noteDetector.SetKeyName(keyName);
        var lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(new Vector3[] { transform.position + Vector3.up * detectorHeight, transform.position + Vector3.up * spawnNoteHeight });
    }

    public void SpawnNote(Func<float> getCurrentTime, float targetTime)
    {
        var spawnedNote = Instantiate(notePrefab, transform.position + Vector3.up * spawnNoteHeight, Quaternion.identity, transform);
        var spawnedNoteController = spawnedNote.GetComponent<NoteController>();
        spawnedNoteController.StartNote(noteDetector.transform.position, getCurrentTime, targetTime, transform.position.y, color);
        notes++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position + Vector3.up * spawnNoteHeight / 2, new Vector3(0.1f, spawnNoteHeight));
    }

    public void ActivateDetector()
    {
        noteDetector.DectectNote();
    }
}
