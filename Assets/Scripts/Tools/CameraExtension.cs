using UnityEngine;

public static class CameraExtension
{
    public static bool IsPositionVisible(this Camera camera, Vector3 position)
    {
        Vector3 viewportPosition = camera.WorldToViewportPoint(position);
        return viewportPosition.x >= 0f && viewportPosition.x <= 1f && viewportPosition.y >= 0f && viewportPosition.y <= 1f && viewportPosition.z >= 0f;
    }
}
