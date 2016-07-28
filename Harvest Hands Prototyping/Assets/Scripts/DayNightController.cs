using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class DayNightController : NetworkBehaviour
{
    [SyncVar] public int ingameDay = 0;
    [Tooltip("Real world second per in-game day")]
    public float secondsInDay = 120f;

    [Tooltip("0 (midnight), 0.25 (sunrise), 0.5 (midday), 0.75(sunset), 1 (midnight)")]
    //[Tooltip("0 (sunrise) - 1 (sunset)")]

    [Range(0, 1)]
    [SyncVar] public float currentTimeOfDay = 0;
    [SyncVar] public float timeMulitplier = 1f;

    public float startDayAt = 0.25f;
    public float endDayAt = 0.75f;
    
    public Light sun;

    public Transform stars;
    Material sky;

    float sunInitialIntensity;

    private NetworkStartPosition[] spawnPoints;
    private ShopScript shop;
    [SerializeField]
    [Tooltip("Score lost per player that died")]
    int deathPenalty = 0;
    

    
	// Use this for initialization
	void Start ()
    {
        sky = RenderSettings.skybox;
        sunInitialIntensity = sun.intensity;
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        shop = FindObjectOfType<ShopScript>();
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
            CmdUpdatePlants();
            CheckPlayersSafe();            
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
    
    [Command]
    void CmdUpdatePlants()
    {
        foreach(GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
        {
            if (!plant.GetComponent<Plantscript>())
                continue;

            Plantscript plantScript = plant.GetComponent<Plantscript>();
            //if watered
            if (plantScript.isWatered)
            {
                //if not grown yet
                if (!plantScript.ReadyToHarvest)
                {
                    //if ready to grow
                    if (ingameDay >= plantScript.dayPlanted + plantScript.TimeToGrow)
                    {
                        plantScript.ReadyToHarvest = true;
                        RpcSwapPlantGraphics(plantScript.netId, Plantscript.PlantState.Grown);
                    }
                }
            }
            //plant dies
            else
            {
                plantScript.ReadyToHarvest = true;
                plantScript.isAlive = false;
                RpcSwapPlantGraphics(plantScript.netId, Plantscript.PlantState.Dead);
            }
        }
    }

    [ClientRpc]
    void RpcSwapPlantGraphics(NetworkInstanceId id, Plantscript.PlantState state)
    {
        var plant = ClientScene.FindLocalObject(id);
        if(plant == null)
        {
            Debug.LogError("Where is plant? ID: " + id.ToString());
            return;
        }

        var plantScript = plant.GetComponent<Plantscript>();
        plantScript.SwitchPlantState(state);
    }

    void CheckPlayersSafe()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        int playersDead = 0;
        

        foreach(GameObject player in Players)
        {
            //if player is NOT safe
            if (!player.GetComponent<PlayerInventory>().isSafe)
            {
                playersDead++;
                int respawnIndex = Random.Range(0, spawnPoints.Length -1);
                player.transform.position = spawnPoints[respawnIndex].transform.position;
                player.transform.rotation = spawnPoints[respawnIndex].transform.rotation;
                
            }
        }
        int scoreLost = deathPenalty * playersDead;
        Debug.Log(shop.Score + " - " + scoreLost);
        shop.Score -= scoreLost;
        Debug.Log(shop.Score);



    }

}
