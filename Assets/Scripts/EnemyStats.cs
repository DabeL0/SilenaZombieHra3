using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    private int damage;
    public int Damage => damage;

    private MeshRenderer meshRenderer;

    [SerializeField]
    private float maxHp;
    private float currentHp;


    [SerializeField]
    private Image hpBar;

    private float CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = Mathf.Max(value, 0f);
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

    private void Awake()
    {   
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        CurrentHp = maxHp;
    }

    public void SetType(Material color, int damage)
    {
        meshRenderer.material = color;
        this.damage = damage;
    }

    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        if(CurrentHp <= 0f) { Destroy(gameObject); }
    }
}
