using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeController : MonoBehaviour
{
    [SerializeField]private float timeMultiplyer = 750f;
    [SerializeField]private float startHour = 12f;
    [SerializeField]private Light sunLight;
    [SerializeField]private Light moonLight;
    [SerializeField]private float sunRiseHour = 7f;
    [SerializeField]private float sunSetHour = 20f;
    [SerializeField]private Color dayAmbientLight;
    [SerializeField]private Color nightAmbientLight;
    [SerializeField]private AnimationCurve lightChangeCurve;
    [SerializeField]private float maxSunLightIntensity = 1f;
    [SerializeField]private float maxMoonLightIntensity = 0.5f;
    private DateTime currentTime;
    private TimeSpan sunRiseTime;
    private TimeSpan sunSetTime;

    [SerializeField]private string currentTimeText;



    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunRiseTime = TimeSpan.FromHours(sunRiseHour);
        sunSetTime = TimeSpan.FromHours(sunSetHour);
    }

    void Update()
    {
        UpdateTimeOfDay();
        RotateSunAndMoon();
        UpdateLighting();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplyer);
        currentTimeText = currentTime.ToString("HH:mm");
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if(difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }
        return difference;
    }

    private void RotateSunAndMoon()
    {
        float sunLightRotation;
        float moonLightRotation;

        if(currentTime.TimeOfDay > sunRiseTime && currentTime.TimeOfDay < sunSetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunRiseTime, sunSetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunRiseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
            moonLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunSetTime, sunRiseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunSetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
            moonLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
        moonLight.transform.rotation = Quaternion.AngleAxis(moonLightRotation, Vector3.right);
    }

    private void UpdateLighting()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    public void GetCurrentTimeOfDay(){
        
    }
}
