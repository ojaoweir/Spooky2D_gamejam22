using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsakUtils;

public class NoteSpawner : MonoBehaviour
{
    private NoteLaneController[] noteLanes;
    public SongBlueprint songBlueprint;
    [Component]
    private AudioSource audioSource;
    private int currentNote;
    private bool active;
    public GameObject thanksForPlaying;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public NoteLaneController GetLane(int laneId)
    {
        return noteLanes[laneId];
    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                active = true;
                audioSource.clip = (songBlueprint.song);
                audioSource.Play();
            }
            return;
        }
        //if (noteLanes != null && noteLanes.Length > 0 && !audioSource.isPlaying)
        //{
        //    audioSource.clip = (songBlueprint.song);
        //    audioSource.Play();
        //} else 
    if  (currentNote >= songBlueprint.GetNotes().Length)
        {
            if (active && audioSource.time >= songBlueprint.song.length - 0.2f)
            {
                thanksForPlaying.SetActive(true);
                enabled = false;
            }
        } 
        else
        {
            SpawnNotes();
        }

    }

    public void SetLanes(NoteLaneController[] noteLanes)
    {
        this.noteLanes = noteLanes;
    }

    private float timeToSpawnNextNote()
    {
        if (currentNote >= songBlueprint.GetNotes().Length) return float.MaxValue;
        var timeForTheNoteToHit = songBlueprint.GetNotes()[currentNote].beatTime.GetTime();
        var timeNoteIsMoving = songBlueprint.timeNotesSpawnBeforeBeat;
        return timeForTheNoteToHit - timeNoteIsMoving;
    }

    private void SpawnNotes()
    {
        if(audioSource.time >= timeToSpawnNextNote())
        {
            SpawnNote();
            SpawnNotes();
        }
    }

    private void SpawnNote()
    {
        var note = songBlueprint.GetNotes()[currentNote];
        noteLanes[note.lane].SpawnNote(() => audioSource.time, note.beatTime.GetTime());
        currentNote++;
    }
}
