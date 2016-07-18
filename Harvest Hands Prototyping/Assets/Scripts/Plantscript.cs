using UnityEngine;
using System.Collections;

public class Plantscript : MonoBehaviour {

    public bool ReadyToHarvest = false;

    [Tooltip("1.0 = 1 full day")]
    public float TimeToGrow;

    public float dayPlanted;
    public float timeOfDay;

    public int minSeedsProduced;
    public int maxSeedsProduced;

    public Material HarvestMaterial;
    public GameObject plantProducePrefab;
    

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
	    //if (!ReadyToHarvest)
        //{
        //    if (dayNightController.ingameDay  >= dayPlanted + TimeToGrow)
        //    {
        //        ReadyToHarvest = true;
        //        GetComponent<Renderer>().material = HarvestMaterial;
        //    }
        //}
	}

    public void Harvest()
    {
        //create produce
        GameObject produce = Instantiate(plantProducePrefab);
        produce.GetComponent<PlantProduce>().ProduceAmount = Random.Range(minSeedsProduced, maxSeedsProduced);
        produce.transform.position = transform.position;

        GetComponentInParent<SoilScript>().occupied = false;

        //destroy self
        Destroy(gameObject);
        Debug.Log("PlantHarvested");
    }

}
