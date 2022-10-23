using UnityEngine;

namespace IsakUtils
{
    public static class Vector2Extensions
    {
        public static Vector2 Rotate(this Vector2 origin, float addAngleDeg)
        {
            float originalAngle = Vector2.SignedAngle(Vector2.right, origin);
            if (originalAngle < 0)
            {
                originalAngle = 360 + originalAngle;
            }
            float newAngle = (originalAngle + addAngleDeg) % 360;
            newAngle = Mathf.Deg2Rad * newAngle;
            return new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
        }
    }
}
