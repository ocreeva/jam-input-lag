using System;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Moyba.Game.UI
{
    [RequireComponent(typeof(Image))]
    public class StartUI_DifficultyButton : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private Difficulty _difficulty;
        [SerializeField] private Color _selectedColor = Color.green;
        [SerializeField] private Color _unselectedColor = Color.white;

        [NonSerialized] private Image _image;

        public void SetDifficulty()
        => Omnibus.Game.Difficulty.Value = _difficulty;

        private void HandleDifficultyChanged(Difficulty difficulty)
        {
            _image.color = difficulty == _difficulty ? _selectedColor : _unselectedColor;
        }

        private void Awake()
        {
            _image = this.GetComponent<Image>();
        }

        private void OnDisable()
        {
            Omnibus.Game.Difficulty.OnChanged -= this.HandleDifficultyChanged;
        }

        private void OnEnable()
        {
            Omnibus.Game.Difficulty.OnChanged += this.HandleDifficultyChanged;
        }

        private void Start()
        {
            this.HandleDifficultyChanged(Omnibus.Game.Difficulty.Value);
        }
    }
}
