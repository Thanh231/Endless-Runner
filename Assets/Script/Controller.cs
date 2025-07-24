using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    [SerializeField]
    float jumpSpace = 1f;
    [SerializeField]
    float animationExitTime = 0.6f;
    [SerializeField]
    GameObject gunObject;

    [SerializeField]
    Animator animCharacter;
    private bool isJumping = false;
    private float laneDistance = 3f;
    private int currentLane = 1;
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
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentLane > 0)
                currentLane--;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentLane < totalLanes - 1)
                currentLane++;
        }

        Vector3 targetPos = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10f);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            ExecuteJumpAnimation();
        }

        if (Input.GetKeyDown(KeyCode.F) && !isJumping)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                ExecuteFireAnimation();
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
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

    }

    private void Shoot()
    {
        currentAmmo--;
        EventManager.OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);
        Vector3 end = firePoint.position + firePoint.forward * 50f;
        CheckHitRange();
        ShowBulletLine(firePoint.position, end);
    }

    private void CheckHitRange()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hitRange, hitLayer))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);

            Obstacle obstacle = hit.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                obstacle.TakeDamage();
            }
        }
    }

    private void ShowBulletLine(Vector3 start, Vector3 end)
    {
        GameObject line = Instantiate(lineEffectPrefab);
        LineRenderer lr = line.GetComponent<LineRenderer>();

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        Destroy(line, 0.1f);
    }

    IEnumerator ExitAnimation(string animString, float timeReset)
    {
        yield return new WaitForSeconds(timeReset);
        animCharacter.SetBool(animString, false);
        isJumping = false;
        gunObject.SetActive(false);
    }
    private void SetUpGunAndBullet()
    {
        gunObject.SetActive(false);
        currentAmmo = maxAmmo;
        EventManager.OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);
    }
}
