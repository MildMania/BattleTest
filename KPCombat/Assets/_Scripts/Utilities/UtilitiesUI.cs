using UnityEngine;

public partial class Utilities
{
    public static Vector2 WorldPosToCanvasPos(Vector3 worldPos, Camera targetCamera, Canvas targetCanvas)
    {
        var viewportPos = targetCamera.WorldToViewportPoint(worldPos);
        return new Vector2(((RectTransform)targetCanvas.transform).sizeDelta.x * viewportPos.x, ((RectTransform)targetCanvas.transform).sizeDelta.y * viewportPos.y);
    }

    public static Vector3 CanvasPosToWorldPos(Vector2 canvasPos, Camera targetCamera, Canvas targetCanvas)
    {
        var viewportPos = new Vector2(canvasPos.x / ((RectTransform)targetCanvas.transform).sizeDelta.x, canvasPos.y / ((RectTransform)targetCanvas.transform).sizeDelta.y);

        return targetCamera.ViewportToWorldPoint(viewportPos);
    }
}
