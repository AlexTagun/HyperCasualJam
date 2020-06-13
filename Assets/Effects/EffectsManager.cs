using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static ParticleSystem spawnParticleSystem(ParticleSystem inParticleSystem, Vector2 inPosition) {
        ParticleSystem theNewSystem = Instantiate(inParticleSystem);
        theNewSystem.gameObject.AddComponent<ParticleSystemAutodestroyer>();
        theNewSystem.transform.position = new Vector3(inPosition.x, inPosition.y, theNewSystem.transform.position.z);
        return theNewSystem;
    }
}
