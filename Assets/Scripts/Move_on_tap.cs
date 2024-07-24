using UnityEngine;

public class TapMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 
    private Vector3 targetPosition;
    private bool isMoving = false;
    private Animator animator;
    private BoxCollider2D boxCollider;
    public GameObject[] upSprites;  
    public GameObject[] downSprites; 

    void Start()
    {
        targetPosition = transform.position; 
        SetSpritesActive(upSprites, false);
        SetSpritesActive(downSprites, true); 
        boxCollider = GetComponent<BoxCollider2D>(); 
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0; 
            isMoving = true;
            animator.SetBool("isMoving", true);
            boxCollider.enabled = false;
        }

        if (isMoving)
        {
            Vector3 previousPosition = transform.position;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }

            if (transform.position.y > previousPosition.y)
            {
                SetSpritesActive(upSprites, true);
                SetSpritesActive(downSprites, false);
            }
            else if (transform.position.y < previousPosition.y)
            {
                SetSpritesActive(upSprites, false);
                SetSpritesActive(downSprites, true);
            }
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                animator.SetBool("isMoving", false);
                boxCollider.enabled = true;
            }
        }
    }

    void SetSpritesActive(GameObject[] sprites, bool isActive)
    {
        foreach (GameObject sprite in sprites)
        {
            sprite.SetActive(isActive);
        }
    }
}