using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{

    public GameObject m_goCamera = null;
    public GameObject m_goBody = null;
    public float m_fTargetingMinAngle = 5f;
    public float m_fAimAssistSpeed = 7f;

    private List<Transform> m_tTargetList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // Please do not use tags to find the targets, this is just a quick hack for a test
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("AimAssistTarget"))
        {
            m_tTargetList.Add(target.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform closestTarget = null;
        float closestTempValue = -1f;
        Vector3 cameraDirection = m_goCamera.transform.forward.normalized;

        // Find the closest target to the cursor
        foreach (Transform target in m_tTargetList)
        {
            Vector3 directionToTemp = (target.position - m_goCamera.transform.position).normalized;
            float dot = Vector3.Dot(directionToTemp, cameraDirection);
            if (dot >= closestTempValue)
            {
                closestTarget = target;
                closestTempValue = dot;
            }
        }

        // Just quit update if none were found
        if (closestTarget == null)
            return;

        Vector3 directionToTarget = (closestTarget.position - m_goCamera.transform.position).normalized;
        if (Vector3.Angle(directionToTarget, cameraDirection) <= m_fTargetingMinAngle)
        {
            // Calculate final rotation towards the target
            Quaternion newRot = Quaternion.Lerp(m_goCamera.transform.rotation, Quaternion.LookRotation(directionToTarget, Vector3.up), m_fAimAssistSpeed * Time.deltaTime);

            // Rotate camera only on X axis
            m_goCamera.transform.rotation = newRot;

            Vector3 cameraEuler = m_goCamera.transform.localEulerAngles;
            cameraEuler.y = 0f;
            cameraEuler.z = 0f;
            m_goCamera.transform.localEulerAngles = cameraEuler;

            // Rotate body only on Y axis
            m_goBody.transform.rotation = newRot;

            Vector3 bodyEuler = m_goBody.transform.localEulerAngles;
            bodyEuler.x = 0f;
            bodyEuler.z = 0f;
            m_goBody.transform.localEulerAngles = bodyEuler;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + m_goCamera.transform.right);
        Gizmos.DrawLine(transform.position, transform.position + m_goBody.transform.up);
    }
}
