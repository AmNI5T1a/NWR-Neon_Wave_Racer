using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool MoveRight;
        public bool MoveLeft;
        public bool MoveFront;
        public bool MoveBack;

        public bool Brake;
    }
}