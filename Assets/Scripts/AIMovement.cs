using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {
    [SerializeField] public bool isWrong;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] int maxWanderDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] ParticleSystem explosionPrefab;
    string sharkTag = "Shark";

    void Start() {  
        Vector3 newDestination = WanderPoint(transform.position, maxWanderDistance, -1);
        agent.SetDestination(newDestination);
    }

    void Update() {
        if (agent.isActiveAndEnabled && !agent.pathPending && agent.remainingDistance <= 0.5f) {
            Vector3 newDestination = WanderPoint(transform.position, maxWanderDistance, -1);
            agent.SetDestination(newDestination);
        }
    }

    Vector3 WanderPoint (Vector3 origin, float distance, int layermask) {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.layer != groundMask && other.gameObject.tag == sharkTag) {
            Debug.Log("Hit this: " + gameObject.name);
            Result();
        }
    }

    void Result() {
        Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        if (isWrong) {
            MainMenu.Instance.Pause();
            MainMenu.Instance.loseScreen.gameObject.SetActive(true);
        }else {
            Movement.currFishEaten += 1;
            if (Movement.currFishEaten >= Movement.goalFishesEaten) {
                MainMenu.Instance.Pause();
                MainMenu.Instance.winScreen.gameObject.SetActive(true);
            }
        }
        Destroy(gameObject);
    }
}
