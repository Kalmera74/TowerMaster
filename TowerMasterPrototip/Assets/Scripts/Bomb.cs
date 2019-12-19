using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float _blastRadius;
    [SerializeField]
    private Color _color;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private Material _skin;
    [SerializeField]
    private LayerMask _enemyMask;
    private ParticleSystem _explosionEffect;

    void Awake()
    {
        Skin = _skin;
    }

    private void Explode()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, _blastRadius,_enemyMask);
        foreach (Collider en in enemies)
            en.GetComponent<Enemy>().TakeDamage(_damage, _color);

        // Partical Effects

        Destroy(gameObject);
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

    private void OnCollisionEnter(Collision collision)
    {
      
        Explode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _blastRadius);
    }
}
