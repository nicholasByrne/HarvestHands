using UnityEngine;
using System.Collections;

public class PlantProduce : MonoBehaviour
{

    public int ProduceAmount = 1;
    public int score = 10;

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.gameObject.CompareTag("DropOffZone"))
    //    {
    //        PlantProduce produce = col.gameObject.GetComponent<PlantProduce>();
    //        PlayerInventory inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    //        inventory.playerScore += score;
    //        Destroy(gameObject);
    //    }
    //}
}
