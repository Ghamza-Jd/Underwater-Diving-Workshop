using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {

	private PlayerController _thePlayer;
	public GameObject death;

	public float speed = 0.3f;

	private Rigidbody2D _myRigidbody;


	public Transform[] moveSpots;
	public Transform[] hideouts;
	public float startWaitTime;
	public float distance;

	private int _randomSpot;
	private float _waitTime;
	private bool _running;


	// Use this for initialization
	private void Start () {
		_thePlayer = FindObjectOfType<PlayerController> ();
		_myRigidbody = GetComponent<Rigidbody2D> ();

		Physics2D.queriesStartInColliders = false;
		_running = false;
		_waitTime = startWaitTime;
		_randomSpot = Random.Range(0, moveSpots.Length);
		if (transform.position.x > moveSpots[_randomSpot].position.x)
		{
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		}
		else if (transform.position.x < moveSpots[_randomSpot].position.x)
		{
			transform.localScale = new Vector3 (1f, 1f, 1f);
		}
	}

	// Update is called once per frame
	private void Update ()
	{
		var hitInfo = Physics2D.Raycast(transform.position, transform.right * transform.localScale.x, distance);
		
		if (hitInfo.collider != null)
			if (hitInfo.collider.CompareTag("Player"))
				_running = true;
		
		if (_running) Run();
		else Move();
	}

	private void Move()
	{
		transform.position =
			Vector2.MoveTowards(transform.position, moveSpots[_randomSpot].position, speed * Time.deltaTime);

		if (!(Vector2.Distance(transform.position, moveSpots[_randomSpot].position) < .2f)) return;
		if (_waitTime <= 0)
		{
			_randomSpot = Random.Range(0, moveSpots.Length);
			if (transform.position.x > moveSpots[_randomSpot].position.x)
			{
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			}
			else if (transform.position.x < moveSpots[_randomSpot].position.x)
			{
				transform.localScale = new Vector3 (1f, 1f, 1f);
			}
			_waitTime = startWaitTime;
		}
		else
		{
			_waitTime -= Time.deltaTime;
		}
	}

	private void Run()
	{
		if (Vector2.Distance(transform.position, hideouts[0].position) < .2f ||
		    Vector2.Distance(transform.position, hideouts[1].position) < .2f)
		{
			gameObject.SetActive(false);
		}
		if (Vector2.Distance(transform.position, hideouts[0].position) <
		    Vector2.Distance(transform.position, hideouts[1].position))
		{
			transform.localScale = new Vector3 (-1f, 1f, 1f);
			transform.position =
				Vector2.MoveTowards(transform.position, hideouts[0].position, speed * 4 * Time.deltaTime);
		}
		else
		{
			transform.localScale = new Vector3 (1f, 1f, 1f);
			transform.position =
				Vector2.MoveTowards(transform.position, hideouts[1].position, speed * 4 * Time.deltaTime);
		}
	}


	private void OnTriggerEnter2D(Collider2D other){
		if (!other.CompareTag("Player") || !_thePlayer.rushing) return;
		var o = gameObject;
		Instantiate (death, o.transform.position, o.transform.rotation);
		Destroy (gameObject);
	}
}
