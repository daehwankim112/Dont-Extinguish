using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class FireManager : MonoBehaviour
{
    public GameObject logText;
    public float log = 100;
    public float speed;
    public Transform fire;
    public GameObject treeSpawner;
    public float burningRadius;

    private ParticleSystem particle_magicFireProOrange;
    private ParticleSystem particle_sparks;
    private ParticleSystem particle_fire;
    private ParticleSystem particle_smoke;
    private ParticleSystem particle_fireDark;

    private List<ParticleSystem> particles = new List<ParticleSystem>();
    private List<float> particle_startLifeTime = new List<float>();
    private List<float> particle_emissionRateOverTime = new List<float>();


    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        particle_magicFireProOrange = fire.gameObject.GetComponent<ParticleSystem>();
        particle_sparks = fire.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        particle_fire = fire.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        particle_smoke = fire.GetChild(2).gameObject.GetComponent<ParticleSystem>();
        particle_fireDark = fire.GetChild(3).gameObject.GetComponent<ParticleSystem>();
        Debug.Log("name of ParticleSystem: " + particle_sparks.name + ", type: " + particle_sparks.GetType() + ", name of GameObject: " + fire.GetChild(0).gameObject.name);
        particles.Add(particle_magicFireProOrange);
        particles.Add(particle_sparks);
        particles.Add(particle_fire);
        particles.Add(particle_smoke);
        particles.Add(particle_fireDark);
        foreach( ParticleSystem p in particles )
        {
            particle_startLifeTime.Add(p.main.startLifetime.constant);
            particle_emissionRateOverTime.Add(p.emission.rateOverTime.constant);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( GameController.isPlaying )
        {
            log -= (Time.deltaTime * speed);
            Debug.Log("log: " + log);

            // Updates size of Fire particles based on its health "log"
            for ( int i = 0; i < particles.Count; i ++ )
            {
                ParticleSystem.MainModule tempMain = particles[i].main;
                ParticleSystem.EmissionModule tempEmission = particles[i].emission;
                tempMain.startLifetime = particle_startLifeTime[i] * log / 100;
                tempEmission.rateOverTime = particle_emissionRateOverTime[i] * log / 100;
            }
        }
        if ( GameController.reset )
        {
            log = 100;
        }
        if ( log < 1 )
        {
            GameController.isPlaying = false;
        }
        foreach(Transform child in treeSpawner.transform)
        {
            if (Vector3.Distance(this.transform.position, child.transform.position) < burningRadius)
            {
                child.gameObject.GetComponent<TreeController>().burning = true;
            }
        }
        logText.GetComponent<TMP_Text>().text = "log: " + (int) log + "";
    }
    /*
    private bool LogDropped()
    {
        
    }
    */
}
