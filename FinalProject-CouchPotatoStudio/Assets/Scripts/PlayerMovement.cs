using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject mainCamera;

    //==Movement==
    public CharacterController controller;
    public float speed = 12f;

    //==Jump==
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 velocity;
    public Vector3 lastSafePosition;
    private bool isGrounded, doubleJump = true;
    private bool wallRunning = false;
    private float wallRunDelay;
    private Transform lastWallrun;
    public float airTime = 0f;

    public bool attacking = false;
    public float comboTime = 0f;
    private float attackCooldown = 0f;
    private int comboState = 0;

    public bool dashing = false;
    private int dashRetargetCooldown = 1;
    private Camera mainCam;
    private GameObject dashTarget;
    public UnityEngine.UI.Image dashReticle;
    public GameObject dashHitbox;

    public Animator animator;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //dashing code
        if(dashRetargetCooldown == 0 && !dashing) //code to find a new dash target
        {
            dashTarget = FindDashTarget();
            dashRetargetCooldown++;
        }
        else if (dashRetargetCooldown < 9) //only check for a new dash target every 10 frames to improve performance
        {
            dashRetargetCooldown++;
        }
        else
        {
            dashRetargetCooldown = 0;
        }

        if (Input.GetButtonDown("Dash") && dashTarget != null && !wallRunning)
        {
            dashing = true;
            dashHitbox.SetActive(true);
            transform.rotation = Quaternion.LookRotation(dashTarget.transform.position - transform.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        if (dashing)
        {
            if (Vector3.Distance(transform.position, dashTarget.transform.position) <= dashTarget.GetComponent<DashTarget>().stopRadius)
            {
                dashHitbox.SetActive(false);
                dashTarget.GetComponent<DashTarget>().cooldown = Time.time + 2.5f;
                dashTarget = null;
                dashReticle.color = new Color(1, 1, 1, 0);
                dashing = false;
                airTime = Time.time + 0.25f;
            }
            if (dashing)
            {
                Vector3 dashDirection = dashTarget.transform.position - transform.position;
                controller.Move(Vector3.Normalize(dashDirection) * speed * 5 * Time.deltaTime);
            }
        }

        //========Moving code=============
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (dashing) //if the player is dashing they cannot move until the dash is finished
        {
            x = 0;
            z = 0;
        }
        if (x != 0 || z != 0) // if the player moves they will stop floating
        {
            airTime = 0f;
        }

        if (wallRunning && wallRunDelay < Time.time) //if the player has not moved for more than half a second on a wall they will stop wallrunning
        {
            wallRunning = false;
            wallRunDelay = Time.time + 0.5f;
        }

        //rotate the player to the same direction as the camera when they move if they are not wallrunning or dashing
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && !wallRunning && !dashing)
        {
            transform.localRotation = Quaternion.Euler(0, mainCamera.transform.parent.localRotation.eulerAngles.y, 0);
        }
        //if the player is wallrunning they cannot move sideways or backwards
        if (wallRunning)
        {
            x = 0;
            if (z < 0)
            {
                z = 0;
            }
            else if (z > 0) //if the player has moved while wallrunning reset the time they can stay on the wall
            {
                z *= 1.5f; //increased speed while wallrunning
                wallRunDelay = Time.time + 0.5f;
            }
        }
        //attacking code
        if (comboTime > Time.time)
        {
            x /= 1 + (comboTime - Time.time) * 3;
            z /= 1 + (comboTime - Time.time) * 3;
        }
        if (comboTime < Time.time)
        {
            if (attacking)
            {
                attackCooldown = Time.time + 0.25f;
            }
            attacking = false;
            comboState = 0;
            animator.SetInteger("Attacking", 0);
        }
        if (Input.GetButtonDown("Attack") && attackCooldown < Time.time && !dashing && !wallRunning)
        {
            attacking = true;
            animator.SetInteger("Attacking", comboState + 1);
            if (comboState == 0)
            {
                comboState++;
                comboTime = Time.time + 0.35f;
            }
            else if (comboState == 1)
            {
                comboTime += 0.35f;
                comboState++;
            }
            else if (comboState == 2)
            {
                comboTime += 0.66f;
                attackCooldown = Time.time + 0.75f;
                comboState = 0;
            }
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //========Jumping code============
        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(0.55f,0.1f, 0.55f), Quaternion.Euler(0,45,0), groundMask);
        if (isGrounded)
        {
            lastSafePosition = transform.position;
        }
        if ((isGrounded && velocity.y < 0) || wallRunning)
        {
            velocity.y = -2f;
            doubleJump = true;
        }
        if (airTime > Time.time || dashing)
        {
            velocity.y = -2f;
        }
        if (!wallRunning && !dashing)
        {
            if (Input.GetButtonDown("Jump") && (isGrounded || doubleJump)) //code for jump
            {
                if (isGrounded) //use the first jump
                {
                    isGrounded = false;
                }
                else //use the double jump
                {
                    doubleJump = false;
                }
                airTime = 0f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            if (airTime < Time.time)
            {
                velocity.y += gravity * Time.deltaTime;
                if (velocity.y < gravity) //cap the falling speed
                {
                    velocity.y = gravity;
                }
                controller.Move(velocity * Time.deltaTime);
            }
        }
        else if (!dashing)
        {
            if (Input.GetButtonDown("Jump")) //wall jump
            {
                wallRunning = false;
                wallRunDelay = Time.time + 0.25f;
                airTime = 0f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                velocity += transform.right * (jumpHeight * -5 * Mathf.Sign(transform.InverseTransformPoint(lastWallrun.position).x));
            }
        }
        //reduce the horizontal force from wall jumping over time
        if (Mathf.Abs(velocity.x) > 0)
        {
            velocity.x -= (velocity.x + 2f) * Time.deltaTime * 2 + Mathf.Sign(velocity.x) * 0.1f;
            if (Mathf.Abs(velocity.x) < 1)
            {
                velocity.x = 0;
            }
        }
        if (Mathf.Abs(velocity.z) > 0)
        {
            velocity.z -= (velocity.z + 2f) * Time.deltaTime * 2 + Mathf.Sign(velocity.z) * 0.1f;
            if (Mathf.Abs(velocity.z) < 1)
            {
                velocity.z = 0;
            }
        }
    }

    void LateUpdate()
    {
        if (dashTarget != null)
        {
            Vector3 screenPosition = mainCam.WorldToScreenPoint(dashTarget.transform.position);
            dashReticle.transform.position = new Vector3(screenPosition.x, screenPosition.y, 0);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Attack"))
        {
            return;
        }
        if (collider.CompareTag("Wallrun") && !wallRunning && wallRunDelay < Time.time)
        {
            if (lastWallrun != null)
            {
                if (collider.gameObject == lastWallrun.gameObject && wallRunDelay + 0.75f > Time.time)
                {
                    return; //if the player has used the same surface to wallrun in the last second, do not wallrun
                }
            }
            //determine which angle to wallrun at
            if (Mathf.Abs(gameObject.transform.rotation.eulerAngles.y - collider.transform.rotation.eulerAngles.y) < 90 || Mathf.Abs(gameObject.transform.rotation.eulerAngles.y - collider.transform.rotation.eulerAngles.y) > 270)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, collider.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, collider.transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);
            }
            wallRunning = true;
            wallRunDelay = Time.time + 0.5f;
            lastWallrun = collider.transform;
            velocity.x = 0;
            velocity.z = 0;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Wallrun") && wallRunning)
        {
            wallRunning = false;
            wallRunDelay = Time.time + 0.25f;
        }
    }

    GameObject FindDashTarget()
    {
        GameObject potentialTarget = null;
        bool currentTargetOnScreen = false;
        List<DashTarget> targetsOnScreen = new List<DashTarget>();
        DashTarget[] allTargets = GameObject.FindObjectsOfType<DashTarget>();
        if (allTargets.Length == 0)
        {
            return null;
        }
        foreach (DashTarget target in allTargets)
        {
            Vector3 screenPoint = mainCam.WorldToViewportPoint(target.transform.position);
            if (screenPoint.x > 0.3f && screenPoint.x < 0.7f && screenPoint.y > 0.35f && screenPoint.y < 0.65f && screenPoint.z > 0 && target.cooldown < Time.time)
            {
                targetsOnScreen.Add(target);
            }
            else if (target == dashTarget && screenPoint.x > 0.25f && screenPoint.x < 0.75f && screenPoint.y > 0.3f && screenPoint.y < 0.7f && screenPoint.z > 0 && target.cooldown < Time.time)
            {
                targetsOnScreen.Add(target);
            }
        }
        if (allTargets.Length == 0)
        {
            return null;
        }
        float bestDistance = 25f;
        foreach (DashTarget target in targetsOnScreen)
        {
            if (target == dashTarget)
            {
                currentTargetOnScreen = true;
            }
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > target.stopRadius * 2f && distance < 25f)
            {
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    potentialTarget = target.gameObject;
                }
            }
        }
        if (currentTargetOnScreen && potentialTarget != dashTarget)
        {
            if (Vector3.Distance(transform.position,potentialTarget.transform.position) > Vector3.Distance(transform.position, dashTarget.transform.position) - 2)
            {
                potentialTarget = dashTarget;
            }
        }
        if (potentialTarget != dashTarget)
        {
            dashReticle.color = new Color(1, 1, 1, 0);
            StopAllCoroutines();
            if (potentialTarget != null)
            {
                StartCoroutine(FadeInReticle());
            }
        }
        return potentialTarget;
    }

    IEnumerator FadeInReticle()
    {
        for (int i = 1; i < 21; i++)
        {
            dashReticle.color = new Color(1, 1, 1, i * 0.05f);
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}