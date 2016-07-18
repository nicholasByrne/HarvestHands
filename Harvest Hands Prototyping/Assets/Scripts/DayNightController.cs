﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayNightController : MonoBehaviour
{
    public int ingameDay = 0;
    [Tooltip("Real world second per in-game day")]
    public float secondsInDay = 120f;

    [Tooltip("0 (midnight), 0.25 (sunrise), 0.5 (midday), 0.75(sunset), 1 (midnight)")]
    //[Tooltip("0 (sunrise) - 1 (sunset)")]
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    public float timeMulitplier = 1f;

    public float startDayAt = 0.25f;
    public float endDayAt = 0.75f;
    
    public Light sun;

    public Transform stars;
    Material sky;

    float sunInitialIntensity;

    
	// Use this for initialization
	void Start ()
    {
        sky = RenderSettings.skybox;
        sunInitialIntensity = sun.intensity;
    }

    // Update is called once per frame
    void Update ()
    {
        //Update Sun rotation according to time of day
        UpdateSun();
        UpdateStars();

        currentTimeOfDay += (Time.deltaTime / secondsInDay) * timeMulitplier;

        //Check if the day is over
        //if (currentTimeOfDay >= 1)
        if (currentTimeOfDay >= endDayAt)
        {
            //currentTimeOfDay = 0;
            currentTimeOfDay = startDayAt;
            ingameDay++;
            UpdatePlants();
        }


	}

    void UpdateSun()
    {
        //float daylightTime = endDayAt - startDayAt;
        //float wholeDayTime = (1 / daylightTime) *  //* secondsInDay;
        //Debug.Log(wholeDayTime);

        //-90 so that sun rise is at 0.25 instead of 0
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);
        //sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;
        //set intensity to low during night
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        //Increase sunlight intensity over time at sunrise
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        //Fade out sunlight over time at sunset
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
            
        }

        //Set sun intensity
        sun.intensity = sunInitialIntensity * intensityMultiplier;

    }

    void UpdateStars()
    {
        stars.transform.rotation = sun.transform.rotation;
    }

    void UpdatePlants()
    {
        foreach(GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
        {
            Plantscript plantScript = plant.GetComponent<Plantscript>();
            if(!plantScript.ReadyToHarvest)
            {
                Debug.Log("hello");
                if (ingameDay >= plantScript.dayPlanted + plantScript.TimeToGrow)
                {
                    plantScript.ReadyToHarvest = true;
                    plantScript.GetComponent<Renderer>().material = plantScript.HarvestMaterial;
                }
            }
        }
    }

}
