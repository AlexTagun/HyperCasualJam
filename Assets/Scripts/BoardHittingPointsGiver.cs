using UnityEngine;

public class BoardHittingPointsGiver : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        Vector2 theHitPosition = collision.GetContact(0).point;

        bool theIsHittedByNailHitter = collision.collider.GetComponent<NailHitter>();
        if (theIsHittedByNailHitter)
            processHit(theHitPosition);
    }

    private void processHit(Vector2 inHitPosition) {
        winPointsManager.changePoints(-_pointsPernaltyForHit);

        spawnHitParticles(inHitPosition);
    }

    private void spawnHitParticles(Vector2 inHitPosition) {
        EffectsManager.spawnParticleSystem(_hitParticles, inHitPosition);
    }

    private WinPointsManager winPointsManager => WinPointsManager.instance;

    //Fields
    [SerializeField] float _pointsPernaltyForHit = 0f;

    [SerializeField] ParticleSystem _hitParticles = null;
}
