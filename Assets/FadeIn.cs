using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float duration = 2.0f;   // Duration of the fade-in effect
    private Image blackPanel;       // The black panel

    void Start()
    {
        // Create a new Canvas
        GameObject canvasObject = new GameObject("FadeCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        // Create a new black panel
        GameObject panel = new GameObject("BlackPanel");
        panel.transform.SetParent(canvasObject.transform, false);
        panel.AddComponent<CanvasRenderer>();

        // Set the panel's size to fill the screen
        blackPanel = panel.AddComponent<Image>();
        blackPanel.color = Color.black;
        RectTransform rt = panel.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // Start the fade-in process
        StartCoroutine(FadeInRoutine());
    }

    IEnumerator FadeInRoutine()
    {

        yield return new WaitForSeconds(2f);
        float time = 0;
        Color startColor = blackPanel.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1, 0, time / duration);
            blackPanel.color = new Color(startColor.r, startColor.g, startColor.b, alphaValue);
            yield return null;
        }

        // Ensure the panel is fully transparent
        blackPanel.color = new Color(startColor.r, startColor.g, startColor.b, 0);
        Destroy(blackPanel.gameObject); // Remove the black panel after fade-in
    }
}
