using UnityEngine;
using System.Collections;



public class Water : MonoBehaviour {

    GameObject Bucket;
    GameObject WaterPuddle;

    bool BucketFill;

    bool PuddleEmpty;

    RaycastHit Hit;

    GameObject BucketWater;

    bool bucketheld;

    bool watertoggle;

    float waterlevel;

    // Use this for initialization
    void Start () {
       
        BucketFill = false;
        WaterPuddle = GameObject.Find("Puddle");
        BucketWater = GameObject.Find("WaterInBucket");
        PuddleEmpty = false;     
        Bucket = GameObject.Find("Bucket");

        watertoggle = false;
        waterlevel = 0.0f;

        
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
            waterlevel -= 0.5f;
        }



        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out Hit) && Input.GetMouseButton(1))
        {
            if (Hit.collider.tag == "Puddle")
            {


                if (PuddleEmpty == false)
                {
                    WaterPuddle.GetComponent<Renderer>().material.mainTexture = null;
                    WaterPuddle.GetComponent<Renderer>().material.color = Color.grey;
                    BucketWater.GetComponent<Renderer>().material.color = Color.blue;
                    BucketFill = true;
                    PuddleEmpty = true;
                }


                //   WaterPuddle.GetComponent<Renderer>().material.color = Color.white;
                //  WaterPuddle.GetComponent<Renderer>().material.SetTexture(1 ,null);
            }
        }


        if (Physics.Raycast(ray, out Hit) && Input.GetMouseButton(1))
        {
            if (Hit.collider.tag == "Puddle")
            {

                if (PuddleEmpty == true)
                {
                    //     WaterPuddle.GetComponent<Renderer>().material.mainTexture = Texture.;
                    WaterPuddle.GetComponent<Renderer>().material.color = Color.blue;
                    BucketWater.GetComponent<Renderer>().material.color = Color.grey;
                    BucketFill = false;
                    PuddleEmpty = false;
                }
        
            }

        }


    }




    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Water")
        {
            waterlevel = 3.0f;
        
        
        }



    }


}
