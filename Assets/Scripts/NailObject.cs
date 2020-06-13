using UnityEngine;

public class NailObject : MonoBehaviour
{
    private void processHit(Vector2 inHitRelativePosition, Vector2 inHitRelativeVelocity) {
        Vector2 theVelocityProjected = Vector3.Project(inHitRelativeVelocity, Vector2.up);
        float theProjectedHitImpulseValue = theVelocityProjected.magnitude;
        float theActialHitImpulse = _movePerHitImpulseValueFactorCurve.Evaluate(theProjectedHitImpulseValue);

        transform.position += transform.TransformDirection(Vector2.down) * theActialHitImpulse;

        //Debug.DrawLine(
        //    transform.TransformPoint(inHitRelativePosition),
        //    transform.TransformPoint(inHitRelativePosition + inHitRelativeVelocity),
        //    Color.red, 10f, false);
    }

    #region CheatHitProcessing
    private void OnCollisionEnter2D(Collision2D collision) {
        bool theIsHitColliderHitter = (collision.otherCollider == _hitCollider);
        bool theIsHittedByNailHitter = collision.collider.GetComponent<NailHitter>();

        //Debug.Log(">>> " + collision.collider.name + "  | " + collision.otherCollider.name);

        if (theIsHitColliderHitter && theIsHittedByNailHitter)
            processHit(transform.InverseTransformPoint(collision.GetContact(0).point), collision.relativeVelocity);
    }
    #endregion

    //Fields
    [SerializeField] private Collider2D _hitCollider = null;

    [SerializeField] private AnimationCurve _movePerHitImpulseValueFactorCurve = null;
}
