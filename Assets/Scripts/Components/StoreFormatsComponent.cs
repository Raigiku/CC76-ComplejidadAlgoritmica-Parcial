using UnityEngine;
using System.Collections.Generic;

namespace Complejidad.Components
{
    public class StoreFormatsComponent : MonoBehaviour
    {
        public Dictionary<string, Models.Format> Formats { get; set; }
    }
}