using UnityEngine;

public class GameCameraObject : MonoBehaviour
{
    private void FixedUpdate() {
        manager.setFocusZone(cameraBounds);
    }

    private Bounds cameraBounds{
        get {
            var theHeight = cameraComponent.orthographicSize;
            var theWidth = theHeight * Screen.width / Screen.height;
            return new Bounds(transform.position, new Vector3(theWidth, theHeight, 0f));
        }
    }

    private Camera cameraComponent => GetComponent<Camera>();
    private LazyMapManager manager => Object.FindObjectOfType<LazyMapManager>();
}
