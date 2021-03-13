using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TrainingRoomPP : MonoBehaviour
{
    [SerializeField]
    private GameObject mPostProcessing;
    private PostProcessVolume mPostProcessVolume;
    private Bloom mbloomLayer;
    private bool lerpTrigger;
    private float endVal;
    
    private void Start()
    {
        mPostProcessVolume = mPostProcessing.GetComponent<PostProcessVolume>();
        mPostProcessVolume.profile.TryGetSettings(out mbloomLayer);
        mbloomLayer.intensity.value = 1.0f;
        endVal = 30.0f;
    }

    private void Update()
    {
        //Debug.Log(mbloomLayer.intensity.value);

        if (mbloomLayer.intensity.value > 25.0f)
        {
            endVal = 1.0f;
        }
        else if (mbloomLayer.intensity.value < 10.0f)
        {
            endVal = 30.0f;
        }

        mbloomLayer.intensity.value = Mathf.Lerp(mbloomLayer.intensity.value, endVal, Time.deltaTime);


    }
}
