using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using SpaceMage.Ships;

namespace SpaceMage.UI {
    public class ShipEditor : MonoBehaviour
    {
        private Ship playerShip;
        private GroupBox hardpointsContainer;
        private List<GroupBox> hardpointContainers = new List<GroupBox>();
        private List<VisualElement> moduleImages = new List<VisualElement>();
        private List<VisualElement> spellImages = new List<VisualElement>();

        //[MenuItem("Window/UI Toolkit/ShipEditor")]
        private void OnEnable()
        {
            playerShip = Player.Ship;

            var uiDocument = GetComponent<UIDocument>();

            hardpointsContainer = uiDocument.rootVisualElement.Q<GroupBox>(name: "Hardpoints-Container");

            var modelLabel = uiDocument.rootVisualElement.Q<Label>(name: "Model");
            var hullStrengthLabel = uiDocument.rootVisualElement.Q<Label>(name: "HullStrength");
            var maneuverabilityLabel = uiDocument.rootVisualElement.Q<Label>(name: "Maneuverability");
            var systemsContainer = uiDocument.rootVisualElement.Q<VisualElement>(name: "SystemsContainer");
            var shipImage = uiDocument.rootVisualElement.Q<VisualElement>(name: "ShipImage"); // TODO: Replace with a gameobject copy of ship.

            modelLabel.text = playerShip.Model;
            hullStrengthLabel.text = playerShip.HullStrength.ToString() + " (" + ((int)playerShip.HullStrength + 1) + " impacts)";
            maneuverabilityLabel.text = playerShip.UIManeuverability;

            var systemsLabel = new Label();
            systemsLabel.AddToClassList("text-white");
            systemsLabel.text = "None";
            systemsContainer.Add(systemsLabel);

            var shipSprite = playerShip.GetComponent<SpriteRenderer>().sprite;
            shipImage.style.backgroundImage = Background.FromSprite(shipSprite);

            populateHardpoints();
        }

        private void OnDisable()
        {
            int numberOfHardpointContainers = hardpointContainers.Count;
            for (int i = 0; i < numberOfHardpointContainers; i++)
                hardpointContainers[i].UnregisterCallback<ClickEvent>(clickHardpoint);

            int numberOfModuleImages = moduleImages.Count;
            for (int i = 0; i < numberOfModuleImages; i++)
                moduleImages[i].UnregisterCallback<ClickEvent>(clickModule);

            int numberOfSpellImages = spellImages.Count;
            for (int i = 0; i < numberOfSpellImages; i++)
                spellImages[i].UnregisterCallback<ClickEvent>(clickSpell);
        }

        private void populateHardpoints()
        {
            var hardpoints = playerShip.Hardpoints;
            var hardpointNames = playerShip.HardpointNames;

            int numberOfHardpoints = hardpoints.Count;
            for (int i = 0; i < numberOfHardpoints; i++)
            {
                // Hardpoint Container.
                var hardpointContainer = new GroupBox();
                hardpointContainer.AddToClassList("hardpoint-container");
                if (hardpoints[i].IsDestroyed)
                    hardpointContainer.AddToClassList("hardpoint-destroyed");

                hardpointsContainer.Add(hardpointContainer); // Add to dom.
                hardpointContainers.Add(hardpointContainer); // Add to list.
                hardpointContainer.RegisterCallback<ClickEvent>(clickHardpoint);

                // Hardpoint Label.
                var nameLabel = new Label();
                nameLabel.AddToClassList("hardpoint-name");
                nameLabel.AddToClassList("label-bold");
                nameLabel.text = hardpointNames[i];

                hardpointContainer.Add(nameLabel);

                // Hardpoint Image.
                var hardpointSpriteRenderer = hardpoints[i].GetComponent<SpriteRenderer>();
                Sprite hardpointSprite = hardpointSpriteRenderer.sprite;

                var hardpointImage = new VisualElement();
                hardpointImage.AddToClassList("hardpoint-image");
                hardpointImage.style.backgroundImage = Background.FromSprite(hardpointSprite);

                hardpointContainer.Add(hardpointImage);

                // Modules Container.
                var modulesContainer = new GroupBox();
                modulesContainer.AddToClassList("hardpoint-modules-container");

                hardpointContainer.Add(modulesContainer);

                // Modules.
                int numberOfModules = hardpoints[i].NumberOfModules;
                for (int j = 0; j < numberOfModules; j++)
                {
                    var moduleSprite = hardpoints[i].Modules[j].Sprite;
                    var moduleImage = new VisualElement();
                    moduleImage.AddToClassList("hardpoint-module");
                    moduleImage.style.backgroundImage = Background.FromSprite(moduleSprite);

                    modulesContainer.Add(moduleImage);
                    moduleImages.Add(moduleImage);
                    moduleImage.RegisterCallback<ClickEvent>(clickModule);
                }

                // Spells Container.
                var spellsContainer = new GroupBox();
                spellsContainer.AddToClassList("hardpoint-spells-container");

                hardpointContainer.Add(spellsContainer);

                // Spells.
                int numberOfSpells = hardpoints[i].NumberOfSpells;
                for (int j = 0; j < numberOfSpells; j++)
                {
                    var spellSprite = hardpoints[i].Spells[j].Sprite;
                    var spellImage = new VisualElement();
                    spellImage.AddToClassList("hardpoint-spell");
                    spellImage.style.backgroundImage = Background.FromSprite(spellSprite);

                    spellsContainer.Add(spellImage);
                    spellImages.Add(spellImage);
                    spellImage.RegisterCallback<ClickEvent>(clickSpell);
                }
            }
        }

        private void clickHardpoint(ClickEvent clickEvent)
        {
            var hardpointContainer = clickEvent.currentTarget as GroupBox;

            Debug.Log($"{hardpointContainer.name} (hardpoint) was clicked!");
        }

        private void clickModule(ClickEvent clickEvent)
        {
            var moduleImage = clickEvent.currentTarget as VisualElement;

            Debug.Log($"{moduleImage.name} (module) was clicked!");
        }

        private void clickSpell(ClickEvent clickEvent)
        {
            var spellImage = clickEvent.currentTarget as VisualElement;

            Debug.Log($"{spellImage.name} (spell) was clicked!");
        }

    }
}