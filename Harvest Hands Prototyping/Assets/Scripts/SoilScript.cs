using UnityEngine;
using System.Collections;

public class SoilScript : MonoBehaviour {

    public bool occupied = false;

    public GameObject plantPrefab;

    private DayNightController dayNightController;
    
	// Use this for initialization
	void Start ()
    {
        dayNightController = GameObject.Find("GameManager").GetComponent<DayNightController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void PlantSeed()
    {
        //create plant
        GameObject myNewPlant = Instantiate(plantPrefab);
        myNewPlant.transform.parent = gameObject.transform;
        myNewPlant.transform.localPosition = plantPrefab.transform.position;
        myNewPlant.transform.localScale = plantPrefab.transform.localScale;

        //set plant details
        Plantscript plantScript = myNewPlant.GetComponent<Plantscript>();
        plantScript.dayPlanted = dayNightController.ingameDay;
        plantScript.TimeToGrow = 1;
    }
}
