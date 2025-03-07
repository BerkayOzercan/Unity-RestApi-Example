using Assets.PlayerSystem.Scripts;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class Collectable : MonoBehaviour
    {
        public CollectableTypes CollectableTypes = CollectableTypes.None;

        private LevelManager _levelManager = null;
        private bool _isCollect = false;

        private void Start()
        {
            _levelManager = LevelManager.Instance;
            Explode();
        }

        private void Update()
        {
            transform.GetChild(0).transform.Rotate(Vector3.forward * Time.deltaTime * 50f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Collect();
            }
        }

        public void Collect()
        {
            if (_isCollect) return;
            if (CollectableTypes == CollectableTypes.Coin)
            {
                _isCollect = true;
                _levelManager.AddCurrency(1);
                Destroy(gameObject);
            }
        }

        private void Explode()
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(250f, transform.position, 2.5f);
            }
        }
    }

    public enum CollectableTypes
    {
        None,
        Coin
    }
}


