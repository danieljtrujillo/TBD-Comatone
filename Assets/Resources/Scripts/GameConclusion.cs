using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConclusion : MonoBehaviour
{
    public void ConcludeGame()
    {
        GameObject obj = GameObject.Find("OVRCameraRig");

        if (obj != null)
        {
            OVRPassthroughLayer ovrpassthroughlayer = obj.GetComponent<OVRPassthroughLayer>();

            if (ovrpassthroughlayer != null)
            {
                // Enable the script
                ovrpassthroughlayer.enabled = true;
            }
            else
            {
                Debug.LogError("OVRPassthroughLayer found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("No GameObject found with the given name.");
        }
    }
}
