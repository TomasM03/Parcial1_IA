using UnityEngine;

public static class AIMovementHelper
{
    public static Vector3 GetAvoidanceDirection(Transform aiTransform, Vector3 desiredDirection, float avoidDistance = 1.5f, float avoidStrength = 10f)
    {
        Vector3 avoidance = Vector3.zero;
        int hitCount = 0;

        if (Physics.Raycast(aiTransform.position, aiTransform.forward, out RaycastHit frontHit, avoidDistance))
        {
            avoidance += frontHit.normal;
            hitCount++;
        }

        Vector3 leftDir = Quaternion.Euler(0, -45, 0) * aiTransform.forward;
        if (Physics.Raycast(aiTransform.position, leftDir, out RaycastHit leftHit, avoidDistance))
        {
            avoidance += leftHit.normal;
            hitCount++;
        }

        Vector3 rightDir = Quaternion.Euler(0, 45, 0) * aiTransform.forward;
        if (Physics.Raycast(aiTransform.position, rightDir, out RaycastHit rightHit, avoidDistance))
        {
            avoidance += rightHit.normal;
            hitCount++;
        }

        if (hitCount == 0)
            return desiredDirection.normalized;

        Vector3 finalDir = (desiredDirection + avoidance * avoidStrength).normalized;

        finalDir = Vector3.Lerp(aiTransform.forward, finalDir, 0.5f).normalized;

        return finalDir;
    }
}
