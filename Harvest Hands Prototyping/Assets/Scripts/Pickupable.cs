using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Pickupable : NetworkBehaviour
{
    [SyncVar]
    public bool beingHeld;
    [SyncVar]
    public NetworkInstanceId parentNetId;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
