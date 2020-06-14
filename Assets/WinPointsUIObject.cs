using UnityEngine;
using DG.Tweening;

public class WinPointsUIObject : MonoBehaviour
{
    private void FixedUpdate() {
        updateBar();        
    }

    private void updateBar() {
        float thePartOfPoints = winPointsManager.points / winPointsManager.maxPoints;

        Vector2 theAnchorMax = _barRectTransform.anchorMax;
        theAnchorMax.x = thePartOfPoints;
        _barRectTransform.DOAnchorMax(theAnchorMax, 0.5f);//anchorMax = theAnchorMax;
    }

    private WinPointsManager winPointsManager => WinPointsManager.instance;

    //Fields
    [SerializeField] private RectTransform _barRectTransform = null;
}
