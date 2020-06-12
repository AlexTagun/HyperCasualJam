using UnityEngine;

public class NailObject : MonoBehaviour
{
    private void processHit(Vector2 inHitRelativePosition, Vector2 inHitRelativeVelocity) {
        if (inHitRelativeVelocity.magnitude < _minimumHitImpulseValue) {
            Vector2 theVelocityProjected = Vector3.Project(inHitRelativeVelocity, Vector2.up);
            float theHitImpulseValue = theVelocityProjected.magnitude;
            float theMoveForHitDistance = theHitImpulseValue * _movePerHitImpulseValueFactor;
            transform.position += transform.TransformDirection(Vector2.down) * theMoveForHitDistance;
        }

        //Debug.DrawLine(
        //    transform.TransformPoint(inHitRelativePosition),
        //    transform.TransformPoint(inHitRelativePosition + inHitRelativeVelocity),
        //    Color.red, 10f, false);
    }

    #region CheatHitProcessing
    private void OnCollisionEnter2D(Collision2D collision) {
        if (_isHitterInZone)
            processHit(transform.InverseTransformPoint(collision.GetContact(0).point), collision.relativeVelocity);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<NailHitter>())
            _isHitterInZone = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<NailHitter>())
            _isHitterInZone = false;
    }
    #endregion

    //Fields
    [SerializeField] private float _minimumHitImpulseValue = 4f;
    [SerializeField] private float _movePerHitImpulseValueFactor = 0.1f;

    private bool _isHitterInZone = false;
}
