using Assets.InputSystem;
using UnityEngine;

namespace Assets.WeaponSystem.Scripts
{
	public class Gun : MonoBehaviour
	{
		[SerializeField] Projectile _projectile = null;
		[SerializeField] Transform _projectileSpawnPoint = null;

		GameInputsManager _inputs;
		float _fireRate = 0.25f;
		float _nextFireTime = 0f;

		void Start()
		{
			_inputs = GameInputsManager.Instance;
		}

		void Update()
		{
			Fire();
		}

		Vector3 RaycastShoot()
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

		void Fire()
		{
			if (_inputs.Fire && Time.time >= _nextFireTime)
			{
				Shoot();
				_nextFireTime = Time.time + _fireRate;
			}
			else
			{
				_inputs.Fire = false;
			}
		}

		void Shoot()
		{
			if (RaycastShoot() != Vector3.zero)
			{
				var newProjectile = Instantiate(_projectile, _projectileSpawnPoint.position, Quaternion.identity);
				newProjectile.SetTarget(RaycastShoot());
			}
		}
	}
}

