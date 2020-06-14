using UnityEngine;
using DG.Tweening;

public class NailStatusObject : MonoBehaviour
{
    public void setNailState(float inPassedHeight, float inHeightToPass) {
        setNailLeftHeight(inPassedHeight, inHeightToPass);
        setNailFinishedStatus(inPassedHeight, inHeightToPass);
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

    private void setNailFinishedStatus(float inPassedHeight, float inHeightToPass) {
        if (0f != inPassedHeight && inPassedHeight >= inHeightToPass)
            showMailFinishedStatusAnimation();
    }

    private void showMailFinishedStatusAnimation() {
        Transform theImageTransform = _nailFinishedStatusImage.transform;

        float theOriginalScale = theImageTransform.localScale.x;
        theImageTransform.DOScale(theOriginalScale * 2f, 0f);
        theImageTransform.DOScale(theOriginalScale * 1f, 1f);

        Color theFinalColor = _nailFinishedStatusImage.color;
        theFinalColor.a = 0.7f;
        _nailFinishedStatusImage.DOColor(theFinalColor, 1f);
    }

    private void Awake() {
        initNailFinishedStatusInvisibility();
    }

    private void initNailFinishedStatusInvisibility() {
        Color theColor = _nailFinishedStatusImage.color;
        theColor.a = 0f;
        _nailFinishedStatusImage.color = theColor;
    }

    private void FixedUpdate() {
        updateLeftHeightMaskAlpha();
    }

    private void updateLeftHeightMaskAlpha() {
        Vector2 theMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 thePosition = transform.position;
        float theDistanceToMouse = (thePosition - theMousePosition).magnitude;

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
    [SerializeField] SpriteRenderer _nailFinishedStatusImage = null;
    [SerializeField] float _fullAlpha = 0.7f;
    [SerializeField] float _fullAlphaDistane = 3f;
    [SerializeField] float _zeroAlphaDistane = 10f;
}
