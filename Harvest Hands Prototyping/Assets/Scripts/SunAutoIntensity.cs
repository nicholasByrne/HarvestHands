using UnityEngine;
using System.Collections;

public class SunAutoIntensity : MonoBehaviour {

    public Gradient nightDayColour;

    public float maxIntensity = 3f;
    public float minIntensity = 0f;
    public float minPoint = -0.2f;

    public float maxAmbient = 1f;
    public float minAmbient = 0f;
    public float minAmbientPoint = -0.2f;

    public Gradient nightDayFogColour;
    public AnimationCurve fogDensityCurve;
    public float fogScale = 1f;

    public float dayAtmosphereThickness = 0.4f;
    public float nightAtmosphereThickness = 0.87f;

    public Vector3 dayRotateSpeed;
    public Vector3 nightRotateSpeed;

    float skySpeed = 1f;

    Light mainLight;
    Skybox sky;
    Material skyMat;

	// Use this for initialization
	void Start ()
    {
        mainLight = GetComponent<Light>();
        skyMat = RenderSettings.skybox;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Calculate Light intensity
        //day length = top(1) - time below horizon (minpoint)
        float tRange = 1 - minPoint;
        //return value between -1 and 1
        float dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
        //Set light intensity
        float intensity = ((maxIntensity - minIntensity) * dot) + minIntensity;
        mainLight.intensity = intensity;

        //Calculate ambient intensity
        tRange = 1 - minAmbientPoint;
        dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
        intensity = ((maxAmbient - minAmbient) * dot) + minAmbient;
        RenderSettings.ambientIntensity = intensity;

        //Calculate main light colour and ambient light colour
        mainLight.color = nightDayColour.Evaluate(dot);
        RenderSettings.ambientLight = mainLight.color;

        //Calculate fog colour and density
        //RenderSettings.fogColor = nightDayFogColour.Evaluate(dot);
        //RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

        //Set atmosphere thickness throughout the day
        //intensity = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
        //skyMat.SetFloat("_AtmosphereThickness", intensity);

        //Rotate Sun
        if (dot > 0)
            transform.Rotate(dayRotateSpeed * Time.deltaTime * skySpeed);
        else
            transform.Rotate(nightRotateSpeed * Time.deltaTime * skySpeed);

        if (Input.GetKeyDown(KeyCode.T))
            skySpeed *= 0.5f;
        if (Input.GetKeyDown(KeyCode.Y))
            skySpeed *= 2f;

        Debug.Log(dot);

	}
}
