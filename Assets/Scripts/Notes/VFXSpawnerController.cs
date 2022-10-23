using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsakUtils;
using UnityEngine.Rendering.Universal;

public class VFXSpawnerController : Singleton<VFXSpawnerController>
{
    public GameObject VFXParticlesPrefab;
    public GameObject lightFXPrefab;
    public GameObject whiteLightFXPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoteDestroyed(NoteController note)
    {
        var position = note.transform.position;
        var particleSystem = Instantiate(VFXParticlesPrefab, position, Quaternion.identity, transform).GetComponent<ParticleSystem>();
        particleSystem.startColor = note.GetColor();
        var light = Instantiate(lightFXPrefab, position, Quaternion.identity, transform).GetComponent<Light2D>();
        Instantiate(whiteLightFXPrefab, position, Quaternion.identity, transform).GetComponent<Light2D>();
        light.color = note.GetColor();
        light.enabled = true;
        particleSystem.Play();
        Destroy(note.gameObject);
        
    }
}
