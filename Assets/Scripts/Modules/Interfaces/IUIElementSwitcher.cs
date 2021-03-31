using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public interface IUIElementSwitcher
    {
        public void CloseOrOpenUIElement(GameObject gameObject);
    }
}
