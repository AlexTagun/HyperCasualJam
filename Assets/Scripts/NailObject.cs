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
            float theDistanceToPassClamped = Mathf.Clamp(theDistanceToPass, 0f, maxHeightToPassPerHit);

            float theOldPassedHeight = _passedHeight;
            transform.position += transform.TransformDirection(Vector2.down) * theDistanceToPassClamped;
            _passedHeight += theDistanceToPassClamped;

            spawnHitEffect(theOldPassedHeight, _passedHeight, heightToPass, inHitRelativePosition);
            SetSoundHitVolumeAndPlay(theDistanceToPassClamped);

            if (null != winPointsGiver)
                winPointsGiver.processNailHitted(theOldPassedHeight, _passedHeight, heightToPass, winPointsBasedOnStartingPassingFactor);
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

    void SetSoundHitVolumeAndPlay(float theDistanceToPassClamped)
    {
        float newVolume = theDistanceToPassClamped / heightToPass;
        if (_audioSource.isPlaying && _audioSource.volume > newVolume) { }
        else
        {
            _audioSource.volume = newVolume / 2; // sound too loud , when volume = 1
            _audioSource.Play();
        }
    }
    private void spawnHitEffect(float inOldNailPassedHeight, float inNewNailPassedHeight, float inHeightToPass, Vector2 inHitRelativePosition) {
        float theHalfHeightToPass = inHeightToPass / 2;

        ParticleSystem theParticleSystemToSpawn = null;

        //Default only if speed is enough
        float theRatioHeightPassedByHit = (inNewNailPassedHeight - inOldNailPassedHeight) / inHeightToPass;
        if (theRatioHeightPassedByHit > _ratioHeightPassedByHitLimitToSpawnEffects)
            theParticleSystemToSpawn = _hitEffectParticleSystem;

        if (inOldNailPassedHeight < theHalfHeightToPass && inNewNailPassedHeight >= theHalfHeightToPass)
            theParticleSystemToSpawn = _halfPassedHitEffectParticleSystem;
        else if (inOldNailPassedHeight < inHeightToPass && inNewNailPassedHeight >= inHeightToPass)
            theParticleSystemToSpawn = _fullPassedHitEffectParticleSystem;

        if (theParticleSystemToSpawn)
            EffectsManager.spawnParticleSystem(theParticleSystemToSpawn, transform.TransformPoint(inHitRelativePosition));
    }

    private void Start() {
        computeFullHeightToPass();
        computeWinPointsBasedOnStartingPassingFactor();
    }

    void computeFullHeightToPass() {
        float theRotation = transform.rotation.eulerAngles.z;
        float theCos = Mathf.Cos(theRotation * Mathf.Deg2Rad);
        float theYDistanceToPass = Mathf.Clamp(transform.position.y - _groundY, 0f, height);
        _heightToPass = Mathf.Clamp(theYDistanceToPass / theCos, 0f, height);
    }

    void computeWinPointsBasedOnStartingPassingFactor() {
        _winPointsBasedOnStartingPassingFactor = _heightToPass / height;
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
            winPointsGiver.processNailFinalizing(_passedHeight, heightToPass, winPointsBasedOnStartingPassingFactor);
    }

    private float height => _bodyCollider.size.y;
    private float heightToPass => _heightToPass;
    private float maxHeightToPassPerHit => heightToPass - _passedHeight;
    private WinPointsForNailGiver winPointsGiver => GetComponent<WinPointsForNailGiver>();
    private float winPointsBasedOnStartingPassingFactor => _winPointsBasedOnStartingPassingFactor;

    //Fields
    [SerializeField] private AnimationCurve _movePerHitImpulseValueFactorCurve = null;
    [SerializeField] private float _groundY = -3.5f;

    [SerializeField] private Collider2D _hitCollider = null;
    [SerializeField] private BoxCollider2D _bodyCollider = null;
    [SerializeField] private ParticleSystem _hitEffectParticleSystem = null;
    [SerializeField] private ParticleSystem _halfPassedHitEffectParticleSystem = null;
    [SerializeField] private ParticleSystem _fullPassedHitEffectParticleSystem = null;
    [SerializeField] private float _ratioHeightPassedByHitLimitToSpawnEffects = 0f;
    [SerializeField] private AudioSource _audioSource = null;

    private float _heightToPass = 0f;
    private float _winPointsBasedOnStartingPassingFactor = 0f;
    private float _passedHeight = 0f;
}
