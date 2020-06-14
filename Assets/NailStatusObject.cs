using UnityEngine;

public class NailStatusObject : MonoBehaviour
{
    public void setNailState(float inPassedHeight, float inHeightToPass) {
        setNailLeftHeight(inPassedHeight, inHeightToPass);
    }

    private void setNailLeftHeight(float inPassedHeight, float inHeightToPass) {
        //Debug.Log(inPassedHeight + " | " + inHeightToPass);

        float theLeftHeightToPass = (inHeightToPass - inPassedHeight);

        Vector3 theLocalPosition = _nailLeftHeightMask.transform.localPosition;
        theLocalPosition.y = -theLeftHeightToPass/2;
        _nailLeftHeightMask.transform.localPosition = theLocalPosition;

        Vector3 theLocalScale = _nailLeftHeightMask.transform.localScale;
        theLocalScale.y = theLeftHeightToPass;
        _nailLeftHeightMask.transform.localScale = theLocalScale;
    }

    private void FixedUpdate() {
        Vector2 theMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 thePosition = transform.position;
        float theDistanceToMouse = (thePosition - theMousePosition).magnitude;
        Debug.Log(theDistanceToMouse);

        float theAlphaFactor = 0f;
        if (theDistanceToMouse < _fullAlphaDistane)
            theAlphaFactor = 1f;
        else if (theDistanceToMouse < _zeroAlphaDistane)
            theAlphaFactor = (_zeroAlphaDistane - theDistanceToMouse) / (_zeroAlphaDistane - _fullAlphaDistane);

        Color theColor = _nailLeftHeightMask.color;
        theColor.a = _fullAlpha * theAlphaFactor;
        _nailLeftHeightMask.color = theColor;
    }

    //Fields
    [SerializeField] SpriteRenderer _nailLeftHeightMask = null;
    [SerializeField] float _fullAlpha = 0.7f;
    [SerializeField] float _fullAlphaDistane = 3f;
    [SerializeField] float _zeroAlphaDistane = 10f;
}
