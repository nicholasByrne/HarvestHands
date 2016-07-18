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

    // Use this for initialization
    void Start () {
       
        BucketFill = false;
        WaterPuddle = GameObject.Find("Puddle");
        BucketWater = GameObject.Find("BucketWater");
        PuddleEmpty = false;     
        Bucket = GameObject.Find("Bucket");

	}
	
	// Update is called once per frame
	void Update () {

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


}
