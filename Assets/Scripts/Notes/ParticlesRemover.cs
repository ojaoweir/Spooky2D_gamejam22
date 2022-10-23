using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsakUtils;

public class ParticlesRemover : MonoBehaviour
{
    ParticleSystem particleSystem;
    bool active;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!active && particleSystem.isPlaying)
        {
            active = true;
        }
        if (active && !particleSystem.isPlaying) Destroy(gameObject);
    }
}
