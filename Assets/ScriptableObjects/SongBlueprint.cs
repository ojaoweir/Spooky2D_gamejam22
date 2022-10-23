using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New SongBlueprint", menuName = "SongBlueprint")]
public class SongBlueprint : ScriptableObject
{
    [SerializeField]
    private NoteEntry[] notes;
    [SerializeField]
    private RepeatingNoteEntry[] repeatingNotes;
    public AudioClip song;
    private NoteEntry[] allNotes;
    public float timeNotesSpawnBeforeBeat;

    public NoteEntry[] GetNotes()
    {
        if(allNotes != null && allNotes.Length != 0)
        {
            return allNotes;
        } else
        {
            List<NoteEntry> allNotesList = new List<NoteEntry>();
            foreach (RepeatingNoteEntry r in repeatingNotes)
            {
                allNotesList.AddRange(r.GetNoteEntries());
            }
            allNotesList.AddRange(notes);
            allNotesList.Sort((a, b) => a.beatTime.GetTime().CompareTo(b.beatTime.GetTime()));
            foreach (NoteEntry n in allNotesList)
            {
                Debug.Log(n.beatTime.GetTime());
            }
            allNotes = allNotesList.ToArray();
            return allNotes;
        }
    }
}
[System.Serializable]
public class NoteEntry
{
    public TimeEntry beatTime;
    public int lane;

    public NoteEntry(TimeEntry beatTime, int lane)
    {
        this.beatTime = beatTime;
        this.lane = lane;
    }
}

[System.Serializable]
public class RepeatingNoteEntry
{
    public TimeEntry startbeatTime;
    public TimeEntry endbeatTime;
    public TimeEntry timeBetweenBeats;
    public int lane;

    public List<NoteEntry> GetNoteEntries()
    {
        float start = startbeatTime.GetTime();
        float end = endbeatTime.GetTime();
        float iteration = timeBetweenBeats.GetTime();
        var time = start;
        List<NoteEntry> notes = new List<NoteEntry>();
        while (time <  end)
        {
            notes.Add(new NoteEntry(new TimeEntry(time), lane));
            time += iteration;
        }
        return notes;
    }
}

[System.Serializable]
public class TimeEntry
{
    public int minutes;
    public int seconds;
    public int milliseconds;

    public TimeEntry()
    {

    }
    public TimeEntry(float time)
    {
        minutes = (int)(time / 60);
        seconds = (int)(time - (minutes * 60));
        milliseconds = (int) ((time - (int)time) * 1000);
    }

    public float GetTime()
    {
        return (float) minutes * 60 + (float) seconds + ((float) milliseconds )/ 1000;
    }
}
