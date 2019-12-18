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


    private void Start()
    {
        Skin = GameManager.GetRandomEnemyColor(0,3);
       // transform.LookAt(GameManager.Player.transform.forward);
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
            _health += damage;
        else
            if (_health - damage <= 0)
                Die();
            else
                _health -= damage;

       // Debug.Log("Ouch " + damage + ", " + damageType);
    }
    public Material Skin
    {
        get { return _skin; }
        set
        {
            _skin = value;
            GetComponent<MeshRenderer>().material = _skin;
            _color = _skin.color;
        }
    }

}
