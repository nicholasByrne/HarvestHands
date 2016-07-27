using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Plantscript : NetworkBehaviour
{

    public enum PlantState
    {
        Sapling,
        Growing,
        Grown,
        Dead,
    }

    [System.Serializable]
    public class MeshState
    {
        public Mesh mesh;
        public Material material;
    }

    [SyncVar]
    public bool ReadyToHarvest = false;
    [SyncVar]
    public bool isWatered = false;
    [SyncVar]
    public bool isAlive = true;
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

    

    public MeshState sapling;
    public MeshState growing;
    public MeshState grown;
    public MeshState dead;


    // Use this for initialization
    void Start()
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
    void Update()
    {

    }

    public void SwitchPlantState(PlantState state)
    {
        var meshFilter = GetComponent<MeshFilter>();
        var rendered = GetComponent<Renderer>();
        switch (state)
        {
            case PlantState.Sapling:
                {
                    meshFilter.mesh = sapling.mesh;
                    rendered.material = sapling.material;
                }
                break;
            case PlantState.Growing:
                {
                    meshFilter.mesh = growing.mesh;
                    rendered.material = growing.material;
                }
                break;
            case PlantState.Grown:
                {
                    meshFilter.mesh = grown.mesh;
                    rendered.material = grown.material;
                }
                break;
            case PlantState.Dead:
                {
                    meshFilter.mesh = dead.mesh;
                    rendered.material = dead.material;
                }
                break;
            default:
                Debug.LogError("U wot m8!?");
                break;
        }
    }

    [Command]
    public void CmdHarvest()
    {
        if (isAlive)
        {
            //create produce
            GameObject produce = Instantiate(plantProducePrefab);
            produce.GetComponent<PlantProduce>().ProduceAmount = Random.Range(minSeedsProduced, maxSeedsProduced);
            produce.transform.position = transform.position;

            //Spawn on server
            NetworkServer.Spawn(produce);
        }
        GetComponentInParent<SoilScript>().occupied = false;

        //destroy self
        Destroy(gameObject);
        Debug.Log("PlantHarvested");
    }

}
