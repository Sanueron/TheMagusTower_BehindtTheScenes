using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MagusAnimManager
{
    // Game Manager
    [SerializeField] GameManager gameManager;
    // Movement
    [SerializeField]
    private Rigidbody rb;
    // Camera
    public Camera playerCamera;
    public GameObject cameraPosition, playerLookAtPosition;
    private float inputHorizontal, inputVertical;
    public Vector3 localVelocity;

    // Step Climb variables
    [SerializeField] GameObject stepRayUpper, stepRayLower;
    [SerializeField] float stepSmooth = 0.1f;

    // Stats
    [Range(0, 50)]
    public int speed, rotateSpeed, speedMax, damage;
    public int jumpForce;
    public int health, maxHealth, minHealth, magicPoints, maxMP, minMP;
    public bool onFloor, onAir, isInvincible, receivedDamage, isDead;

    // Magic Casting MP cost
    private int mpCostForForBasicSpell = 5, mpCostForsummonSpell = 10, mpCostForSpecialSpell = 50;

    // Temporizadores
    [SerializeField] internal float invincibilityTimer;
    private float moveOnAirTimer, maxTimeOnAir = 0.5f;

    // Spells block bool variables
    [SerializeField] internal bool wait, specialSpellUnlocked;

    // VFX attacks
    internal ParticleSystem earthShatter;
    [SerializeField] GameObject bubble;
    PoolManager bubblePool;

    // VFX Status effects
    [SerializeField] internal ParticleSystem healingHP, healingMP;

    //[SerializeField] private GameObject fireballManager, fireballSpell;
    [SerializeField] private GameObject fireball;

    // Audio
    public GameObject earthShatterSFX;
    AudioSource aSEarthShatter;

    // UI
    [SerializeField] Slider _HPSlider, _MPSlider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image hpfill;

    //Fuunciones principales
    // Start is called before the first frame update
    void Start()
    {
        // Al inicio "Start" asignamos a la variable "rb" el Rigidbody del gameObject que lleva el Script
        rb = transform.GetComponent<Rigidbody>();
        
        // Asignamos el animator a la variable magusAnimator
        magusAnimator = GetComponent<Animator>();

        // Asignamos los elementos de los ataques a sus respectivas variables  
        earthShatter = GetComponentInChildren<ParticleSystem>();
        
        // Paramos los VFX del jugador
        earthShatter.Stop();
        healingHP.Stop();
        healingMP.Stop();

        // Desactivamos la burbuja
        //bubble.SetActive(false);
        bubblePool = FindObjectOfType<PoolManager>();

        // Asiganmos los AudioSource
        aSEarthShatter = earthShatterSFX.GetComponentInChildren<AudioSource>();

        //Starting stats
        maxHealth = 100;
        _HPSlider.maxValue = maxHealth;
        health = maxHealth;
        minHealth = 0;
        maxMP = 100;
        _MPSlider.maxValue = maxMP;
        minMP = 0;
        magicPoints = maxMP;
        damage = 10;
        isDead = false;

        // Establecemos estado de invincibilidad en nulo
        isInvincible = false;
        receivedDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Asignamos los valores de la entrada de los ejes de movimiento
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");
        localVelocity = transform.InverseTransformDirection(rb.velocity);
       
        HPManager();
        InvincibilityCheck();
        MPManager();
        // Funcion para asignar la Camara al punto de seguimiento del personaje y que mire siempre al punto que mira el personaje
        CameraPostion();
        RotateCamera();
        RotateCameraUpandDown();
        Vector3 velDir = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (velDir.magnitude >= speedMax)
        {
            velDir = velDir.normalized * speedMax;
            rb.velocity = new Vector3(velDir.x, rb.velocity.y, velDir.z);
        }        
        BasicSpell();
        SummonSpell();
        SpecialSpell();

    }
    private void FixedUpdate()
    {
        Jump();
        if (onAir)
        {
            moveOnAirTimer += Time.deltaTime;
            if(moveOnAirTimer < maxTimeOnAir)
            {
                PlayerMovement();
            }
        }
        else
        {
            PlayerMovement();
            moveOnAirTimer = 0;
        }
        StepClimb();

    }
    private void LateUpdate()
    {
        //magusAnimator.SetBool("CastBasicSpell", false);
        magusAnimator.SetFloat("Speed", localVelocity.z / speedMax);
        magusAnimator.SetFloat("HorSpeed", localVelocity.x / speedMax);
    }

    #region Movement and Camera
    private void PlayerMovement()
    {
        Vector3 dir = (transform.forward * inputVertical) + transform.right * inputHorizontal;
        rb.AddForce(dir * speed * rb.mass);
    }
    private void CameraPostion()
    {
        playerCamera.transform.position = cameraPosition.transform.position;
        playerCamera.transform.LookAt(playerLookAtPosition.transform.position);
    }
    private void RotateCamera()
    {
        if (Input.GetKey("q"))
        {
            if (!wait)
            {
                // Función para rotar al personaje a la izquierda
                transform.Rotate(0, -rotateSpeed, 0);
            }
        }
        else if (Input.GetKey("e"))
        {
            if (!wait)
            {
                // Función para rotar al personaje a la izquierda
                transform.Rotate(0, rotateSpeed, 0);
            }
        }
        else
        {
            return;
        }
    }
    private void RotateCameraUpandDown()
    {
        if (Input.GetKey("t"))
        {
            if (!wait)
            {
                // Función para la camara hacia arriba
                if(cameraPosition.transform.localPosition.y < 5)
                {
                    cameraPosition.transform.position += new Vector3(0, 0.1f, 0);
                }
            }
        }
        else if (Input.GetKey("g"))
        {
            if (!wait)
            {
                // Función para rotar la camara hacia abajo
                if(cameraPosition.transform.localPosition.y > 1)
                {
                    cameraPosition.transform.position += new Vector3(0, -0.1f, 0);
                }
            }
        }
    }
    private void Jump()
    {
        if(onFloor == true)
        {
            if (Input.GetKey("j"))
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }        
    }
    void StepClimb()
    {
        if (Input.GetKey("w"))
        {
            RaycastHit hitLower;
            if (Physics.Raycast(stepRayLower.transform.position, stepRayLower.transform.forward, out hitLower, 0.1f))
            {
                RaycastHit hitUpper;
                if (!Physics.Raycast(stepRayUpper.transform.position, stepRayUpper.transform.forward, out hitUpper, 0.2f))
                {
                    rb.position -= new Vector3(0, -stepSmooth, 0);
                }
            }
        }
    }
    #endregion
    #region HP and MP
    public void HPManager()
    {
        _HPSlider.maxValue = maxHealth;
        _HPSlider.value = health;
        hpfill.color = gradient.Evaluate(_HPSlider.normalizedValue);
        if (health >= maxHealth)
        {
            health = maxHealth;
            isDead = false;
        }
        else if (health <= minHealth)
        {
            health = minHealth;
            isDead = true;
        }
        else { isDead = false; }
    }
    public void MPManager()
    {
        _MPSlider.maxValue = maxMP;
        _MPSlider.value = magicPoints;
        if (magicPoints >= maxMP)
        {
            magicPoints = maxMP;
        }
        else if (magicPoints <= minMP)
        {
            magicPoints = minMP;
        }
        else { return; }
    }
    private void InvincibilityCheck()
    {
        if(isInvincible)
        {
            invincibilityTimer += Time.deltaTime;
        }
        if (receivedDamage)
        {
            isInvincible = true;
            invincibilityTimer = 0;
        }
        if(invincibilityTimer > 1)
        {
            isInvincible = false;
            receivedDamage = false;
        }
    }
    #endregion

    #region Spells and Attacks
    private void BasicSpell()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if(magicPoints >= mpCostForForBasicSpell && !wait)
            {
                wait = true;
                magicPoints -= mpCostForForBasicSpell;
                speed = 0;
                magusAnimator.SetBool("CastBasicSpell", true);
                StartCoroutine("BubbleAttack", 0.5f);
                StartCoroutine("Contador");
                StartCoroutine("ResetSpeed", 1f);
            }

        }

    }
    private void SummonSpell()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(magicPoints >= mpCostForsummonSpell && !wait)
            {
                wait = true;
                magicPoints -= mpCostForsummonSpell;
                // Detenmos el movimiento del jugador
                speed = 0;
                // Indicamos el hechizo con un gameObject o un transform

                // Activamos animacion
                magusAnimator.SetBool("CastSummonSpell", true);
                aSEarthShatter.PlayDelayed(0.5f);
                StartCoroutine("Contador");
                StartCoroutine("EarthShatter", 0.5f);
                StartCoroutine("ResetSpeed", 2f);
                StartCoroutine("EarthShatterSetOff", 2f);
            }
        }
    }
    private void SpecialSpell()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (specialSpellUnlocked)
            {
                if (magicPoints >= mpCostForSpecialSpell && !wait)
                {
                    if (!fireball.activeSelf)
                    {
                        wait = true;
                        magicPoints -= mpCostForSpecialSpell;
                        speed = 0;
                        magusAnimator.SetBool("CastBasicSpell", true);
                        StartCoroutine("Fireball", 0.5f);
                        StartCoroutine("Contador");
                        StartCoroutine("ResetSpeed", 1f);
                    }
                }
            }
        }
    }
    #endregion

    #region IEnumerators of Attacks
    IEnumerator Contador()
    {
        yield return new WaitForSeconds(2f);
        wait = false;
        magusAnimator.SetBool("CastBasicSpell", false);
        magusAnimator.SetBool("CastSummonSpell", false);
    }
    IEnumerator EarthShatter(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        // Activamos el VFX
        earthShatter.Play();
    }
    IEnumerator EarthShatterSetOff(float contador)
    {
        yield return new WaitForSeconds(contador);
        earthShatter.Stop();
    }
    IEnumerator BubbleAttack(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        bubblePool.RequestObjects();
        //bubble.SetActive(true);
    }
    IEnumerator Fireball(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        //Activate Fireball
        fireball.SetActive(true);
    }
    IEnumerator ResetSpeed(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        speed = 20;
    }
    #endregion

    #region Collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = true;
            onAir = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            onFloor = true;
            onAir = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = false;
            onAir = true;
        }
    }
    #endregion
}
