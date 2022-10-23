using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDectection : MonoBehaviour
{
    public float detectDistance;
    public float lateDetectRange;
    public float[] dectectScoreRanges;
    public float detectScale;
    public float resetScaleSpeed;
    public LayerMask notesLayer;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale != Vector3.one)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, resetScaleSpeed * Time.deltaTime);
        }
    }

    public void DectectNote()
    {
        var detectedNote = Physics2D.Raycast(GetCastFromPosition(), Vector3.up, GetCastRange(), notesLayer);
        if(detectedNote)
        {
            var noteController = detectedNote.transform.GetComponent<NoteController>();
            noteController.GetHit();
        } else
        {
        }
        transform.localScale = Vector3.one * detectScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(GetCastFromPosition() + Vector3.up * (GetCastRange() / 2), new Vector3(1, GetCastRange()));
    }

    private Vector3 GetCastFromPosition()
    {
        return transform.position + Vector3.up * (lateDetectRange - 0.5f);
    }

    private float GetCastRange()
    {
        return detectDistance - lateDetectRange;
    }

    public void SetKeyName(string name)
    {

    }

    public void SetColor(Color c)
    {
        sr.color = c;
    }
}
