using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SoilScript : NetworkBehaviour {

    [SyncVar] public bool occupied = false;

    public GameObject plantPrefab;

    private DayNightController dayNightController;
    
	// Use this for initialization
	void Start ()
    {
        dayNightController = GameObject.Find("GameManager").GetComponent<DayNightController>();
        GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Command]
    public void CmdPlantSeed()
    {
        //create plant
        GameObject myNewPlant = Instantiate(plantPrefab);
        myNewPlant.transform.parent = gameObject.transform;
        myNewPlant.transform.localPosition = plantPrefab.transform.position;
        //myNewPlant.transform.localScale = plantPrefab.transform.localScale;

        //set plant details
        Plantscript plantScript = myNewPlant.GetComponent<Plantscript>();
        plantScript.dayPlanted = dayNightController.ingameDay;
        //plantScript.TimeToGrow = 1;

        plantScript.parentNetId = netId;

        //Add plant to server
        NetworkServer.Spawn(myNewPlant);
    }
        

}
