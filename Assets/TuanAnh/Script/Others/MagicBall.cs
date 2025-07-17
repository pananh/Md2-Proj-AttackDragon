using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MagicBall : MonoBehaviour
{

    private float speed;
    private float timeFly;
    private float maxTime;
    private Vector3 targetBall;
    private const float ballRadius = 0.5f;
    private Vector3 direction;
    private float castDistance;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private AudioClip explosionSound;


    private bool needMoving;
    public bool NeedMoving
    {
        get => needMoving;
        set => needMoving = value;
    }



    public void Init(float speed, float MaxTime, Vector3 targetBall)
    {
        timeFly = 0f;
        this.speed = speed;
        this.maxTime = MaxTime;
        this.targetBall = targetBall;
        needMoving = false;
        direction = (targetBall - transform.position).normalized;

    }

    void Update()
    {
        if (!needMoving)
        {
            return;
        }
        MoveForward();
    }

    private void PlayExplosionSound()
    {
        float t = Mathf.InverseLerp(1f, 100f, Vector3.SqrMagnitude(transform.position - PlayerController.Instance.transform.position));
        float volume = Mathf.Lerp(1f, 0.3f, t);
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, volume);
        //AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // tranh bi destroy som lam am thanh bi mat
    }
    private void MoveForward()
    {
        timeFly += Time.deltaTime;
        castDistance = speed * Time.deltaTime;
        if (Physics.SphereCast(transform.position, ballRadius, direction, out RaycastHit hit, castDistance, layerMask))
        {
            PlayExplosionSound();
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<PlayerController>().TakeDamage(1);
                Destroy(gameObject);
                return;
            }
            else if (hit.collider.CompareTag("Monster"))
            {
                Debug.Log("Hit Monster");
                Destroy(gameObject);

                hit.collider.GetComponent<IMonsterController>().TakeDamage(
                    PlayerController.Instance.CurrentPlayerData.attackMagic);

                return;
            }
            else if (hit.collider.CompareTag("Ground"))
            {
                Destroy(gameObject);
                return;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, targetBall, speed * Time.deltaTime);
        if (timeFly > maxTime)
        {
            Destroy(gameObject);
        }
    }



}
