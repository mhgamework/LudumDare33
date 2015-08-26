using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Removes the gameobject to root scope on startup by removing its parent
    /// </summary>
    public class RemoveParentScript:MonoBehaviour
    {
        public void Start()
        {
            transform.parent = null;
        }
    }
}