using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Anon : MonoBehaviour
{
    [SerializeField] private Vector2 range;
    [SerializeField] private Vector3 hitPos;
    [SerializeField] private Laptop lapDog;
    [SerializeField] private FadeScreen fadeScreen;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Light lights;
    [SerializeField] private Material mat;
    [SerializeField] private AudioClip jumpscare;
    [SerializeField] private AudioClip[] footsteps;
    [HideInInspector] public bool canAttack;
    [HideInInspector] public SkinnedMeshRenderer[] mesh;
    private NavMeshAgent agent;
    private Animator anim;
    private bool isAttacking;
    private Vector3 startingPos;

    private void Start()
    {
        mesh = GetComponentsInChildren<SkinnedMeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        startingPos = transform.position;
    }

    private void Update()
    {
        if(isAttacking)
        {
            if (Vector3.Distance(agent.transform.position, target.position) <= 1f)
            {
                if (lapDog.isOn)
                {
                    canAttack = true;
                }
                else
                {
                    StartCoroutine(ReturnToVoid());
                }
            }
        }
    }

    private IEnumerator ReturnToVoid()
    {
        isAttacking = false;
        yield return new WaitForSeconds(10f);
        lights.enabled = true;
        mat.EnableKeyword("_EMISSION");
        agent.SetDestination(startingPos);
        yield return new WaitForSeconds(10f);
        foreach (var item in mesh)
        {
            item.enabled = false;
        }
    }

    public IEnumerator KillPlayer()
    {
        agent.enabled = false;
        transform.position = hitPos;
        transform.rotation = Quaternion.Euler(new Vector3(0, 110, 0));
        LeanTween.rotate(playerCamera.gameObject, new Vector3(0, -90, 0), .5f);
        LeanTween.move(playerCamera.gameObject, new Vector3(6.0002f, 3.945f, -2.237f), .5f);
        yield return new WaitForSeconds(.5f);
        AudioManager.instance.PlayAudio(jumpscare, 1f);
        yield return new WaitForSeconds(.125f);
        anim.SetTrigger("Punch");
        yield return new WaitForSeconds(1f);
        fadeScreen.FadeBlack(1, .7f);
        //print("Using default scene Manager");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GAME");
    }

    public IEnumerator RandomAttack()
    {
        float rng = Random.Range(range.x, range.y);
        yield return new WaitForSeconds(rng);
        if(lapDog.isOn == true)
        {
            AttackSequence();
        }
    }

    private void AttackSequence()
    {
        foreach (var item in mesh)
        {
            item.enabled = true;
        }
        agent.SetDestination(target.position);
        isAttacking = true;
        lights.enabled = false;
        mat.DisableKeyword("_EMISSION");
    }

    public void Footstep(int s)
    {
        if(isAttacking && !canAttack)
        AudioManager.instance.PlayAudio(footsteps[s], 1.0f, true, 32, transform.position);
    }
}
