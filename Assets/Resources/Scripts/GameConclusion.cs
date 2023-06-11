using System.Collections;
using UnityEngine;

public class GameConclusion : MonoBehaviour
{
    public Material[] materials;
    public GameObject materialGameObject; // The GameObject that has the materials.
    public float fadeTime = 5.0f;
    private Renderer objectRenderer;
    private int currentMaterialIndex = 0;

    void Start()
    {
        // Get the Renderer from the GameObject that has the materials.
        if (materialGameObject != null)
        {
            objectRenderer = materialGameObject.GetComponent<Renderer>();
            if (objectRenderer == null)
            {
                Debug.LogError("No Renderer found on the material GameObject.");
            }
        }
        else
        {
            Debug.LogError("No material GameObject set.");
        }
    }

    public void CycleMaterialsAndConcludeGame()
    {
        StartCoroutine(SwitchMaterial());
    }

    IEnumerator SwitchMaterial()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            Color originalColor = objectRenderer.material.color;

            // Fade out the current material
            for (float t = 5.0f; t < fadeTime; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1, 0, t / fadeTime);
                objectRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            // Switch the material to the next one in the array
            objectRenderer.material = materials[i];

            // Reset the color to full opacity before fading in
            objectRenderer.material.color = new Color(objectRenderer.material.color.r, objectRenderer.material.color.g, objectRenderer.material.color.b, 0);

            // Fade in the new material
            for (float t = 5.0f; t < fadeTime; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(0, 1, t / fadeTime);
                objectRenderer.material.color = new Color(objectRenderer.material.color.r, objectRenderer.material.color.g, objectRenderer.material.color.b, alpha);
                yield return null;
            }
        }

        // Activate OVRPassthroughLayer here
        ConcludeGame();

        // Fade out and disable the game object
        StartCoroutine(FadeOutAndDisable());
    }

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
                Debug.LogError("No OVRPassthroughLayer found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("No GameObject found with the given name.");
        }
    }

    IEnumerator FadeOutAndDisable()
    {
        Color originalColor = objectRenderer.material.color;

        for (float t = 5.0f; t < fadeTime; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeTime);
            objectRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}