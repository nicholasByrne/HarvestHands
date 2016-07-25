using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerInventory : NetworkBehaviour {

    public bool isSafe;

    ShopScript shop;

    //[SyncVar] public float playerScore = 0;
    //[SyncVar] public int SeedCount = 0;

	// Use this for initialization
	void Start ()
    {
        shop = GameObject.Find("GameManager").GetComponent<ShopScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //spawns seed infront of player
            //shop.CmdSpawnSeeds(transform.position + transform.forward * 2, transform.rotation);

            CmdSpawnSeeds();
        }

    }

    [Command]
    void CmdSpawnSeeds()
    {
        shop.Score--;
        //create seeds
        GameObject seeds = (GameObject)Instantiate(shop.seedsPrefab, transform.position, transform.rotation);
        seeds.transform.position += transform.forward * 2;

        //spawn on clients
        NetworkServer.Spawn(seeds);
    }
    

}
