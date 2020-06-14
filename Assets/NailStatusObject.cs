using UnityEngine;
using DG.Tweening;

public class NailStatusObject : MonoBehaviour
{
    public void setNailState(float inPassedHeight, float inHeightToPass) {
        setNailLeftHeight(inPassedHeight, inHeightToPass);
        setNailFinishedStatus(inPassedHeight, inHeightToPass);
        setNailShouldBeFinishedWarningState(inPassedHeight, inHeightToPass);
    }

    public void processNailPassedHalfOfScreen(float inPassedHeight, float inHeightToPass) {
        if (inPassedHeight < inHeightToPass / 2)
            showNailShouldBeHittedWarning();
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
            showNailFinishedStatusAnimation();
    }

    void setNailShouldBeFinishedWarningState(float inPassedHeight, float inHeightToPass) {
        if (_isNailShouldBeHittedWarningShown)
            if (inPassedHeight >= inHeightToPass / 2)
                hideNailShouldBeHittedWarning();
    }

    private void showNailFinishedStatusAnimation() {
        Transform theImageTransform = _nailFinishedStatusImage.transform;

        theImageTransform.rotation = Quaternion.identity;
        float theOriginalScale = theImageTransform.localScale.x;
        theImageTransform.DOScale(theOriginalScale * 2f, 0f);
        theImageTransform.DOScale(theOriginalScale * 1f, 1f);

        Color theFinalColor = _nailFinishedStatusImage.color;
        theFinalColor.a = 0.7f;
        _nailFinishedStatusImage.DOColor(theFinalColor, 1f);
    }

    private void showNailShouldBeHittedWarning() {
        Transform theImageTransform = _nailShouldBeHittedWarning.transform;

        theImageTransform.transform.rotation = Quaternion.identity;
        float theOriginalScale = theImageTransform.localScale.x;
        theImageTransform.DOScale(theOriginalScale * 3f, 0f);
        theImageTransform.DOScale(theOriginalScale * 1f, 1f);

        Color theFinalColor = _nailShouldBeHittedWarning.color;
        theFinalColor.a = 0.7f;
        _nailShouldBeHittedWarning.DOColor(theFinalColor, 1f);

        _isNailShouldBeHittedWarningShown = true;
    }

    private void hideNailShouldBeHittedWarning() {
        Color theFinalColor = _nailShouldBeHittedWarning.color;
        theFinalColor.a = 0f;
        _nailShouldBeHittedWarning.DOColor(theFinalColor, 1f);
    }

    private void Awake() {
        initNailFinishedStatusInvisibility();
        initNailShouldBeHittedWarningInvisibility();
    }

    private void initNailFinishedStatusInvisibility() {
        Color theColor = _nailFinishedStatusImage.color;
        theColor.a = 0f;
        _nailFinishedStatusImage.color = theColor;
    }

    private void initNailShouldBeHittedWarningInvisibility() {
        Color theColor = _nailShouldBeHittedWarning.color;
        theColor.a = 0f;
        _nailShouldBeHittedWarning.color = theColor;
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
    [SerializeField] SpriteRenderer _nailShouldBeHittedWarning = null;
    [SerializeField] float _fullAlpha = 0.7f;
    [SerializeField] float _fullAlphaDistane = 3f;
    [SerializeField] float _zeroAlphaDistane = 10f;

    bool _isNailShouldBeHittedWarningShown = false;
}
