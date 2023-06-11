using UnityEngine;
using System.Collections;

public class MaterialSwitcher : MonoBehaviour
{
    public Material[] materials;
    private int currentMaterialIndex = 0;
    public float fadeTime = 1.0f;
    public GameObject objectToFade;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        // If the GameObject is active, start fading in.
        if(gameObject.activeInHierarchy)
        {
            objectRenderer.material.color = new Color(objectRenderer.material.color.r, objectRenderer.material.color.g, objectRenderer.material.color.b, 0);
            StartCoroutine(FadeInFromTransparent());
        }
    }

    void OnEnable()
    {
        // If the GameObject is enabled, start fading in.
        objectRenderer.material.color = new Color(objectRenderer.material.color.r, objectRenderer.material.color.g, objectRenderer.material.color.b, 0);
        StartCoroutine(FadeInFromTransparent());
    }

    public IEnumerator FadeInFromTransparent()
    {
        Color nextColor = materials[currentMaterialIndex].color;
        float t = 0.0f;

        // Fade in to the next color
        for (; t < fadeTime; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / fadeTime);
            objectRenderer.material.color = new Color(nextColor.r, nextColor.g, nextColor.b, alpha);
            yield return null;
        }
    }

    public void SwitchMaterial()
    {
        if (objectRenderer != null)
        {
            StartCoroutine(FadeMaterial());
        }
        else
        {
            Debug.LogError("No renderer found on this GameObject.");
        }
    }

public IEnumerator FadeMaterial()
{
    Color originalColor = objectRenderer.material.color;

    float t = 3.0f;

    for (; t < fadeTime; t += Time.deltaTime)
    {
        float alpha = Mathf.Lerp(1, 0, t / fadeTime);
        objectRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        yield return null;
    }

    // Switch the material to the next one in the array.
    currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;
    objectRenderer.material = materials[currentMaterialIndex];

    // Reset the color to full opacity before fading in.
    objectRenderer.material.color = new Color(objectRenderer.material.color.r, objectRenderer.material.color.g, objectRenderer.material.color.b, 1);

    // Fade in the new material.
    for (t = 3.0f; t < fadeTime; t += Time.deltaTime)
    {
        float alpha = Mathf.Lerp(0, 1, t / fadeTime);
        objectRenderer.material.color = new Color(objectRenderer.material.color.r, objectRenderer.material.color.g, objectRenderer.material.color.b, alpha);
        yield return null;
    }
}



    public void FadeOutAndDisable()
    {
        if (objectRenderer != null)
        {
            StartCoroutine(FadeOutToTransparentAndDisable());
        }
        else
        {
            Debug.LogError("No renderer found on this GameObject.");
        }
    }

    public IEnumerator FadeOutToTransparentAndDisable()
    {
        if (objectRenderer == null) yield break;
        
        Color originalColor = objectRenderer.material.color;

        for (float t = 5.0f; t < fadeTime; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeTime);
            objectRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        objectToFade.SetActive(false);
    }

    public void StartFadeOutToTransparentAndDisable()
    {
        StartCoroutine(FadeOutToTransparentAndDisable());
    }
}
