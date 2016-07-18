using UnityEngine;
using System.Collections;

public class PlayerRaycast : MonoBehaviour {

    [SerializeField]
    private float RayCastDist;

    private PlayerInventory inventory;
    

	// Use this for initialization
	void Start ()
    {
        inventory = GetComponent<PlayerInventory>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Check if an object is with in RayCastDistance
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(ray, out hit, RayCastDist))
        {
            //Debug.Log(hit.collider.gameObject.name);


            //If looking at Seed
            if (hit.collider.CompareTag("Seed"))
            {
                if (Input.GetKeyDown("f"))
                {
                    hit.transform.GetComponent<SeedScript>().CollectSeed();
                }
            }

            //If looking at Soil
            if (hit.collider.CompareTag("Soil"))
            {
                if (Input.GetKeyDown("f"))
                {
                    SoilScript soil = hit.transform.GetComponent<SoilScript>();
                    if (soil.occupied == false)
                    {
                        if (inventory.SeedCount > 0)
                        {
                            inventory.SeedCount--;
                            soil.PlantSeed(); // pass in type of seed?
                        }
                        else
                            Debug.Log("Out of seeds.");
                    }
                    //else harvest?
                    else 
                        Debug.Log("Soil is occupied.");
                }
            }
            //If looking at Plant
            if (hit.collider.CompareTag("Plant"))
            {
                if (Input.GetKeyDown("f"))
                {
                    Plantscript plant = hit.transform.GetComponent<Plantscript>();
                    {
                        if (plant.ReadyToHarvest)
                        {
                            plant.Harvest();
                        }
                    }
                }
            }
            //If looking at plantProduce
            if (hit.collider.CompareTag("Produce"))
            {
                if (Input.GetKey("f"))
                {
                    PlantProduce produce = hit.transform.GetComponent<PlantProduce>();
                    inventory.SeedCount += produce.ProduceAmount;
                    Destroy(hit.transform.gameObject);
                }
            }






        }
	}
}
