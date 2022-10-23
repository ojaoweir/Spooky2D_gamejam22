using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    bool active = false;
    private float avgSpeed;
    private float meetTargetTime;
    private Vector3 targetPosition;
    private Func<float> getCurrentTime;
    private Vector3 startPosition;
    private float startTime;
    private float dieAtY;
    public SpriteRenderer sr;

    public enum States
    {
        Spawned,
        GoingForTarget,
        AfterTarget,
    }

    private States state = States.Spawned;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case States.Spawned:
                //donothing
                break;
            case States.GoingForTarget:
                MoveToTarget();
                break;
            case States.AfterTarget:
                transform.position -= Vector3.up * avgSpeed * Time.deltaTime;
                if (transform.position.y < dieAtY) Destroy(gameObject);
                break;
        }
    }

    public void StartNote(Vector3 targetposition, Func<float> getCurrentTime, float meetTargetTime, float dieAtY, Color color)
    {
        sr.color = color;
        startPosition = transform.position;
        startTime = getCurrentTime.Invoke();
        targetPosition = targetposition;
        this.meetTargetTime = meetTargetTime;
        this.getCurrentTime = getCurrentTime;
        avgSpeed = (transform.position - targetposition).y / (meetTargetTime - startTime);
        state = States.GoingForTarget;
        this.dieAtY = dieAtY;
    }
    
    private void MoveToTarget()
    {
        transform.position = Vector3.Lerp(startPosition, targetPosition, ((getCurrentTime.Invoke() - startTime) / (meetTargetTime - startTime)));
        if(transform.position.y <= targetPosition.y + 0.25)
        {
            state = States.AfterTarget;
        }
    }

    public void GetHit()
    {
        VFXSpawnerController.instance.NoteDestroyed(this);
    }

    public Color GetColor()
    {
        return sr.color;
    }
}
