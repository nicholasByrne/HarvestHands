using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShopScript : NetworkBehaviour
{
    public GameObject seedsPrefab;

    [SyncVar]
    public int Score = 0;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    [Command]
    public void CmdSpawnSeeds(Vector3 position, Quaternion rotation)
    {
        //create seeds
        GameObject seeds = (GameObject)Instantiate(seedsPrefab, position, rotation);
        seeds.transform.position += transform.forward * 2;

        //spawn on clients
        NetworkServer.Spawn(seeds);
    }
}
