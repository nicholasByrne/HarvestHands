using UnityEngine;
using System.Collections;
using UnityEngine.Networking;



public class Water : NetworkBehaviour {

    GameObject Bucket;
    GameObject WaterPuddle;

    bool BucketFill;

    bool PuddleEmpty;

    RaycastHit Hit;

    GameObject BucketWater;
    
    [SyncVar]
    bool watertoggle;

    [Tooltip("Current water level")]
    [SyncVar]
    public float waterlevel = 0.0f;

    [Tooltip("How much is used to water a plant")]
    [SyncVar]
    public float waterdrain = 0.5f;

    [Tooltip("How much water the bucket can hold")]
    [SyncVar]
    public float waterfill = 3.0f;

    // Use this for initialization
    void Start () {
       
        BucketFill = false;
        WaterPuddle = GameObject.Find("Puddle");
        BucketWater = GameObject.Find("WaterInBucket");
        PuddleEmpty = false;     
        Bucket = GameObject.Find("Bucket");

        watertoggle = false;
     
        
	}
	
	// Update is called once per frame
	void Update () {

        //BucketWater.transform.position = Bucket.transform.position;


        if (watertoggle == false)
        {
            BucketWater.SetActive(false);
        
        }


        if (watertoggle == true)
        {
            BucketWater.SetActive(true);
        }






     //   if (Bucket)
        if (waterlevel > 0)
        {
            watertoggle = true;
        
        }



        if (waterlevel <= 0)
        {
            watertoggle = false;

        }



        if (waterlevel > 0 && waterlevel <= 1)
        {

            Vector3 tmpPos = BucketWater.transform.localPosition; // Store all Vector3
            tmpPos.y = 0.2f; // example assign individual fox Y axe
            BucketWater.transform.localPosition = tmpPos; // Assign back all Vector3


          //  BucketWater.transform.position.Set(BucketWater.transform.position.x, 0.3f, BucketWater.transform.position.z);
        }


        if (waterlevel > 1 && waterlevel <= 2)
        {


            Vector3 tmpPos = BucketWater.transform.localPosition; // Store all Vector3
            tmpPos.y = 0.5f; // example assign individual fox Y axe
            BucketWater.transform.localPosition = tmpPos; // Assign back all Vector3


           // BucketWater.transform.position.Set(BucketWater.transform.position.x,0.5f,BucketWater.transform.position.z);
        }


        if (waterlevel > 2 && waterlevel <= 3)
        {


            

            Vector3 tmpPos = BucketWater.transform.localPosition; // Store all Vector3
            tmpPos.y = 0.7f; // example assign individual fox Y axe
            BucketWater.transform.localPosition = tmpPos; // Assign back all Vector3


            //BucketWater.transform.position.Set(BucketWater.transform.position.x, 0.7f, BucketWater.transform.position.z);


           
        }




        if (Input.GetMouseButtonDown(2) && waterlevel > 0)
        {
            waterlevel -= waterdrain;
        }
        

    }




    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Water")
        {
            waterlevel = waterfill;
        
        
        }

        if (coll.gameObject.tag == "Plant")
        {
            Debug.Log("Watered by collision");
        }

        


    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Plant")
        {
            Debug.Log("Watered");

            if (waterlevel > 0)
            {                
                Plantscript plant = col.gameObject.GetComponent<Plantscript>();
                if (!plant.isWatered)
                {
                    plant.isWatered = true;
                    waterlevel -= waterdrain;
                }
            }
        }
    }



}
