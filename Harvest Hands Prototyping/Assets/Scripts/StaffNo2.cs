using UnityEngine;
using System.Collections;

public class StaffNo2 : MonoBehaviour {




    //game objects you can pick up
    //add them here first
    GameObject Bucket;
    GameObject TestItem;







    GameObject WaterPuddle;

    bool BucketFill;
    bool PuddleEmpty;

    RaycastHit Hit;

    GameObject BucketWater;
    GameObject StaffGrabber;



    //array of objects to pick up
    GameObject[] arrayOfPickupItems;



    Vector3 camcent;
    Vector3 lastPos;
    Vector3 lastPosRot;

    bool objectheld;

    float thresholdY;
    float thresholdX;
    float thresholdZ;
    float timeLeft;




    int i;

	// Use this for initialization
	void Start () {



        //initialize the gameobjects here
        Bucket = GameObject.Find("Bucket");
        TestItem = GameObject.Find("TestItem");



       

        BucketFill = false;
        WaterPuddle = GameObject.Find("Puddle");
        BucketWater = GameObject.Find("BucketWater");
        PuddleEmpty = false;
        StaffGrabber = GameObject.Find("Staff Grabber");





       





        objectheld = false;
        thresholdY = 4.5f; // minimum displacement to recognize.
        thresholdX = 14.5f; // minimum displacement to recognize.
        thresholdZ = 5.0f; // minimum displacement to recognize.
        timeLeft = 0.02f;
       

        //how many objects there are to pick up
         arrayOfPickupItems = new GameObject[3];



        //add the next pickup item into the array
         arrayOfPickupItems[0] = null;
         arrayOfPickupItems[1] = Bucket;
         arrayOfPickupItems[2] = TestItem;

         i = 1;


         lastPos = arrayOfPickupItems[i].transform.position;
         lastPosRot = arrayOfPickupItems[i].transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {

       


            



            Vector3 offset = arrayOfPickupItems[i].transform.position - lastPos;
            Vector3 offsetrot = arrayOfPickupItems[i].transform.rotation.eulerAngles - lastPosRot;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            StaffGrabber.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 4));


            if (objectheld == true)
            {
                //these return the bucket to the center of the camera if bucket is offset
                arrayOfPickupItems[i].transform.parent = StaffGrabber.transform;




                if (offset.y > thresholdY || offset.y < -thresholdY)
                {
                    arrayOfPickupItems[i].transform.position = StaffGrabber.transform.position;
                }

                if (offset.x > thresholdX || offset.x < -thresholdX)
                {
                    arrayOfPickupItems[i].transform.position = StaffGrabber.transform.position;
                }


                if (offset.z > thresholdZ || offset.z < -thresholdZ)
                {

                }

                arrayOfPickupItems[i].GetComponent<Rigidbody>().useGravity = false;
                arrayOfPickupItems[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                arrayOfPickupItems[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }


            if (objectheld == true)
            {
                ray = Camera.main.ScreenPointToRay(Vector3.zero);
                timeLeft -= Time.deltaTime;

                //if bucket held , throws the bucket
                if (Input.GetMouseButton(0) && timeLeft < 0.01)
                {
                    ray = Camera.main.ScreenPointToRay(Vector3.zero);

                    arrayOfPickupItems[i].GetComponent<Rigidbody>().useGravity = true;
                    arrayOfPickupItems[i].transform.parent = null;
                    arrayOfPickupItems[i].GetComponent<Rigidbody>().AddForce(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 100)));

                    objectheld = false;
                }
            }


            if (Input.GetMouseButton(1))
            {
                //this drops the bucket 
                arrayOfPickupItems[i].GetComponent<Rigidbody>().useGravity = true;
                arrayOfPickupItems[i].transform.parent = null;
                objectheld = false;
            }

























            if (Physics.Raycast(ray, out Hit) && Input.GetMouseButton(0) && objectheld == false)
            {
                //this grabs the bucket



                //duplicate this if statement , but with the collider tag for the new object and the objects array number for i
                if (Hit.collider.tag == "Bucket")
                {
                    i = 1;
                    objectheld = true;
                    ray = Camera.main.ScreenPointToRay(Vector3.zero);
                    arrayOfPickupItems[i].transform.parent = StaffGrabber.transform;
                    arrayOfPickupItems[i].GetComponent<Rigidbody>().useGravity = false;
                }




                if (Hit.collider.tag == "TestItem")
                {
                    i = 2;
                    objectheld = true;
                    ray = Camera.main.ScreenPointToRay(Vector3.zero);
                    arrayOfPickupItems[i].transform.parent = StaffGrabber.transform;
                    arrayOfPickupItems[i].GetComponent<Rigidbody>().useGravity = false;
                }




            }
        }
	


    void OnCollisionEnter(Collision coll)
    {   //if bucket colides with something push it back to the grab location


        //duplicate this if statement , but with the Gameobject name for the new object and the objects array number for i
            if (coll.gameObject.name == "Bucket" && objectheld == true)
            {
                i = 1;
                arrayOfPickupItems[i].transform.position = Vector3.MoveTowards(transform.position, StaffGrabber.transform.position, 1);
            }



            if (coll.gameObject.name == "TestItem" && objectheld == true)
            {
                i = 2;
                arrayOfPickupItems[i].transform.position = Vector3.MoveTowards(transform.position, StaffGrabber.transform.position, 1);
            }




        }
    
	
	
}
