using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace TK.Rendering.PostFX
{
    [System.Serializable, VolumeComponentMenu("TK/ReverseColor")]
    public class ReverseColor : VolumeComponent
    {
        public BoolParameter isActivation = new BoolParameter(false);
        public bool IsActive => isActivation.value;
    }
}
