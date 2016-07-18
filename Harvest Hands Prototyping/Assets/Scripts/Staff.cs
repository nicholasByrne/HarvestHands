using UnityEngine;
using System.Collections;

public class Staff : MonoBehaviour {


    GameObject Bucket;
    GameObject WaterPuddle;

    bool BucketFill;
    bool PuddleEmpty;

    RaycastHit Hit;

    GameObject BucketWater;
    GameObject StaffGrabber;

    Vector3 camcent;
    Vector3 lastPos;
    Vector3 lastPosRot;

    bool bucketheld;

    float thresholdY;
    float thresholdX;
    float thresholdZ;
    float timeLeft;

	// Use this for initialization
	void Start () {

        BucketFill = false;
        WaterPuddle = GameObject.Find("Puddle");
        BucketWater = GameObject.Find("BucketWater");
        PuddleEmpty = false;
        StaffGrabber = GameObject.Find("Staff Grabber");
        Bucket = GameObject.Find("Bucket");
        bucketheld = false;
        thresholdY = 0.5f; // minimum displacement to recognize.
        thresholdX = 10.5f; // minimum displacement to recognize.
        thresholdZ = 5.0f; // minimum displacement to recognize.
        timeLeft = 0.02f;
        lastPos = Bucket.transform.position;
        lastPosRot = Bucket.transform.rotation.eulerAngles;

	}
	
	// Update is called once per frame
	void Update () {


        Vector3 offset = Bucket.transform.position - lastPos;
        Vector3 offsetrot = Bucket.transform.rotation.eulerAngles - lastPosRot;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        StaffGrabber.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 1));
     

        if (bucketheld == true)
        {
            //these return the bucket to the center of the camera if bucket is offset
            Bucket.transform.parent = StaffGrabber.transform;

         


            if (offset.y > thresholdY || offset.y < -thresholdY)
            {            
                Bucket.transform.position = StaffGrabber.transform.position;
            }

            if (offset.x > thresholdX || offset.x < -thresholdX)
            {
                Bucket.transform.position = StaffGrabber.transform.position;
            }


            if (offset.z > thresholdZ || offset.z < -thresholdZ)
            {

            }

            Bucket.GetComponent<Rigidbody>().useGravity = false;        
            Bucket.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Bucket.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }


        if (bucketheld == true)
        {
            ray = Camera.main.ScreenPointToRay(Vector3.zero);
            timeLeft -= Time.deltaTime;

            //if bucket held , throws the bucket
            if (Input.GetMouseButton(0) && timeLeft < 0.01)
            {
                ray = Camera.main.ScreenPointToRay(Vector3.zero);

                Bucket.GetComponent<Rigidbody>().useGravity = true;
                Bucket.transform.parent = null;
                Bucket.GetComponent<Rigidbody>().AddForce(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 100)));

                bucketheld = false;
            }
        }


        if (Input.GetMouseButton(1))
        {
            //this drops the bucket 
            Bucket.GetComponent<Rigidbody>().useGravity = true;
            Bucket.transform.parent = null;
            bucketheld = false;
        }


        if (Physics.Raycast(ray, out Hit) && Input.GetMouseButton(0) && bucketheld == false)
        {
            //this grabs the bucket
            if (Hit.collider.tag == "Bucket")
            {
                bucketheld = true;
                ray = Camera.main.ScreenPointToRay(Vector3.zero);
                Bucket.transform.parent = StaffGrabber.transform;
                Bucket.GetComponent<Rigidbody>().useGravity = false;
            }

        }
	
	}


    void OnCollisionEnter(Collision coll)
    {   //if bucket colides with something push it back to the grab location
        if (coll.gameObject.name == "Bucket" && bucketheld == true)
        {
            Bucket.transform.position = Vector3.MoveTowards(transform.position, StaffGrabber.transform.position, 1);
        }

    }

}
