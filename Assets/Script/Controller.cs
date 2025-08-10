using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Player player;
    public PlayerInput input;
    [SerializeField] float jumpSpace = 1f;
    [SerializeField] float animationExitTime = 0.6f;
    [SerializeField] GameObject gunObject;
    [SerializeField] Animator animCharacter;
    private bool isJumping = false;
    private float laneDistance = 1.1f;
    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right
    private int totalLanes = 3;

    public int maxAmmo = 6;
    private int currentAmmo;

    [SerializeField] private Transform firePoint;
    public float hitRange = 50f;
    public LayerMask hitLayer;
    public GameObject lineEffectPrefab;

    private void Start()
    {
        SetUpGunAndBullet();
    }

    void Update()
    {
        if (player == null || !player.isPlaying)
            return; 

        input.HandleTouchInput();
        input.HandleKeyboardFallback();
        MoveBehavior d = MoveDirectionSafe();

        if (d == MoveBehavior.Left)
        {
            if (currentLane > 0)
                currentLane--;
        }
        else if (d == MoveBehavior.Right)
        {
            if (currentLane < totalLanes - 1)
                currentLane++;
        }
        else if (d == MoveBehavior.Jump)
        {
            if (!isJumping)
                ExecuteJumpAnimation();
        }
        else if (d == MoveBehavior.Fire)
        {
            if (!isJumping && currentAmmo > 0)
            {
                Shoot();
                ExecuteFireAnimation();
            }
            else if(currentAmmo <= 0)
            {
                Reload();
            }
        }
        else if (d == MoveBehavior.Reload)
        {
            Reload();
        }

        Vector3 targetPos = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10f);
    }

    private MoveBehavior MoveDirectionSafe()
    {
        if (input == null) return MoveBehavior.None;
        return input.GetAndClearDirection();
    }

    private void ExecuteFireAnimation()
    {
        animCharacter.SetBool("Fire", true);
        gunObject.SetActive(true);
        StartCoroutine(ExitAnimation("Fire", animationExitTime));
    }
    private void ExecuteJumpAnimation()
    {
        isJumping = true;
        transform.position += new Vector3(0, jumpSpace, 0);
        animCharacter.SetBool("Jump", true);
        StartCoroutine(ExitAnimation("Jump", animationExitTime));
    }

    private void Reload()
    {
        currentAmmo = maxAmmo;
        EventManager.OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);
        EventManager.OnReloaded?.Invoke();
        AudioController.Ins.PlaySound(AudioController.Ins.reload, AudioController.Ins.sfxAus);
    }

    private void Shoot()
    {
        currentAmmo--;
        EventManager.OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);
        AudioController.Ins.PlaySound(AudioController.Ins.bullet, AudioController.Ins.sfxAus);
        CheckHitRange();
    }

    private void CheckHitRange()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hitRange, hitLayer))
        {
            ShowBulletLine(firePoint.position, hit.point);
            Obstacle obstacle = hit.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                obstacle.TakeDamage();
            }
        }
        else
        {
            Vector3 end = firePoint.position + firePoint.forward * 50f;
            ShowBulletLine(firePoint.position, end);
        }
    }

    private void ShowBulletLine(Vector3 start, Vector3 end)
    {
        if (lineEffectPrefab == null) return;
        GameObject line = Instantiate(lineEffectPrefab);
        LineRenderer lr = line.GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
        }
        Destroy(line, 0.1f);
    }

    IEnumerator ExitAnimation(string animString, float timeReset)
    {
        yield return new WaitForSeconds(timeReset);
        animCharacter.SetBool(animString, false);

        if (animString == "Jump")
        {
            isJumping = false;
        }

        if (animString == "Fire")
        {
            gunObject.SetActive(false);
        }
    }
    private void SetUpGunAndBullet()
    {
        if (gunObject != null) gunObject.SetActive(false);
        currentAmmo = maxAmmo;
        EventManager.OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);
    }
}
