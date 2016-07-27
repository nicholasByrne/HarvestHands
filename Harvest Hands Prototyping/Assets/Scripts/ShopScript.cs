using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShopScript : NetworkBehaviour
{
    public GameObject seedsPrefab;

    [SyncVar]
    public int Score = 0;
    public int oldScore = 0;

    public Text playerUI;

	// Use this for initialization
	void Start ()
    {
        oldScore = Score - 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(oldScore != Score)
        {
            //GameObject.FindGameObjectsWithTag("Player");
            if (playerUI)
                playerUI.text = "Score: " + Score;

            oldScore = Score;
            //Do stuff

        }

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
