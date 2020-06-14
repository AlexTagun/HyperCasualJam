using System.Collections.Generic;
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
            SpawnSoundPlayerHit(theDistanceToPassClamped, _sounds[0], transform.TransformPoint(inHitRelativePosition));
            updateStatusObject(_passedHeight, _heightToPass);

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

    private void SpawnSoundPlayerHit(float theDistanceToPassClamped, Sound sound, Vector2 positionSpawn)
    {
        AudioSource soundPlayerAudioSourse = SoundsManager.spawnSoundPlayer(_soundPlayer, sound, positionSpawn);
        float newVolume = theDistanceToPassClamped / heightToPass * sound._volume;
        soundPlayerAudioSourse.volume = newVolume;
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
        {
            theParticleSystemToSpawn = _fullPassedHitEffectParticleSystem;
            SoundsManager.spawnSoundPlayer(_soundPlayer, _sounds[1], transform.TransformPoint(inHitRelativePosition));
        }

        if (theParticleSystemToSpawn)
            EffectsManager.spawnParticleSystem(theParticleSystemToSpawn, transform.TransformPoint(inHitRelativePosition));

        //SpawnSoundPlayer(_sounds[0], transform.TransformPoint(inHitRelativePosition));
    }

    private void updateStatusObject(float inPassedHeight, float inHeightToPass) {
        _nailStatusObject.setNailState(inPassedHeight, inHeightToPass);
    }

    private void Start() {
        computeFullHeightToPass();
        computeWinPointsBasedOnStartingPassingFactor();
        initialUpdateNailStatusObject();
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

    void initialUpdateNailStatusObject() {
        updateStatusObject(_passedHeight, _heightToPass);
    }

    public void finalize() {
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
    [SerializeField] private NailStatusObject _nailStatusObject = null;

    [Header("Sound")]
    [SerializeField] private List<Sound> _sounds = new List<Sound>();
    [SerializeField] private GameObject _soundPlayer;

    private float _heightToPass = 0f;
    private float _winPointsBasedOnStartingPassingFactor = 0f;
    private float _passedHeight = 0f;
}
