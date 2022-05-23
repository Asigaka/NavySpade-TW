using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerAnimations anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            MoveToTouch(Input.GetTouch(0).position);
        }

        anim.SetVelocityParametre(agent.desiredVelocity.magnitude);
    }

    private void MoveToTouch(Vector3 movePos)
    {
        Ray ray = cam.ScreenPointToRay(movePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50000, groundLayer))
        {
            agent.SetDestination(hit.point);
        }
    }
}
