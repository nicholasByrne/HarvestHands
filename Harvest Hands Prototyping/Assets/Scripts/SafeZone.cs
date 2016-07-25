using UnityEngine;
using System.Collections;

public class SafeZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerInventory>().isSafe = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerInventory>().isSafe = false;
        }
    }

}
