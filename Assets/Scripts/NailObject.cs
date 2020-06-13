using UnityEngine;

public class NailObject : MonoBehaviour
{
    private void processHit(Vector2 inHitRelativePosition, Vector2 inHitRelativeVelocity) {
        Vector2 theVelocityProjected = Vector3.Project(inHitRelativeVelocity, Vector2.up);
        bool theVelocityIsDownOriented = (theVelocityProjected.y < 0);
        if (theVelocityIsDownOriented) {
            float theProjectedHitImpulseValue = theVelocityProjected.magnitude;
            float theActialHitImpulse = _movePerHitImpulseValueFactorCurve.Evaluate(theProjectedHitImpulseValue);
            float theDistanceToPass = theActialHitImpulse;
            float theDistanceToPassClamped = Mathf.Clamp(theDistanceToPass, 0f, maxDistanceToPassPerHit);

            float theOldPassedHeight = _passedHeight;
            transform.position += transform.TransformDirection(Vector2.down) * theDistanceToPassClamped;
            _passedHeight += theDistanceToPassClamped;

            float theRatioHeightPassedByHit = theDistanceToPassClamped / height;
            if (theRatioHeightPassedByHit > _ratioHeightPassedByHitLimitToSpawnEffects)
                spawnHitEffect(theOldPassedHeight, _passedHeight, height, inHitRelativePosition);

            if (null != winPointsGiver)
                winPointsGiver.processNailHitted(theOldPassedHeight, _passedHeight, height);
        }
    }

    #region CheatHitProcessing
    private void OnCollisionEnter2D(Collision2D collision) {
        bool theIsHitColliderHitter = (collision.otherCollider == _hitCollider);
        bool theIsHittedByNailHitter = collision.collider.GetComponent<NailHitter>();
        if (theIsHitColliderHitter && theIsHittedByNailHitter)
            processHit(transform.InverseTransformPoint(collision.GetContact(0).point), collision.relativeVelocity);
    }
    #endregion

    void spawnHitEffect(float inOldNailPassedHeight, float inNewNailPassedHeight, float inNailHeight, Vector2 inHitRelativePosition) {
        float theHalfHeight = inNailHeight / 2;

        ParticleSystem theParticleSystemToSpawn = _hitEffectParticleSystem;
        if (inOldNailPassedHeight < theHalfHeight && inNewNailPassedHeight >= theHalfHeight)
            theParticleSystemToSpawn = _halfPassedHitEffectParticleSystem;
        else if (inOldNailPassedHeight < inNailHeight && inNewNailPassedHeight >= inNailHeight)
            theParticleSystemToSpawn = _fullPassedHitEffectParticleSystem;

        EffectsManager.spawnParticleSystem(theParticleSystemToSpawn, transform.TransformPoint(inHitRelativePosition));
    }

    private void FixedUpdate() {
        updateFinalizingLogic();
    }

    private void updateFinalizingLogic() {
        Vector3 theViewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (theViewportPosition.x < 0f)
            finalize();
    }

    private void finalize() {
        if (null != winPointsGiver)
            winPointsGiver.processNailFinalizing(_passedHeight, height);
    }

    private float height => _bodyCollider.size.y;
    private float maxDistanceToPassPerHit => (_fixPassingDistanceByHeight ? (height - _passedHeight) : float.MaxValue);
    private WinPointsForNailGiver winPointsGiver => GetComponent<WinPointsForNailGiver>();

    //Fields
    [SerializeField] private AnimationCurve _movePerHitImpulseValueFactorCurve = null;
    [SerializeField] private bool _fixPassingDistanceByHeight = true;

    [SerializeField] private Collider2D _hitCollider = null;
    [SerializeField] private BoxCollider2D _bodyCollider = null;
    [SerializeField] private ParticleSystem _hitEffectParticleSystem = null;
    [SerializeField] private ParticleSystem _halfPassedHitEffectParticleSystem = null;
    [SerializeField] private ParticleSystem _fullPassedHitEffectParticleSystem = null;
    [SerializeField] private float _ratioHeightPassedByHitLimitToSpawnEffects = 0f;

    private float _passedHeight = 0f;

}
