namespace Ilumisoft.BubblePop
{
    using UnityEngine;

    public class MouseRaycast<T> where T : MonoBehaviour
    {
        Camera camera;

        public MouseRaycast(Camera camera)
        {
            this.camera = camera;
        }

        public bool Perform(out T hit)
        {
            hit = null;

            var hitObject = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hitObject.collider != null)
            {
                hit = hitObject.transform.GetComponentInParent<T>();

                if (hit != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}