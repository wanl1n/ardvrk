using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    List<ARAgent> agents;

    [SerializeField]
    private GameObject surface;

    //public static bool isTrackedImageFound = false;

    // Start is called before the first frame update
    void Start()
    {
        agents = new List<ARAgent>(GetComponentsInChildren<ARAgent>());
    }

    // Update is called once per frame
    void Update()
    {
        if (ARNavigationManager.Instance.HasLevelPlaced)
        {
            Debug.Log("AgentManager: Level Has Been Placed.");
            Ray r = new Ray(Camera.main.transform.position, (ARNavigationManager.Instance.GetBeaconPosition() - Camera.main.transform.position).normalized);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit))
            {
                Debug.Log("AgentManager: Checking Raycast.");
                if (hit.collider.CompareTag("Plane"))
                    MoveAllAgents(hit.point);
            }
        }

        if (!ARNavigationManager.Instance.IsBeaconVisible)
        {
            this.StopAllAgents();
        }
    }

    public void MoveAllAgents(Vector3 position)
    {
        if (!this.surface.activeSelf)
            this.surface.SetActive(true);

        Debug.Log("AgentManager: Move All Agents.");
        foreach (ARAgent agent in agents)
            agent.MoveAgent(position);
    }

    public void StopAllAgents()
    {
        foreach (ARAgent agent in agents)
            agent.StopAgent();
    }
}
