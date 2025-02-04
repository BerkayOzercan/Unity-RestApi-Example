using Assets.InputSystem;
using UnityEngine;

namespace Assets.WeaponSystem.Scripts
{
	public class Gun : MonoBehaviour
	{
		[SerializeField] private Projectile _projectile = null;
		[SerializeField] private Transform _projectileSpawnPoint = null;
		[SerializeField] private GameInputsManager _inputs = null;

		private float _fireRate = 0.25f;
		private float _nextFireTime = 0f;

		private void Update()
		{
			Fire();
		}

		private Vector3 RaycastShoot()
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
			RaycastHit hit;
			Physics.Raycast(ray, out hit);

			if (hit.point == Vector3.zero)
			{
				Vector3 limitedPosition = ray.origin + ray.direction * 20f;
				return limitedPosition;
			}

			return hit.point;
		}

		private void Fire()
		{
			if (_inputs.Fire && Time.time >= _nextFireTime) // Left Mouse Button
			{
				Shoot();
				_nextFireTime = Time.time + _fireRate;
			}
			else
			{
				_inputs.Fire = false;
			}
		}

		private void Shoot()
		{
			if (RaycastShoot() != Vector3.zero)
			{
				var newProjectile = Instantiate(_projectile, _projectileSpawnPoint.position, Quaternion.identity);
				newProjectile.SetTarget(RaycastShoot());
			}
		}
	}
}

