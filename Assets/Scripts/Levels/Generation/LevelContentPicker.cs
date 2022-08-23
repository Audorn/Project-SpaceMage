using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Data;

namespace SpaceMage.LevelGeneration
{
    [RequireComponent(typeof(LevelGenerator))]
    public class LevelContentPicker : MonoBehaviour
    {
        // Singleton allowing access to the level content picker through the static class.
        private static LevelContentPicker _;
        public static LevelContentPicker Singleton { get { return _; } }
        private void Awake() 
        { 
            _ = this;
            levelGenerator = GetComponent<LevelGenerator>();
        }
        private void OnDestroy() { _ = null; }

        private LevelGenerator levelGenerator;
        [SerializeField] private MissionGenerationData missionGenerationData;

        private void initialize()
        {
            LevelContentPickerStateHandler.RegisterActiveContentPicker(this);
            StartCoroutine(_initialize());
        }
        private IEnumerator _initialize()
        {
            // Do work here.
            Debug.Log("Content picker working...");
            missionGenerationData = MissionManager.GetSelectedMissionGenerationData();

            yield return new WaitForFixedUpdate();
            LevelContentPickerStateHandler.DeRegisterActiveContentPicker(this);
        }

        private void pickPrimaryModifiers()
        {
            LevelContentPickerStateHandler.RegisterActiveContentPicker(this);
            StartCoroutine(_pickPrimaryModifier());
        }
        private IEnumerator _pickPrimaryModifier()
        {
            // Do work here.
            Debug.Log("Content picker working...");
            levelGenerator.SetPrimaryModifiers(null);

            yield return new WaitForFixedUpdate();
            LevelContentPickerStateHandler.DeRegisterActiveContentPicker(this);
        }

        private void pickSecondaryModifiers()
        {
            LevelContentPickerStateHandler.RegisterActiveContentPicker(this);
            StartCoroutine(_pickSecondaryModifiers());
        }
        private IEnumerator _pickSecondaryModifiers()
        {
            // Do work here.
            Debug.Log("Content picker working...");
            levelGenerator.SetSecondaryModifiers(null);

            yield return new WaitForFixedUpdate();
            LevelContentPickerStateHandler.DeRegisterActiveContentPicker(this);
        }

        private void pickSetpieces()
        {
            LevelContentPickerStateHandler.RegisterActiveContentPicker(this);
            StartCoroutine(_pickSetpieces());
        }
        private IEnumerator _pickSetpieces()
        {
            // Do work here.
            Debug.Log("Content picker working...");
            levelGenerator.SetSetpieces(null);

            yield return new WaitForFixedUpdate();
            LevelContentPickerStateHandler.DeRegisterActiveContentPicker(this);
        }

        private void pickHazards()
        {
            LevelContentPickerStateHandler.RegisterActiveContentPicker(this);
            StartCoroutine(_pickHazards());
        }
        private IEnumerator _pickHazards()
        {
            // Do work here.
            Debug.Log("Content picker working...");
            levelGenerator.SetHazards(null);

            yield return new WaitForFixedUpdate();
            LevelContentPickerStateHandler.DeRegisterActiveContentPicker(this);
        }

        private void pickEnemies()
        {
            LevelContentPickerStateHandler.RegisterActiveContentPicker(this);
            StartCoroutine(_pickEnemies());
        }
        private IEnumerator _pickEnemies()
        {
            // Do work here.
            Debug.Log("Content picker working...");

            yield return new WaitForFixedUpdate();
            LevelContentPickerStateHandler.DeRegisterActiveContentPicker(this);
        }

        private void pickObjectives()
        {
            LevelContentPickerStateHandler.RegisterActiveContentPicker(this);
            StartCoroutine(_pickObjectives());
        }
        private IEnumerator _pickObjectives()
        {
            // Do work here.
            Debug.Log("Content picker working...");
            levelGenerator.SetObjectives(null);

            yield return new WaitForFixedUpdate();
            LevelContentPickerStateHandler.DeRegisterActiveContentPicker(this);
        }

        private void Start()
        {
            LevelContentPickerStateHandler.Singleton.Initializing.AddListener(initialize);
            LevelContentPickerStateHandler.Singleton.PickingPrimaryModifiers.AddListener(pickPrimaryModifiers);
            LevelContentPickerStateHandler.Singleton.PickingSecondaryModifiers.AddListener(pickSecondaryModifiers);
            LevelContentPickerStateHandler.Singleton.PickingSetpieces.AddListener(pickSetpieces);
            LevelContentPickerStateHandler.Singleton.PickingHazards.AddListener(pickHazards);
            LevelContentPickerStateHandler.Singleton.PickingEnemies.AddListener(pickEnemies);
            LevelContentPickerStateHandler.Singleton.PickingObjectives.AddListener(pickObjectives);
        }
    }
}