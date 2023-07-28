// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-7-28
// ------------------------------

using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 范围检测
    /// </summary>
    public static class RangeCheckUtil
    {
        /// <summary>
        /// 扇形包含
        /// </summary>
        /// <param name="originDirection">原点方向(原点朝向)</param>
        /// <param name="originPosition">原点位置</param>
        /// <param name="targetPosition">目标位置</param>
        /// <param name="minDistance">扇形范围距离原点的最小距离</param>
        /// <param name="maxDistance">扇形范围距离原点的最大距离</param>
        /// <param name="maxAngle">扇形范围的最大角度</param>
        /// <returns>目标位置是否在原点为扇形的范围内</returns>
        public static bool SectorContains(Vector3 originDirection, Vector3 originPosition, Vector3 targetPosition, float minDistance, float maxDistance, float maxAngle)
        {
            if (Vector3.Angle(targetPosition - originPosition, originDirection) > maxAngle * 0.5f)
            {
                return false;
            }
            float distance = Vector3.Distance(targetPosition, originPosition);
            return minDistance <= distance && distance <= maxDistance;
        }

        /// <summary>
        /// 扇形包含
        /// </summary>
        /// <param name="originDirection">原点方向(原点朝向)</param>
        /// <param name="originPosition">原点位置</param>
        /// <param name="targetPosition">目标位置</param>
        /// <param name="maxDistance">扇形范围距离原点的最大距离</param>
        /// <param name="maxAngle">扇形范围的最大角度</param>
        /// <returns>目标位置是否在原点为扇形的范围内</returns>
        public static bool SectorContains(Vector3 originDirection, Vector3 originPosition, Vector3 targetPosition, float maxDistance, float maxAngle)
        {
            return SectorContains(originDirection, originPosition, targetPosition, 0, maxDistance, maxAngle);
        }

        /// <summary>
        /// 圆形包含
        /// </summary>
        /// <param name="originPosition">原点位置</param>
        /// <param name="targetPosition">目标位置</param>
        /// <param name="minDistance">圆形范围距离原点的最小距离</param>
        /// <param name="maxDistance">圆形范围距离原点的最大距离</param>
        /// <returns>目标位置是否在原点为圆形的范围内</returns>
        public static bool CircleContains(Vector3 originPosition, Vector3 targetPosition, float minDistance, float maxDistance)
        {
            float distance = Vector3.Distance(targetPosition, originPosition);
            return minDistance <= distance && distance <= maxDistance;
        }

        /// <summary>
        /// 圆形包含
        /// </summary>
        /// <param name="originPosition">原点位置</param>
        /// <param name="targetPosition">目标位置</param>
        /// <param name="maxDistance">圆形范围距离原点的最大距离</param>
        /// <returns>目标位置是否在原点为圆形的范围内</returns>
        public static bool CircleContains(Vector3 originPosition, Vector3 targetPosition, float maxDistance)
        {
            return CircleContains(originPosition, targetPosition, 0, maxDistance);
        }

        /// <summary>
        /// 矩形包含
        /// </summary>
        /// <param name="originDirection">原点方向(原点朝向)</param>
        /// <param name="originPosition">原点位置</param>
        /// <param name="targetPosition">目标位置</param>
        /// <param name="distance">矩形距原点的距离</param>
        /// <param name="squareWidth">矩形宽度</param>
        /// <param name="squareHeight">矩形高度</param>
        /// <returns>目标位置是否在原点为矩形的范围内</returns>
        public static bool SquareContains(Vector3 originDirection, Vector3 originPosition, Vector3 targetPosition, float distance, float squareWidth, float squareHeight)
        {
            float angle = Vector3.Angle(targetPosition - originPosition, originDirection);
            if (angle > 90)
            {
                return false;
            }
            float targetDistance = Vector3.Distance(targetPosition, originPosition);
            float z = targetDistance * Mathf.Cos(angle * Mathf.Deg2Rad);
            float x = targetDistance * Mathf.Sin(angle * Mathf.Deg2Rad);
            return distance <= z && z <= distance + squareHeight && x <= squareWidth * 0.5f;
        }

        /// <summary>
        /// 矩形包含
        /// </summary>
        /// <param name="originDirection">原点方向(原点朝向)</param>
        /// <param name="originPosition">原点位置</param>
        /// <param name="targetPosition">目标位置</param>
        /// <param name="squareWidth">矩形宽度</param>
        /// <param name="squareHeight">矩形高度</param>
        /// <returns>目标位置是否在原点为矩形的范围内</returns>
        public static bool SquareContains(Vector3 originDirection, Vector3 originPosition, Vector3 targetPosition, float squareWidth, float squareHeight)
        {
            return SquareContains(originDirection, originPosition, targetPosition, 0, squareWidth, squareHeight);
        }

        /// <summary>
        /// 等腰三角形包含
        /// </summary>
        /// <param name="originDirection">原点方向(原点朝向)</param>
        /// <param name="originPosition">原点位置</param>
        /// <param name="targetPosition">目标位置</param>
        /// <param name="sideLength">等腰三角形的腰边长</param>
        /// <param name="maxAngle">等腰三角形的最大角度</param>
        /// <returns>目标位置是否在原点为等腰三角形的范围内</returns>
        public static bool IsoscelesTriangleContains(Vector3 originDirection, Vector3 originPosition, Vector3 targetPosition, float sideLength, float maxAngle)
        {
            float angle = Vector3.Angle(targetPosition - originPosition, originDirection);
            if (angle > maxAngle * 0.5f)
            {
                return false;
            }
            float angleDistance = sideLength * Mathf.Cos(maxAngle * 0.5f * Mathf.Deg2Rad) / Mathf.Cos(angle * Mathf.Deg2Rad);
            return Vector3.Distance(targetPosition, originPosition) <= angleDistance;
        }
    }
}