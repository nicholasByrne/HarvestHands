using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SeedScript : NetworkBehaviour
{

    [SyncVar] public int NumberOfSeeds = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! fix with staff script?
    [Command]
    public void CmdCollectSeed()
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().SeedCount += NumberOfSeeds;

        Destroy(gameObject);
        Debug.Log("SeedCollected");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Soil"))
        {
            SoilScript soil = col.GetComponent<SoilScript>();
            if (soil.occupied == false)
            {
                soil.CmdPlantSeed(); // pass in type of seed?
                NumberOfSeeds--;
                if (NumberOfSeeds < 1)
                {
                    Destroy(gameObject);
                }
            }
            //else harvest?
            else
                Debug.Log("Soil is occupied.");
        }
    }
}
