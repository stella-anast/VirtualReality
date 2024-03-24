using UnityEngine;

public class UIPositionController : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform[] panels;

    // Adjust these values according to your desired position
    public float offsetX = 0.8f;
    public float offsetY = 0.8f;

    void Update()
    {
        // Get the camera's position in world space
        Vector3 cameraPosition = Camera.main.transform.position;

        // Calculate positions for each panel
        Vector3[] panelPositions = new Vector3[panels.Length];
        for (int i = 0; i < panels.Length; i++)
        {
            panelPositions[i] = new Vector3(cameraPosition.x + offsetX, cameraPosition.y + offsetY, cameraPosition.z);
        }

        // Set positions for each panel
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].position = panelPositions[i];
        }
    }
}
