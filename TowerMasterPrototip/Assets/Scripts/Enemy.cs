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


    void Awake()
    {
        Skin = _skin;
    }

    private void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        transform.position += new Vector3(_moveSpeed*Time.deltaTime, 0, 0);
    }
    
    private void Die()
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
