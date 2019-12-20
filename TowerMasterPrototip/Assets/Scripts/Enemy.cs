using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int _health;
    [SerializeField]
    protected float _moveSpeed;
    [SerializeField]
    protected Color _color;
    [SerializeField]
    protected int _gold;
    [SerializeField]
    protected Material _skin;
    [SerializeField]
    protected ParticleSystem _deathEffect;
    public SkinnedMeshRenderer renderer;


    private void Start()
    {
        // Skin = GameManager.GetRandomEnemyColor(0,4);
        // transform.LookAt(GameManager.Player.transform.forward);
        Skin = _skin;
    }

    private void Grow()
    {
        transform.localScale = transform.localScale * 1.2f;
        _moveSpeed += 0.3f;
    }
    private void Shrink()
    {
        transform.localScale = transform.localScale * 0.9f;
        _moveSpeed -= 0.3f;
    }

    private void Update()
    {
        Move();
       
    }

    protected virtual void Move()
    {

        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward*3);
    }

    protected virtual void Die()
    {
        GameManager.Player.AddGold(_gold);
        // Partical Effect
        Destroy(gameObject);
    }
    public void TakeDamage(int damage, Color damageType)
    {
        if (damageType.Equals(_color))
        {
            _health += damage;
            Grow();
        }   
        else
            if (_health - damage <= 0)
                Die();
            else
            {
                _health -= damage;
                Shrink();
            }
            
    }
    public Material Skin
    {
        get { return _skin; }
        set
        {
            _skin = value;
            renderer.material = _skin;
            _color = _skin.color;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Castle")
        {
            GameManager.Lost();
        }
    }

}
