using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Bandit : MonoBehaviour {

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;

    public int health;
    public float timeBetweenAttaks;
    float nextAttackTime;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;
    public int damage;
    bool banditAlive;

    // Start is called before the first frame update
    void Start () {
        banditAlive = true;
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    // Update is called once per frame
    void Update () {
        if (banditAlive == true)
        {
            //Check if character just landed on the ground
            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //Check if character just started falling
            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            // -- Handle input and movement --
            float inputX = Input.GetAxis("Horizontal");

            // Swap direction of sprite depending on walk direction
            if (inputX > 0)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (inputX < 0)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // Move
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

            //Attack
            if (Time.time > nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
                    foreach (Collider2D col in enemiesToDamage)
                    {
                        col.GetComponent<scorpion>().TakeDamage(damage);
                    }

                    m_animator.SetTrigger("Attack");
                    nextAttackTime = Time.time + timeBetweenAttaks;
                }
            }

            //Change between idle and combat idle (extra feature)
            if (Input.GetKeyDown("f"))
                m_combatIdle = !m_combatIdle;

            //Jump
            if (Input.GetKeyDown("space") && m_grounded)
            {
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                m_animator.SetInteger("AnimState", 2);

            //Combat Idle
            else if (m_combatIdle)
                m_animator.SetInteger("AnimState", 1);

            //Idle
            else
                m_animator.SetInteger("AnimState", 0);

        }
    }

    //Allows the player to take damage when collide with enemy box collider
    public void TakeDamage(int damage)
    {
        m_animator.SetTrigger("Hurt");
        health -= damage;
        if(health <= 0)
        {
            banditAlive = false;
            m_animator.SetTrigger("Death");
            Invoke("AfterDeathTimer", 3);
            Invoke("LoadNextScene", 2);
        }
    }

    //Destroy character 3 seconds after died
    public void  AfterDeathTimer()
    {
        Destroy(gameObject);
    }

    //Load End Scene after character died
    public void LoadNextScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
