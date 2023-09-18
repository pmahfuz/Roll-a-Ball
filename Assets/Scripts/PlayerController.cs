using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

	public int maxLife = 1;
    public int currentLife;

	public Transform respawnPoint;

    private float movementX;
    private float movementY;

	private Rigidbody rb;
	private int count;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		currentLife = maxLife;

		// Set the count to zero 
		count = 0;

		SetCountText ();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
		
	}

	void DelayedAction()
    {
        Debug.Log("Delayed Action");
    }

	void Update()
	{
		if (transform.position.y < -10)
		{
			Respawn();
		}
	}

	void FixedUpdate ()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}
	}

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;
    }

    void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 12) 
		{
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
			Invoke("DelayedAction", 100000);
			SceneManager.LoadScene("MainMenu");
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			currentLife--;
			if (currentLife <= 0)
			{
				SceneManager.LoadScene("GameOver");
			}
		}
	}

	void Respawn(){
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.Sleep();

		transform.position = respawnPoint.position;
	}
}
