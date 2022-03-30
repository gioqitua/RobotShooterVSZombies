using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeReference] float shootingDelay = 0.1f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Transform bulletStartPos;
    [SerializeField] LayerMask aimLayerMask;
    [SerializeField] float rotationSpeed = 5f;
    private float nextShootTime;
    Animator anime;

    void Start()
    {
        anime = GetComponent<Animator>();
    }

    void Update()
    {
        ApplyMovement();
        AimTowardMouse();

        if (CanShoot())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        nextShootTime = Time.time + shootingDelay;
        ActivateBulletFromPool(transform.forward);
    }

    private void ActivateBulletFromPool(Vector3 direction)
    {
        var bullet = BulletPool.Instance.Get();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = bulletStartPos.position;
        bullet.Launch(direction);
    }

    void AimTowardMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask))
        {
            var destination = hitInfo.point;

            destination.y = transform.position.y;

            Vector3 direction = destination - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    private bool CanShoot()
    {
        return Time.time >= nextShootTime;
    }

    private void ApplyMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        AnimationHandler(horizontal, vertical);

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);

        transform.position += movementDirection * moveSpeed * Time.deltaTime;

    }
    void AnimationHandler(float horizontal, float vertical)
    {
        anime.SetFloat("Horizontal", horizontal);
        anime.SetFloat("Vertical", vertical);

    }

}
