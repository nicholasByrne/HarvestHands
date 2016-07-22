using UnityEngine;
using System.Collections;

public class DropOffZone : MonoBehaviour {

    GameObject gameManager;
    ShopScript shop;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager");
        shop = gameManager.GetComponent<ShopScript>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Produce"))
        {
            Debug.Log(name.ToString() + " + " + col.name.ToString());
            PlantProduce produce = col.gameObject.GetComponent<PlantProduce>();
            shop.Score += produce.score;
            Destroy(produce.gameObject);

            //PlayerInventory inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
            //inventory.playerScore += score;
            //Destroy(gameObject);
        }
    }
}
