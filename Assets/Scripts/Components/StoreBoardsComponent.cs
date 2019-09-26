using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace Complejidad.Components
{
    public class StoreBoardsComponent : MonoBehaviour
    {
        [SerializeField]
        private int activeBoard;

        [SerializeField]
        private TMP_Text activeBoardTxt = null;

        public List<GameObject> Boards { get; set; }

        private void Awake()
        {
            activeBoard = 0;
            Boards = new List<GameObject>();
        }

        public void ChangeBoard(int adder)
        {
            if (activeBoard + adder > -1 && activeBoard + adder < Boards.Count)
            {
                Boards[activeBoard].SetActive(false);
                activeBoard += adder;
                Boards[activeBoard].SetActive(true);
                activeBoardTxt.text = "Plancha: " + (activeBoard + 1).ToString();
            }
        }

        public void Clear()
        {
            if (Boards.Count > 0)
            {
                Boards[activeBoard].SetActive(false);
                activeBoard = 0;
                Boards[activeBoard].SetActive(true);
                activeBoardTxt.text = "Plancha: 1";
                Boards.ForEach(
                    boardGameObject => Destroy(boardGameObject)
                );
                Boards.Clear();
            }
        }
    }
}
