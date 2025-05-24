using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	private Rigidbody playerRb;
	
	[SerializeField]
	private float jumpForce;
	
	[SerializeField]
	private float gravityModifier;
	
	[SerializeField]
	private bool isOnGround = true;
	
	[SerializeField]
	private Animator playerAnim;
	
	[SerializeField]
	private AudioSource playerAudio;
	
	public AudioClip jumpSound;
	public AudioClip deathSound;

	[SerializeField] private ParticleSystem crashParticle;
	[SerializeField] private ParticleSystem dirtParticle;
	
	private CoinManager _coinManager;
	
	//Roll Fix
	[SerializeField] private bool canRoll=true;
	private BoxCollider boxCollider;
	private Vector3 originalCenter;
	private Vector3 originalSize;

	public bool gameOver = false;
	private static bool gravitySet = false;

	private int currentLane = 0; // -1 = Left, 0 = middle, 1 = right
	private const int minLane = -1;
	private const int maxLane = 1;

	[SerializeField] private float laneDist = 2f;
	[SerializeField] private float laneChangeSpeed = 10f;
	[SerializeField] private float laneChangeSmoothness = 0.25f;
	public AudioClip swipeSound;

	private Vector3 targetPos;
		
	private bool isChangingLanes = false;

	private float initialX;
	private float targetX;

	
	// Start is called before the first frame update
    void Start()
    {
		playerRb = GetComponent<Rigidbody>();
		if (!gravitySet)
		{
			Physics.gravity *= gravityModifier;
			gravitySet = true;
		}
		playerAudio = GetComponent<AudioSource>();
		playerAnim = GetComponent<Animator>();
		_coinManager = FindObjectOfType<CoinManager>();
		
		boxCollider = GetComponent<BoxCollider>();
		originalCenter = boxCollider.center;
		originalSize = boxCollider.size;
		
		// cache start X for lanes
		initialX = transform.position.x;
		targetX  = initialX;
    }

    // Update is called once per frame
    void Update()
    {
	    if (!gameOver)
	    {
		    if (((Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.UpArrow)) && isOnGround == true)
		    {
			    playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			    isOnGround = false;
			    canRoll = false;
			    playerAudio.PlayOneShot(jumpSound, 5.0f);
			    playerAnim.SetTrigger("Jump_trig");
			    dirtParticle.Stop();
		    }

		    else if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.DownArrow)) && canRoll)
		    {
			    StartCoroutine(RollCoroutine());
		    }
		    
		    // Lane switching - only allow if not already changing lanes
		    if (!isChangingLanes)
		    {
			    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
			    {
				    ChangeLane(-1);
			    }
			    else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
			    {
				    ChangeLane(1);
			    }
		    }
		    
		    // —— Smooth X movement toward targetX ——
		    float newX = Mathf.MoveTowards(transform.position.x, targetX,
			    laneChangeSpeed * Time.deltaTime);
		    transform.position = new Vector3(newX,
			    transform.position.y,
			    transform.position.z);
	    }
    }
    
    IEnumerator RollCoroutine()
    {
	    canRoll = false;
	    // Play animation
	    playerAnim.SetTrigger("Roll");

	    // Adjust collider for roll
	    boxCollider.center = new Vector3(0, 0.75f, 0); // Match crouch pose
	    boxCollider.size = new Vector3(1f, 1f, 1f);   // Shorter

	    yield return new WaitForSeconds(1.25f); // Match animation length

	    // Restore collider
	    boxCollider.center = originalCenter;
	    boxCollider.size = originalSize;
	    
	    canRoll = true;
    }

    private void OnCollisionEnter(Collision other){
		if(other.gameObject.CompareTag("Ground")){
			dirtParticle.Play();
			isOnGround = true;
			canRoll = true;
		}
		else if(other.gameObject.CompareTag("Obs")){
			//GameOver
			gameOver = true;
			crashParticle.Play();
			playerAnim.SetBool("Death_b", true);
			playerAnim.SetInteger("DeathType_int", 2);
			playerAudio.PlayOneShot(deathSound, 1.0f);
			dirtParticle.Stop();
		}
	}
    
    void ChangeLane(int direction)
    {
	    int newLane = currentLane + direction;
	    if (newLane < minLane || newLane > maxLane) return;

	    currentLane = newLane;
	    targetX = initialX + (currentLane * laneDist);

	    playerAudio.PlayOneShot(swipeSound, 0.55f);
	    if (direction < 0)
		    playerAnim.SetTrigger("MoveLeft");
	    else
		    playerAnim.SetTrigger("MoveRight");
    }
}
