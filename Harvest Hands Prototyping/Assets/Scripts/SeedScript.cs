using UnityEngine;
using System.Collections;

public class SeedScript : MonoBehaviour {

    public int NumberOfSeeds = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CollectSeed()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().SeedCount += NumberOfSeeds;
        Destroy(gameObject);
        Debug.Log("SeedCollected");
    }
}
