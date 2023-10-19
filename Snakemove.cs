using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private List<Transform> _tails;
    [SerializeField] private float _bonesDistance;
    [SerializeField] private GameObject _BonePrefab;
    [Range(0, 8), SerializeField] private float _moveSpeed;
    public enum Direction {
        Up, 
        Right,
        Down,
        Left
    }

    public GameObject BodyPrefab;
    private List<GameObject> BodyParts = new List<GameObject>();
    private Direction direction;

    private void Update()
    {
        MoveHead(_moveSpeed);
        HandleInput();
        MoveTail();
        Rotate();
    }
    void Start()
    {
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
    }
    private void GrowSnake()
    {
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }


    void MoveHead(float speed) 
    {
        switch(direction)
        {
            case Direction.Up:
                transform.position += transform.forward * _moveSpeed * Time.deltaTime;
                break;
            
            case Direction.Right:  
                transform.position += transform.forward * _moveSpeed * Time.deltaTime;
                break;
            
            case Direction.Down:
                transform.position += transform.forward * _moveSpeed * Time.deltaTime; 
                break;
            
            case Direction.Left:
                transform.position += transform.forward * _moveSpeed * Time.deltaTime;
                break;
        }
    }

    void HandleInput() {
        if (Input.GetKeyDown(KeyCode.D)) {
            direction = Direction.Right;
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            direction = Direction.Down;
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            direction = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.W)) {
            direction = Direction.Up;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rigid))
        {
            Destroy(other.gameObject);
            GameObject bone = Instantiate(_BonePrefab);
            _tails.Add(bone.transform);
        }
    }

    void MoveTail()
    {
        for (int i = _tails.Count - 1; i > 0; i--)
        {
            _tails[i].position = _tails[i - 1].position;
        }
    }
    void Rotate()
    {
        Quaternion targetRotation = Quaternion.identity;

        switch (direction)
        {
            case Direction.Up:
                targetRotation = Quaternion.Euler(0, 0, 0);
                break;

            case Direction.Right:
                targetRotation = Quaternion.Euler(0, 90, 0);
                break;

            case Direction.Down:
                targetRotation = Quaternion.Euler(0, 180, 0);
                break;

            case Direction.Left:
                targetRotation = Quaternion.Euler(0, 270, 0);
                break;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _moveSpeed);
    }
}

