using UnityEngine;

public class ParticleSystemAutodestroyer : MonoBehaviour
{
    private void Awake() {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if (!_particleSystem.IsAlive())
            Destroy(gameObject);
    }

    private ParticleSystem _particleSystem = null;
}
