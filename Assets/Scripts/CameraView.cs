using UnityEngine;

public static class CameraView
{
	public static void AdjustCameraPosition(int width, int height)
	{
		Camera cam = Camera.main;
		cam.orthographicSize = height / 2.0f;

		cam.transform.position = new Vector3((width / 2.0f) - 0.5f, (height / 2.0f) - 0.5f, -10);
	}
}