using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StaffNo3 : NetworkBehaviour
{




    //game objects you can pick up
    //add them here first




    [SyncVar]
    RaycastHit Hit;

    //remove syncvar
    //[SyncVar] 
    GameObject StaffGrabber;


    [SyncVar]
    bool objectheld;


    float timeLeft;


    //  [SyncVar]
    GameObject ChosenObj;






    public float throwforce = 500f;

    public float GrabDistance = 3.0f;

    [SyncVar]
    public NetworkInstanceId carriedItemID = NetworkInstanceId.Invalid;




    [Command]
    void CmdPickedUp(NetworkInstanceId pickedUpItemID)
    {
        carriedItemID = pickedUpItemID;
    }

    //   NetworkConnection con1 = NetworkPlayer




    //  [Client]
    //  void staffmove()
    //  {
    //      StaffGrabber = transform.FindChild("FirstCamera").FindChild("Staff Grabber").gameObject;

    //  }




    [Command]
    void CmdDropped()
    {
        //ChosenObj.GetComponent<Rigidbody>().useGravity = true;
        ChosenObj.transform.parent = null;
        objectheld = false;
        Debug.Log(ChosenObj.GetComponent<Pickupable>().beingHeld);
        ChosenObj.GetComponent<Pickupable>().beingHeld = false;
        Debug.Log(ChosenObj.GetComponent<Pickupable>().beingHeld);
        carriedItemID = NetworkInstanceId.Invalid;
        ChosenObj.GetComponent<Rigidbody>().useGravity = true;
        ChosenObj.GetComponent<NetworkIdentity>().localPlayerAuthority = false;
    }

    // Use this for initialization
    void Start()
    {
        //initialize the gameobjects here

        StaffGrabber = transform.FindChild("FirstCamera").FindChild("Staff Grabber").gameObject;
        objectheld = false;
        timeLeft = 0.02f;




    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            if (!objectheld)
            {
                if (carriedItemID != NetworkInstanceId.Invalid)
                {
                    GameObject heldItem = ClientScene.FindLocalObject(carriedItemID);
                    ChosenObj = heldItem;
                    //ChosenObj.transform.parent = StaffGrabber.transform;


                    ChosenObj.GetComponent<Rigidbody>().useGravity = false;
                    objectheld = true;
                }
            }
            else
            {





                //Camera.current.GetComponent<NetworkTransformChild>().

                //    Camera.current.GetComponent<Transform>().FindChild("Staff Grabber").gameObject.GetComponent<NetworkTransformChild>().transform.;

                //   NetworkTransformChild.get

                //  staffmove();
                ChosenObj.GetComponent<Rigidbody>().MovePosition(StaffGrabber.transform.position);








                // SyncVarAttribute



                //  NetworkTransform ChosenTrans = ChosenObj.GetComponent<NetworkTransform>();





                if (carriedItemID == NetworkInstanceId.Invalid)
                {
                    //ChosenObj.transform.parent = null;
                    ChosenObj.GetComponent<Rigidbody>().useGravity = true;
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
                        Debug.Log("SUCESS");


                        //    ChosenObj = Hit.collider.gameObject;
                        Debug.Log(ChosenObj.GetComponent<Pickupable>().beingHeld);
                        //check that another player isn't holding the object
                        if (!ChosenObj.GetComponent<Pickupable>().beingHeld)
                        {
                            objectheld = true;
                            
                            ChosenObj.GetComponent<Pickupable>().beingHeld = true;
                            Debug.Log(ChosenObj.GetComponent<Pickupable>().beingHeld);
                            //ChosenObj.transform.parent = StaffGrabber.transform;
                            ChosenObj.GetComponent<Rigidbody>().useGravity = false;
                            carriedItemID = ChosenObj.GetComponent<NetworkIdentity>().netId;

                            CmdPickedUp(carriedItemID);

                            //  ChosenObj.GetComponent<NetworkIdentity>().localPlayerAuthority = true;


                            //  ChosenObj.GetComponent<NetworkIdentity>().localPlayerAuthority = false;

                            //   ChosenObj.GetComponent<NetworkIdentity>().auth


                            // ChosenObj.GetComponent<NetworkIdentity>().AssignClientAuthority(locl)



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
        else if (ChosenObj == null)
        {
            objectheld = false;
        }
        else
        {

            //    if (carriedItemID != NetworkInstanceId.Invalid)
            //    {





            // staffmove();
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
                ChosenObj.GetComponent<Pickupable>().beingHeld = false;

                objectheld = false;

                CmdDropped();
                Debug.Log(ChosenObj.GetComponent<Pickupable>().beingHeld);
            }

            //StaffGrabber.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 4));


            if (Input.GetMouseButton(1))
            {
                //this drops the bucket 
                ChosenObj.GetComponent<Rigidbody>().useGravity = true;
                ChosenObj.transform.parent = null;
                objectheld = false;
                carriedItemID = NetworkInstanceId.Invalid;
                CmdDropped();
                ChosenObj.GetComponent<Pickupable>().beingHeld = false;
                Debug.Log(ChosenObj.GetComponent<Pickupable>().beingHeld);

            }
        }
    }


    void OnCollisionEnter(Collision coll)
    {   //if bucket colides with something push it back to the grab location
        //ChosenObj.transform.position = Vector3.MoveTowards(transform.position, StaffGrabber.transform.position, 1);
    }
}
