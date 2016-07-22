using UnityEngine;
using System.Collections;

public class Scythe : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Plant"))
        {            
            Plantscript plant = col.gameObject.GetComponent<Plantscript>();
            {
                if (plant.ReadyToHarvest)
                {
                    plant.CmdHarvest();
                }
            }
        }
    }


}
