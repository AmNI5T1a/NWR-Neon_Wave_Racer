using System.Collections;
using UnityEngine;

namespace NWR.Modules
{
    public interface IAppearAnimation
    {
        IEnumerator AppearAnimation(GameObject obj);
    }
}
