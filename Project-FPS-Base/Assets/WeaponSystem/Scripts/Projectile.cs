using UnityEngine;

namespace Assets.WeaponSystem.Scripts
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _projectileParticle = null;
		private float _speed = 20f;
		private Vector3 _direction;

		private void Update()
		{
			transform.Translate(_direction * _speed * Time.deltaTime);
			Destroy(gameObject, 1f);
		}

		void OnCollisionEnter(Collision collision)
		{
			if (collision.transform.TryGetComponent(out IDamageable damageable))
			{
				damageable.TakeDamage(1);
				OnHit();
			}
			Destroy(gameObject);
		}

		public void SetTarget(Vector3 target)
		{
			_direction = (target - transform.position).normalized;
		}

		private void OnHit()
		{
			Instantiate(_projectileParticle, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
