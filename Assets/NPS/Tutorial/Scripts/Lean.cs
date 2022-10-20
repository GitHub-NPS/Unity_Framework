using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS
{
    namespace Tutorial
    {
        public class Lean : MonoBehaviour
        {
            public static Lean S;

            [SerializeField] private LeanFingerDown lean;

#if UNITY_EDITOR
            private void OnValidate()
            {
                lean = GetComponent<LeanFingerDown>();
            }
#endif

            private void Awake()
            {
                if (!S) S = this;
            }

            private void Start()
            {

            }

            public void Enable(bool value = true)
            {
                lean.IgnoreStartedOverGui = value;
            }
        }
    }
}