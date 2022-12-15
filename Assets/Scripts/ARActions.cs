using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class ARActions : MonoBehaviour, IMixedRealityTouchHandler
{

    private Vector3 _startPosition;
    public PinchSlider pinchSlider;

    [FormerlySerializedAs("notEyeTrackedColour")] public Material primaryColour;
    [FormerlySerializedAs("eyeTrackedColour")] public Material secondaryColour;

    private bool isGrabbed;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = gameObject.transform.position;
        if (primaryColour != null)
        {
            gameObject.GetComponent<MeshRenderer>().material = primaryColour;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrabbed)
        {
            Vector3 current = gameObject.transform.position;
            gameObject.transform.position = Vector3.MoveTowards(current, _startPosition, Time.deltaTime);
        }
    }

    public void StartGrabbingObject()
    {
        _startPosition = gameObject.transform.position;
        isGrabbed = true;
    }

    public void StopGrabbingObject()
    {
        isGrabbed = false;
    }

    public void ShowObject()
    {
        gameObject.SetActive(true);
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
    }

    public void ScaleObject()
    {
        float value = pinchSlider.SliderValue * 0.2f;
        Debug.Log(value);
        gameObject.transform.localScale = new Vector3(value, value, value);
    }

    public void SetSecondaryColour()
    {
        gameObject.GetComponent<MeshRenderer>().material = secondaryColour;
    }

    public void SetPrimaryColour()
    {
        gameObject.GetComponent<MeshRenderer>().material = primaryColour;
    }


    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        SetSecondaryColour();
    }

    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        SetPrimaryColour();   
    }

    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        
    }
}
