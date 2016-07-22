using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Plantscript : NetworkBehaviour {

    [SyncVar] public bool ReadyToHarvest = false;
    [SyncVar]
    public NetworkInstanceId parentNetId;

    [Tooltip("1.0 = 1 full day")]
    public float TimeToGrow;

    public float dayPlanted;
    public float timeOfDay;

    public int minSeedsProduced;
    public int maxSeedsProduced;

    public Material HarvestMaterial;
    public GameObject plantProducePrefab;

    [SyncVar] public bool isWatered = false;
    

    // Use this for initialization
    void Start ()
    {
        if (ReadyToHarvest)
        {
            GetComponent<Renderer>().material = HarvestMaterial;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        GameObject parentObject = ClientScene.FindLocalObject(parentNetId);
        transform.SetParent(parentObject.transform);

    }

    // Update is called once per frame
    void Update ()
    {
	    
	}



    [Command]
    public void CmdHarvest()
    {
        //create produce
        GameObject produce = Instantiate(plantProducePrefab);
        produce.GetComponent<PlantProduce>().ProduceAmount = Random.Range(minSeedsProduced, maxSeedsProduced);
        produce.transform.position = transform.position;

        //Spawn on server
        NetworkServer.Spawn(produce);

        GetComponentInParent<SoilScript>().occupied = false;

        //destroy self
        Destroy(gameObject);
        Debug.Log("PlantHarvested");
    }

}
