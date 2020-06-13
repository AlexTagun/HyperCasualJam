using UnityEngine;

public class WinPointsUIObject : MonoBehaviour
{
    private void FixedUpdate() {
        updateBar();        
    }

    private void updateBar() {
        float thePartOfPoints = winPointsManager.points / winPointsManager.maxPoints;

        Vector2 theAnchorMax = _barRectTransform.anchorMax;
        theAnchorMax.x = thePartOfPoints;
        _barRectTransform.anchorMax = theAnchorMax;
    }

    private WinPointsManager winPointsManager => WinPointsManager.instance;

    //Fields
    [SerializeField] private RectTransform _barRectTransform = null;
}
