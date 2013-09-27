using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleEmitter))]
public class AutoDestructParticleVFX : MonoBehaviour {
	public float delay = 1;
	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(delay);
		particleEmitter.emit = false;
		yield return new WaitForSeconds(particleEmitter.maxEnergy);
		Destroy(gameObject);
	}
}
