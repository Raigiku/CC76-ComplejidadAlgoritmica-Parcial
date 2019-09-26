using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Complejidad.Components
{
    public class StoreAlgorithmsComponent : MonoBehaviour
    {
        [SerializeField]
        private int activeAlgorithm;

        [SerializeField]
        private TMP_Text activeAlgorithmTxt = null;

        public List<Models.Algorithm> Algorithms { get; set; }

        [SerializeField]
        private TMP_Text usedMemoryTxt;

        [SerializeField]
        private TMP_Text elapsedTimeTxt;

        private void Awake()
        {
            activeAlgorithm = 0;
            Algorithms = new List<Models.Algorithm>();
            Algorithms.Add(new Models.Solution1.Main
            {
                Name = "Binary Tree Packing"
            });
            Algorithms.Add(new Models.Solution2.Main
            {
                Name = "1st Fit Deacreasing Height"
            });
            Algorithms.Add(new Models.Solution3.Main
            {
                Name = "Best Fit Heuristic"
            });
        }

        public void ChangeAlgorithm(int adder)
        {
            if (activeAlgorithm + adder > -1 && activeAlgorithm + adder < Algorithms.Count)
            {
                activeAlgorithm += adder;
                activeAlgorithmTxt.text = Algorithms[activeAlgorithm].Name;
            }
        }

        public void Execute()
        {
            Algorithms[activeAlgorithm].Execute();
            elapsedTimeTxt.text = $"Segundos: {Algorithms[activeAlgorithm].TimeElapsed}";
            usedMemoryTxt.text = $"Memoria: {Algorithms[activeAlgorithm].MemoryUsed} KB";
        }
    }
}