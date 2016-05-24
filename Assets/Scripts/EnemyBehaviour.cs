using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 150;
	public GameObject projectile;
	public float projectileSpeed;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;

	public AudioClip fireSound;
	public AudioClip deathSound;

	private ScoreKeeper scoreKeeper;

	void Start() {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile) {
			health-= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				AudioSource.PlayClipAtPoint(deathSound, transform.position);
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}

		}
	}

	void Fire() {
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
	}

	void Update () {
		float probability = Time.deltaTime * shotsPerSeconds;
		if (Random.value < probability) {
			Fire();
		}

	}
}
