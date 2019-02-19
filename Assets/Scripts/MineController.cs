using UnityEngine;

namespace Assets.Scripts
{
	public class MineController : MonoBehaviour {

		public GameObject explosion;

		private void OnTriggerEnter2D(Collider2D other){
			if (!other.CompareTag("Player")) return;
			Destroy (gameObject);
			var o = gameObject;
			Instantiate (explosion, o.transform.position, o.transform.rotation);
		}
	}
}
