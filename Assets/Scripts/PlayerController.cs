using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    bool status = true;
    public float speed = 8;
    public float forceJump = 6;
    private float horizontalInput;
    private float forwardInput;
    public float rotateSpeed = 700;
    private float rotateInput;

    public Rigidbody rb;
    bool grounded;

    public Text score;
    public int totalItems;
    public Text winText;
    public Text loseText;
    public int itemsCollected;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grounded = true;
        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
        UpdateUI();
        winText.enabled = false;
        loseText.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (status == true)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontalInput, 0, forwardInput);
            direction = Vector3.ClampMagnitude(direction, 1) * speed * Time.deltaTime;
            transform.Translate(direction);

            rotateInput = Input.GetAxis("Mouse X");
            float angle = rotateInput * rotateSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * angle);

            if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
            {
                rb.AddForce(0, forceJump, 0, ForceMode.Impulse);
                grounded = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            itemsCollected++;
            UpdateUI();
            if (itemsCollected == totalItems)
            {
                status = false;
                winText.enabled = true;
            }

        }

        if (collision.gameObject.CompareTag("GameOver"))
        {
            loseText.enabled = true;
            status = false;
        }
    }

    void UpdateUI()
    {
        score.text = itemsCollected.ToString() + " / " + totalItems.ToString();
    }
}
