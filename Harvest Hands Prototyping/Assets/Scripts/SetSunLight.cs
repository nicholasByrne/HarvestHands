using UnityEngine;
using System.Collections;

public class SetSunLight : MonoBehaviour {

    public Transform stars;
    Material sky;


	// Use this for initialization
	void Start ()
    {
        sky = RenderSettings.skybox;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Rotate stars
        stars.transform.rotation = transform.rotation;
	}
}
