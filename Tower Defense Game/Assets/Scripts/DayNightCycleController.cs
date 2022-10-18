using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleController : MonoBehaviour
{
    [Range(0, 24)]
    [SerializeField]private float timeOfDay;
    [SerializeField]private float timeMultiplyer = 1f;
    [SerializeField]private AnimationCurve lightChangeCurve;
    [SerializeField]private bool isNightTime;
    [Header("Sun Settings")]
    [SerializeField]private Light sun;
    [SerializeField]private Color dayAmbientLight;
    [SerializeField]private float maxSunLightIntensity = 1f;
    [Header("Moon Settings")]
    [SerializeField]private Light moon;
    [SerializeField]private Color nightAmbientLight;
    [SerializeField]private float maxMoonLightIntensity = 0.5f;

    void Update()
    {
        UpdateTimeOfDay();
        UpdateLighting();
    }

    private void OnValidate()
    {
        UpdateLightRotation();
    }

    private void UpdateTimeOfDay()
    {
        timeOfDay += Time.deltaTime * timeMultiplyer;
        if(timeOfDay > 24)
        {
            timeOfDay = 0;
        }
        if(timeOfDay >= 6 && timeOfDay <= 18)
        {
            isNightTime = false;
        }
        else isNightTime = true;
        UpdateLightRotation();

    }

    private void UpdateLightRotation(){
        float currentTime = timeOfDay / 24f;
        float sunRotation = Mathf.Lerp(-90, 270, currentTime);
        float moonRotation = sunRotation - 180;
        sun.transform.rotation = Quaternion.Euler(sunRotation, -150f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -150f, 0);
    }

    private void UpdateLighting()
    {
        float dotProduct = Vector3.Dot(sun.transform.forward, Vector3.down);
        sun.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moon.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    public bool CheckIsNightTime(){
        return isNightTime;
    }
}
