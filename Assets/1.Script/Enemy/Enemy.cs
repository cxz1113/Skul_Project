using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct EnemyData
{
    public EnemyType type;
    public EnemyState state;
    public float maxhp;
    public float hp;
    public float damage;
    public float atkRange;
    public float atkDelay;

    public bool isDead;
}

public enum EnemyType
{
    Tanker,
    Archer,
    Knight
}

public enum EnemyState
{
    Idle,
    Move,
    Attack,
    Hit,
    Dead
}

public abstract class Enemy : MonoBehaviour
{
    public EnemyData ed = new EnemyData();

    [SerializeField] GameObject hpBar;
    [SerializeField] Image hpBar_img;
    [SerializeField] TMP_Text damage_Text;
    [SerializeField] GameObject attack_Fx;
    public GameObject canvas;
    public Rigidbody2D rigid;
    public CapsuleCollider2D capsuleColl;
    public Animator anim;
    public Transform target;
    public WayPoint2 way2;
    public CountCheck killCheck;
    public Attack_Box_Enemy atkBox;

    

    public int nextMove;
    protected bool canThink = true;
    protected bool canAttack = true;
    protected bool flipX;

    

    void Awake()
    {
        killCheck = FindObjectOfType<CountCheck>();
        ed.isDead = false;
        Think();

    }
    void Start()
    {
        Init();
        canvas = GameObject.Find("Canvas");
        target = GameObject.FindWithTag("Player").transform;
        Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), target.gameObject.GetComponent<CapsuleCollider2D>());
    }
    public abstract void Init();
    void FixedUpdate()
    {
        if (ed.state == EnemyState.Hit || ed.state == EnemyState.Dead)
            return;

        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        PlatformCheck();
    }

    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
            Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), target.gameObject.GetComponent<CapsuleCollider2D>());
            return;
        }

        if (ed.state == EnemyState.Dead)
            return;

        if (ed.isDead == true || ed.hp <= 0)
        {
            StopAllCoroutines();
            //nextMove = 0;
            anim.SetBool("Dead", true);
            ed.state = EnemyState.Dead;
            rigid.velocity = Vector2.zero;
            rigid.bodyType = RigidbodyType2D.Static;
            capsuleColl.isTrigger = true;
            killCheck.killCount--;
            MapManager.Instance.spawnCount--;
            Invoke("Die", 2f);
        }
        DamageTest();

        if (ed.state == EnemyState.Attack)
            return;

        // 공격 거리 체크
        if (Vector3.Distance(transform.position, target.position) < ed.atkRange)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            
            flipX = target.position.x > transform.position.x ? false : true;

            if (canAttack)
                AttackStart();
            else if (Vector3.Distance(transform.position, target.position) > ed.atkRange)
            {
                nextMove = flipX ? -3 : 3;
                anim.SetInteger("Walk", nextMove);
            }
        }
        else if (canThink && ed.state != EnemyState.Attack)
        {
            StartCoroutine("Think");
        }
        transform.localScale = flipX ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

    }

    public IEnumerator Think()
    {
        canThink = false;

        //다음 움직임 방향 * 속도
        nextMove = Random.Range(-1, 2) * 3;
        ed.state = nextMove == 0 ? EnemyState.Idle : EnemyState.Move;
        anim.SetInteger("Walk", nextMove);

        //스프라이트 Flip.x
        if (nextMove != 0)
            flipX = nextMove == -3;

        yield return new WaitForSeconds(Random.Range(1, 3));

        canThink = true;
    }

    public void Turn()
    {
        nextMove *= -1;
        flipX = nextMove == -3;

        CancelInvoke();

        Invoke("Think", 2);
    }

    protected virtual void AttackStart()
    {
        StartCoroutine("AttackCoolDown");
        nextMove = 0;
        anim.SetTrigger("Attack");
        ed.state = EnemyState.Attack;
    }

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(ed.atkDelay);
        canAttack = true;
    }

    //공격 끝나는 시점, hit 끝나는 시점
    void EventAttackEnd()
    {
        ed.state = EnemyState.Idle;
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Hit");
        StartCoroutine("Think");
    }

    public virtual void Damaged(float damage)
    {  
        if (ed.state != EnemyState.Dead)
        {
            ed.hp -= damage;
            CreateDamage_Text(damage);
            CreateFx_Effect();
            anim.SetTrigger("Hit");
            ed.state = EnemyState.Hit;
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        nextMove = 0;
    }

    public void CreateDamage_Text(float damage)
    {
        Vector3 pos = transform.position + Vector3.up * capsuleColl.size.y * 0.7f;
        TMP_Text dmgTxt = Instantiate(damage_Text, pos, Quaternion.identity, canvas.transform);
        dmgTxt.text = damage.ToString();
    }

    public void CreateFx_Effect()
    {
        float x = Random.Range(-capsuleColl.size.x, capsuleColl.size.x);
        float y = Random.Range(0, capsuleColl.size.y);
        Vector3 pos = transform.position + new Vector3(x,y);

        GameObject attack_Effect = Instantiate(attack_Fx, pos, Quaternion.identity, FxManager.Instance.transform);
        //int scaleX = transform.position.x >player;
        //attack_Effect.transform.localScale = new Vector3(scaleX, attack_Effect.transform.localScale.y);

        //GameObject attack_Effect2 = Instantiate(FxManager.Instance.FxByPlayer(), pos, Quaternion.identity, transform);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void DamageTest()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ed.hp -= 100;
        }
    }

    void PlatformCheck()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.75f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
            Turn();
    }
}
