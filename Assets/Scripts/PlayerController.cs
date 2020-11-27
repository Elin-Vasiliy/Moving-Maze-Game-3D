using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public bool IsDie = false;
    public float Speed;

    private Animator animator;
    [SerializeField] private LayerMask _block;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private TouchManager _touchManager;
    [SerializeField] private GameObject _diePanel;
    [SerializeField] private GameObject[] _players;

    private bool isMoving = false;
    private bool isDir = false;

    public event UnityAction DiePlayer;

    private void OnEnable()
    {
        _touchManager.DoubleTap += DestoyForwardBlock;

        transform.GetChild(PlayerPrefs.GetInt("Count")).gameObject.SetActive(true);
        animator = transform.GetChild(PlayerPrefs.GetInt("Count")).gameObject.GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _touchManager.DoubleTap -= DestoyForwardBlock;
    }

    private void Update()
    {
        if (ChekingBlock(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z + 1.2f), 0.2f).Length > 0 && !isDir)
        {
            animator.Play("Idle");

            isMoving = false;
            Move(Vector3.zero);
        }
        else
        {
            isMoving = true;
        }

        if (_touchManager.isLeft)
        {
            isDir = true;
            isMoving = false;

            if (ChekingBlock(new Vector3(transform.position.x - 0.4f, transform.position.y + 1.5f, transform.position.z), 0.2f).Length > 1)
            {
                animator.Play("Idle");

                isMoving = false;
                Move(Vector3.zero);
            }
            else
            {
                isDir = true;
                Move(Vector3.left);
                animator.Play("Run Left");
            }
        }
        else if (_touchManager.isRight)
        {
            isDir = true;
            isMoving = false;

            if (ChekingBlock(new Vector3(transform.position.x + 0.4f, transform.position.y + 1.5f, transform.position.z), 0.2f).Length > 1)
            {
                animator.Play("Idle");

                isMoving = false;
                Move(Vector3.zero);
            }
            else
            {
                isDir = true;
                Move(Vector3.right);
                animator.Play("Run Right");
            }
        }
        else
        {
            isDir = false;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving && !isDir)
        {
            Move(Vector3.forward);
            animator.Play("Run Forward");
        }
    }

    private void Move(Vector3 dir)
    {
        _rb.velocity = Speed * dir;
    }

    private Collider[] ChekingBlock(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        return hitColliders;
    }

    private void OnBecameInvisible()
    {
        IsDie = true;
        Destroy(this.gameObject);
        if (_diePanel != null)
        {
            _diePanel.SetActive(true);
        }

        DiePlayer?.Invoke();
    }

    private void DestoyForwardBlock()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _block))
        {            
            Destroy(hit.collider.gameObject);
        }
    }
}
