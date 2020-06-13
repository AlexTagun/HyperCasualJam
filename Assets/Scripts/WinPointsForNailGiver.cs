using UnityEngine;

public class WinPointsForNailGiver : MonoBehaviour
{
    internal void processNailHitted(float inOldNailPassedHeight, float inNewNailPassedHeight, float inNailHeight) {
        if (inNewNailPassedHeight > inOldNailPassedHeight) {
            float theHeightDeltaRatio = (inNewNailPassedHeight - inOldNailPassedHeight) / inNailHeight;
            givePointsForHitPerHeightDelta(theHeightDeltaRatio);

            float theHalfNailHeight = inNailHeight / 2;
            if (inOldNailPassedHeight < theHalfNailHeight && inNewNailPassedHeight >= theHalfNailHeight)
                givePointsForHalfHeightPassing();

            if (inOldNailPassedHeight < inNailHeight && inNewNailPassedHeight >= inNailHeight)
                givePointsForHeightPassing();
        }
    }

    internal void processNailFinalizing(float inNailPassedHeight, float inNailHeight) {
        if (0 == inNailPassedHeight) {
            givePenaltyForNotTouchedNail();
        } else {
            float theHalfNailHeight = inNailHeight / 2;
            float theHeightDistanceFromHalfHeight = Mathf.Abs(inNailPassedHeight - theHalfNailHeight);
            float theHeightDistanceFromHalfHeightRatio = theHeightDistanceFromHalfHeight / theHalfNailHeight;

            if (inNailPassedHeight > theHalfNailHeight)
                givePointsPerMoreThenHalfHeightPassing(theHeightDistanceFromHalfHeightRatio);
            else
                givePenaltyForLessThenHalfHeight(theHeightDistanceFromHalfHeightRatio);
        }
    }

    private void givePointsForHitPerHeightDelta(float inHeightDeltaRatio) {
        float theBonusToGive = _bonusForHitPerHeightDelta * inHeightDeltaRatio;
        winPointsManager.changePoints(theBonusToGive);
    }

    private void givePointsForHalfHeightPassing() {
        winPointsManager.changePoints(_bonusForHalfHeightPassing);
    }

    private void givePointsForHeightPassing() {
        winPointsManager.changePoints(_bonusForHeightPassing);
    }

    private void givePointsPerMoreThenHalfHeightPassing(float inBonusHeightRatioPassed) {
        float theBounsPointsToGive = inBonusHeightRatioPassed * _pointsBonusPerMoreThenHalfHeight;
        winPointsManager.changePoints(theBounsPointsToGive);
    }

    private void givePenaltyForLessThenHalfHeight(float inPenaltyHeightRatioNotPassed) {
        float thePenaltyPointsToGive = inPenaltyHeightRatioNotPassed * _pointsPenaltryPerLessThenHalfHeight;
        winPointsManager.changePoints(-thePenaltyPointsToGive);
    }

    private void givePenaltyForNotTouchedNail() {
        winPointsManager.changePoints(-_pointsPenaltryForNotTouchedNail);
    }

    private WinPointsManager winPointsManager => WinPointsManager.instance;

    //Fields
    [SerializeField] private float _bonusForHitPerHeightDelta = 0f;
    [SerializeField] private float _bonusForHalfHeightPassing = 0f;
    [SerializeField] private float _bonusForHeightPassing = 0f;
    [SerializeField] private float _pointsBonusPerMoreThenHalfHeight = 0f;
    [SerializeField] private float _pointsPenaltryPerLessThenHalfHeight = 0f;
    [SerializeField] private float _pointsPenaltryForNotTouchedNail = 0f;
}
