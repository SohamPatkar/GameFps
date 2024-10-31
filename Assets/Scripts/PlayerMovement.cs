using UnityEngine;

public class PlayerMovement : MonoBehaviour, IInteractable
{
    [SerializeField] public GameObject _camera;
    [SerializeField] private GameObject _holdingPlace;
    [SerializeField] private float throwForce = 6f;
    [SerializeField] private Rigidbody _playerRigidBody;
    public static PlayerMovement Instance { get; set; }
    private float _rotationX, _rotationY;
    private bool isTurnedOn;
    private bool isPicked = false;
    // Update is called once per frame

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        LookAround();
        Movement();
    }

    void LookAround()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rotationX += Input.GetAxis("Mouse Y") * 2f;
        _rotationY += Input.GetAxis("Mouse X") * 2f;
        _camera.transform.localEulerAngles = new Vector3(-_rotationX, _rotationY, 0);
        Interact();
        if (Input.GetKeyDown(KeyCode.T))
        {
            ThrowObject();
        }
    }

    public bool isPickedCheck()
    {
        return isPicked;
    }

    void Interact()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameObject _item = hit.collider.gameObject;
                    if (_item.CompareTag("Interactable"))
                    {
                        PickUp(_item);
                    }
                    else
                    {
                        Debug.Log("Not Interactable");
                    }
                }
            }
            // Debug.DrawRay(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            // Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    void Movement()
    {
        float _horizontalAxis = Input.GetAxis("Horizontal");
        float _verticalAxis = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Interaction();
        }

        transform.Translate(Vector3.forward * _verticalAxis * 2f * Time.deltaTime);
        transform.Translate(Vector3.right * _horizontalAxis * 2f * Time.deltaTime);
    }

    void PickUp(GameObject item)
    {
        isPicked = true;
        item.transform.SetParent(_holdingPlace.transform);
        item.GetComponent<BoxCollider>().enabled = false;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.position = _holdingPlace.transform.position;
        item.transform.rotation = _holdingPlace.transform.rotation;

    }

    //Throwing the object
    void ThrowObject()
    {
        isPicked = false;
        if (_holdingPlace.transform.childCount != 0)
        {
            Rigidbody _holdingPlaceObjectRB = _holdingPlace.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
            _holdingPlace.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
            _holdingPlace.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic = false;
            _holdingPlace.transform.GetChild(0).transform.SetParent(null);
            _holdingPlaceObjectRB.AddForce(_camera.transform.forward * throwForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("No object Picked!");
        }
    }

    public void Interaction()
    {
        GameObject _heldItem = _holdingPlace.transform.GetChild(0).gameObject;
        GameObject _lightItem = _heldItem.transform.GetChild(1).gameObject;
        if (_lightItem.CompareTag("Flashlight"))
        {
            if (isTurnedOn)
            {
                _lightItem.SetActive(false);
                isTurnedOn = false;
            }
            else
            {
                _lightItem.SetActive(true);
                isTurnedOn = true;
            }
        }
    }
}
