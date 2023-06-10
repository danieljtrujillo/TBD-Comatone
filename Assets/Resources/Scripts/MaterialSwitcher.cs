using UnityEngine;
using System.Collections;

public class MaterialSwitcher : MonoBehaviour
{
    public Material[] materials;
    private int currentMaterialIndex = 0;
    public float fadeTime = 1.0f;

    private Material blackMaterial;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        blackMaterial = new Material(Shader.Find("Standard"));
        blackMaterial.color = Color.black;
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

    IEnumerator FadeMaterial()
    {
        Color originalColor = objectRenderer.material.color;
        Color nextColor;

        float t = 0.0f;

        for (; t < fadeTime; t += Time.deltaTime)
        {
            objectRenderer.material.color = Color.Lerp(originalColor, blackMaterial.color, t / fadeTime);
            yield return null;
        }

        currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;
        nextColor = materials[currentMaterialIndex].color;

        for (t = 0.0f; t < fadeTime; t += Time.deltaTime)
        {
            objectRenderer.material.color = Color.Lerp(blackMaterial.color, nextColor, t / fadeTime);
            yield return null;
        }

        objectRenderer.material.color = nextColor;
    }

    // New method to fade out and then disable the GameObject.
    public void FadeOutAndDisable()
    {
        if (objectRenderer != null)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            Debug.LogError("No renderer found on this GameObject.");
        }
    }

    IEnumerator FadeOut()
    {
        // The color of the current material.
        Color originalColor = objectRenderer.material.color;

        float t = 0.0f;

        // Fade the current material to black.
        for (; t < fadeTime; t += Time.deltaTime)
        {
            objectRenderer.material.color = Color.Lerp(originalColor, blackMaterial.color, t / fadeTime);
            yield return null;
        }

        // Disable the GameObject after the fade.
        gameObject.SetActive(false);
    }
}
