using UnityEngine;

public class WinPointsForNailGiver : MonoBehaviour
{
    internal void processNailHitted(float inOldNailPassedHeight, float inNewNailPassedHeight, float inNailHeight, float inPointsGivingFactor) {
        if (inNewNailPassedHeight > inOldNailPassedHeight) {
            float theHeightDeltaRatio = (inNewNailPassedHeight - inOldNailPassedHeight) / inNailHeight;
            givePointsForHitPerHeightDelta(theHeightDeltaRatio, inPointsGivingFactor);

            float theHalfNailHeight = inNailHeight / 2;
            if (inOldNailPassedHeight < theHalfNailHeight && inNewNailPassedHeight >= theHalfNailHeight)
                givePointsForHalfHeightPassing(inPointsGivingFactor);

            if (inOldNailPassedHeight < inNailHeight && inNewNailPassedHeight >= inNailHeight)
                givePointsForHeightPassing(inPointsGivingFactor);
        }
    }

    internal void processNailFinalizing(float inNailPassedHeight, float inNailHeight, float inPointsGivingFactor) {
        if (0 == inNailPassedHeight) {
            givePenaltyForNotTouchedNail(inPointsGivingFactor);
        } else {
            float theHalfNailHeight = inNailHeight / 2;
            float theHeightDistanceFromHalfHeight = Mathf.Abs(inNailPassedHeight - theHalfNailHeight);
            float theHeightDistanceFromHalfHeightRatio = theHeightDistanceFromHalfHeight / theHalfNailHeight;

            if (inNailPassedHeight < theHalfNailHeight)
                givePenaltyForLessThenHalfHeight(theHeightDistanceFromHalfHeightRatio, inPointsGivingFactor);
        }
    }

    private void givePointsForHitPerHeightDelta(float inHeightDeltaRatio, float inPointsGivingFactor) {
        float theBonusToGive = _bonusForHitPerHeightDelta * inHeightDeltaRatio;
        winPointsManager.changePoints(theBonusToGive * inPointsGivingFactor);
    }

    private void givePointsForHalfHeightPassing(float inPointsGivingFactor) {
        winPointsManager.changePoints(_bonusForHalfHeightPassing * inPointsGivingFactor);
    }

    private void givePointsForHeightPassing(float inPointsGivingFactor) {
        winPointsManager.changePoints(_bonusForHeightPassing * inPointsGivingFactor);
    }

    private void givePenaltyForLessThenHalfHeight(float inPenaltyHeightRatioNotPassed, float inPointsGivingFactor) {
        float thePenaltyPointsToGive = inPenaltyHeightRatioNotPassed * _pointsPenaltryPerLessThenHalfHeight;
        winPointsManager.changePoints(-thePenaltyPointsToGive * inPointsGivingFactor);
    }

    private void givePenaltyForNotTouchedNail(float inPointsGivingFactor) {
        winPointsManager.changePoints(-_pointsPenaltryForNotTouchedNail * inPointsGivingFactor);
    }

    private WinPointsManager winPointsManager => WinPointsManager.instance;

    //Fields
    [SerializeField] private float _bonusForHitPerHeightDelta = 0f;
    [SerializeField] private float _bonusForHalfHeightPassing = 0f;
    [SerializeField] private float _bonusForHeightPassing = 0f;
    [SerializeField] private float _pointsPenaltryPerLessThenHalfHeight = 0f;
    [SerializeField] private float _pointsPenaltryForNotTouchedNail = 0f;
}
