using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;




public class M2 : MonoBehaviour {


    GameObject Player;
    public float movementspeed = 100;
    FirstPersonController bScript;


    // Use this for initialization
    void Start()
    {

        Player = GameObject.Find("Player");


        bScript = Player.GetComponent<FirstPersonController>();
        
    }

    // Update is called once per frame
    void Update()
    {
      

       


        if (Input.GetKey(KeyCode.W))
        {
            // Player.GetComponent<Rigidbody>().AddForce(new Vector3(10, 0, 0));

            Player.transform.Translate(Vector3.forward * movementspeed * Time.deltaTime);
        }



        if (Input.GetKey(KeyCode.S))
        {
            // Player.GetComponent<Rigidbody>().AddForce(new Vector3(10, 0, 0));

            Player.transform.Translate(-Vector3.forward * movementspeed * Time.deltaTime);
        }






        if (Input.GetKey(KeyCode.A))
        {
            // Player.GetComponent<Rigidbody>().AddForce(new Vector3(10, 0, 0));

            Player.transform.Translate(-Vector3.right * movementspeed * Time.deltaTime);
        }



        if (Input.GetKey(KeyCode.D))
        {
            // Player.GetComponent<Rigidbody>().AddForce(new Vector3(10, 0, 0));

            Player.transform.Translate(Vector3.right * movementspeed * Time.deltaTime);
        }



       


    }
}
