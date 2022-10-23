using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsakUtils;
using UnityEngine.Rendering.Universal;

public class LightRemover : MonoBehaviour
{
    Light2D light;
    public float lifeTime;
    private float lifeTimer;
    private float startIntensity;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        lifeTimer = lifeTime;
        startIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        light.intensity = Mathf.Lerp(0, startIntensity, (lifeTimer / lifeTime));
        if (lifeTimer <= 0) Destroy(gameObject);
    }
}
