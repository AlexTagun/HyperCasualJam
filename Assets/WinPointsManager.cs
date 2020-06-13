using UnityEngine;

public class WinPointsManager : MonoBehaviour
{
    public float points => _points;
    public float maxPoints => _maxPoints;

    public void changePoints(float inDelta) {
        setPoints(_points + inDelta);
    }

    public void setPoints(float inValue) {
        _points = Mathf.Clamp(inValue, 0f, _maxPoints);
    }

    public static WinPointsManager instance => FindObjectOfType<WinPointsManager>();

    private void Awake() {
        setPoints(_startPoints);
    }

    //Fields
    [SerializeField] private float _startPoints = 50f;
    [SerializeField] private float _maxPoints = 100f;

    private float _points = 0f;
}
