using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StaffNo3 : NetworkBehaviour
{




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

    Vector3 camcent;
    Vector3 lastPos;
    Vector3 lastPosRot;

    [SyncVar]
    [SerializeField]
    bool objectheld;

   
    float timeLeft;


    [SyncVar]
    GameObject ChosenObj;
    Camera m_Camera;

    public float throwforce = 500f;

    public float GrabDistance = 3.0f;

    [SyncVar]
    public NetworkInstanceId carriedItemID;

    // Use this for initialization
    void Start()
    {


        //initialize the gameobjects here
        Bucket = GameObject.Find("Bucket");
        TestItem = GameObject.Find("TestItem");





        BucketFill = false;
        WaterPuddle = GameObject.Find("Puddle");
        BucketWater = GameObject.Find("BucketWater");
        PuddleEmpty = false;
        StaffGrabber = transform.FindChild("Staff Grabber").gameObject;

        carriedItemID = NetworkInstanceId.Invalid;


        objectheld = false;
      
        timeLeft = 0.02f;


        //lastPos = ChosenObj.transform.position;
        //lastPosRot = ChosenObj.transform.rotation.eulerAngles;

       // throwforce = 500f;

    }

    [Command]
    public void CmdPickUp(NetworkInstanceId pickedUpObject)
    {
        carriedItemID = pickedUpObject;
        Debug.Log("commanded triggered: remote player carriedItemID" + carriedItemID);
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            Debug.Log("is remote player");
            if (ChosenObj)
            { 
                Debug.Log("object pos1: " + ChosenObj.transform.position);
                Debug.Log("staffgrabber pos1: " + StaffGrabber.transform.position);
            }
            if (!objectheld)
            {
                Debug.Log("remote player not holding object");
                Debug.Log("remote player carriedItemID" + carriedItemID);

                if (carriedItemID != NetworkInstanceId.Invalid)
                {
                    Debug.Log("remote player picking up object:" + carriedItemID);
                    GameObject heldItem = ClientScene.FindLocalObject(carriedItemID);
                    ChosenObj = heldItem;
                    ChosenObj.transform.parent = StaffGrabber.transform;
                    Debug.Log("object pos2: " + ChosenObj.transform.position);
                    Debug.Log("staffgrabber pos2: " + StaffGrabber.transform.position);
                    objectheld = true;
                }
            }
            else
            {
                if (carriedItemID == NetworkInstanceId.Invalid)
                {
                    ChosenObj.transform.parent = null;
                    ChosenObj = null;
                    objectheld = false;

                }
            }
                return;
        }

        if (objectheld == false)
        {
            if (Input.GetMouseButtonDown(0))
            {


                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

                if (Physics.Raycast(ray, out Hit, GrabDistance))
                {

                    ChosenObj = Hit.collider.gameObject;



                    if ((Hit.collider.gameObject.GetComponent<Pickupable>() != null))
                    {
                        //Debug.Log("SUCESS");
                        
                        
                        ChosenObj = Hit.collider.gameObject;

                        //check that another player isn't holding the object
                        if (!ChosenObj.GetComponent<Pickupable>().beingHeld)
                        {
                            objectheld = true;
                            ChosenObj.transform.parent = StaffGrabber.transform;
                            ChosenObj.GetComponent<Rigidbody>().useGravity = false;
                            carriedItemID = ChosenObj.GetComponent<NetworkIdentity>().netId;
                            CmdPickUp(carriedItemID);

                        }
                        else
                        {
                            ChosenObj = null;
                        }

                    }
                }
            }
        }
        //plants get destroyed sometimes while being held
        else if(ChosenObj == null)
        {
            objectheld = false;
        }
        else
        {

            ChosenObj.GetComponent<Rigidbody>().MovePosition(StaffGrabber.transform.position);


            ChosenObj.GetComponent<Rigidbody>().useGravity = false;
            ChosenObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ChosenObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            timeLeft -= Time.deltaTime;

            //if bucket held , throws the bucket
            if (Input.GetMouseButtonDown(0))
            {
              

                ChosenObj.GetComponent<Rigidbody>().useGravity = true;
                ChosenObj.transform.parent = null;


                ChosenObj.GetComponent<Rigidbody>().AddForce(transform.forward * throwforce);
                carriedItemID = NetworkInstanceId.Invalid;

                objectheld = false;
            }
        }



        //Vector3 offset = ChosenObj.transform.position - lastPos;
        //Vector3 offsetrot = ChosenObj.transform.rotation.eulerAngles - lastPosRot;



        StaffGrabber.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 4));

        //moveposition


        if (Input.GetMouseButton(1))
        {


            //this drops the bucket 
            ChosenObj.GetComponent<Rigidbody>().useGravity = true;
            ChosenObj.transform.parent = null;
            objectheld = false;
            carriedItemID = NetworkInstanceId.Invalid;
        }

    }



    void OnCollisionEnter(Collision coll)
    {   //if bucket colides with something push it back to the grab location
        ChosenObj.transform.position = Vector3.MoveTowards(transform.position, StaffGrabber.transform.position, 1);
    }


}
