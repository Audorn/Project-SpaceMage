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
        private List<VisualElement> hardpointImages = new List<VisualElement>();
        private List<VisualElement> moduleImages = new List<VisualElement>();
        private List<VisualElement> spellImages = new List<VisualElement>();

        private GroupBox activeTooltipContainer;
        private GroupBox activeTooltip;
        private Label activeToolTipLabel;
        private List<Hardpoint> hardpoints = new List<Hardpoint>();

        //[MenuItem("Window/UI Toolkit/ShipEditor")]
        private void OnEnable()
        {
            playerShip = Player.Ship;

            var uiDocument = GetComponent<UIDocument>();

            hardpointsContainer = uiDocument.rootVisualElement.Q<GroupBox>(name: "Hardpoints-Container");

            // Tooltips.
            activeTooltipContainer = uiDocument.rootVisualElement.Q<GroupBox>(name: "ActiveTooltipContainer");
            activeTooltipContainer.pickingMode = PickingMode.Ignore;
            activeTooltip = new GroupBox();
            activeTooltipContainer.Add(activeTooltip);
            activeTooltip.name = "ActiveTooltip";
            activeToolTipLabel = new Label();
            activeTooltip.Add(activeToolTipLabel);
            activeToolTipLabel.name = "ActiveTooltipLabel";

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
            int numberOfHardpointImages = hardpointImages.Count;
            for (int i = 0; i < numberOfHardpointImages; i++)
            {
                hardpointImages[i].UnregisterCallback<ClickEvent>(clickHardpoint);
                hardpointImages[i].UnregisterCallback<MouseOverEvent>(hoverHardpointOn);
                hardpointImages[i].UnregisterCallback<MouseOutEvent>(hoverHardpointOff);
            }

            int numberOfModuleImages = moduleImages.Count;
            for (int i = 0; i < numberOfModuleImages; i++)
            {
                moduleImages[i].UnregisterCallback<ClickEvent>(clickModule);
                moduleImages[i].UnregisterCallback<MouseOverEvent>(hoverModuleOn);
                moduleImages[i].UnregisterCallback<MouseOutEvent>(hoverModuleOff);
            }

            int numberOfSpellImages = spellImages.Count;
            for (int i = 0; i < numberOfSpellImages; i++)
            {
                spellImages[i].UnregisterCallback<ClickEvent>(clickSpell);
                spellImages[i].UnregisterCallback<MouseOverEvent>(hoverSpellOn);
                spellImages[i].UnregisterCallback<MouseOutEvent>(hoverSpellOff);
            }
        }

        private void populateHardpoints()
        {
            var hardpoints = playerShip.Hardpoints;
            var hardpointNames = playerShip.HardpointNames;

            int numberOfHardpoints = hardpoints.Count;
            for (int i = 0; i < numberOfHardpoints; i++)
            {
                // Hardpoint.
                var hardpointContainer = new GroupBox();
                this.hardpoints.Add(hardpoints[i]);
                createHardpointUI(hardpoints[i], hardpointNames[i], hardpointContainer);

                // Modules.
                var modulesContainer = new GroupBox();
                hardpointContainer.Add(modulesContainer);
                createModulesUI(hardpoints[i], modulesContainer);

                // Spells.
                var spellsContainer = new GroupBox();
                hardpointContainer.Add(spellsContainer);
                createSpellsUI(hardpoints[i], spellsContainer);
            }
        }

        private void createHardpointUI(Hardpoint hardpoint, string hardpointName, GroupBox hardpointContainer)
        {
            hardpointContainer.AddToClassList("hardpoint-container");
            if (hardpoint.IsDestroyed)
                hardpointContainer.AddToClassList("hardpoint-destroyed");

            hardpointsContainer.Add(hardpointContainer); // Add to dom.
            hardpointContainers.Add(hardpointContainer); // Add to list.

            var nameLabel = new Label();
            nameLabel.AddToClassList("hardpoint-name");
            nameLabel.AddToClassList("label-bold");
            nameLabel.text = hardpointName;

            hardpointContainer.Add(nameLabel);

            var hardpointSpriteRenderer = hardpoint.GetComponent<SpriteRenderer>();
            Sprite hardpointSprite = hardpointSpriteRenderer.sprite;

            var hardpointImage = new VisualElement();
            hardpointImage.name = hardpointName;
            hardpointImage.AddToClassList("hardpoint-image");
            hardpointImage.style.backgroundImage = Background.FromSprite(hardpointSprite);

            hardpointContainer.Add(hardpointImage);
            hardpointImage.RegisterCallback<ClickEvent>(clickHardpoint);
            hardpointImage.RegisterCallback<MouseOverEvent>(hoverHardpointOn);
            hardpointImage.RegisterCallback<MouseOutEvent>(hoverHardpointOff);
        }

        private void createModulesUI(Hardpoint hardpoint, GroupBox modulesContainer)
        {
            modulesContainer.AddToClassList("hardpoint-modules-container");

            int numberOfModules = hardpoint.NumberOfModules;
            for (int j = 0; j < numberOfModules; j++)
            {
                var moduleSprite = hardpoint.Modules[j].Sprite;
                var moduleImage = new VisualElement();
                moduleImage.AddToClassList("hardpoint-module");
                moduleImage.name = hardpoint.Modules[j].Id;
                moduleImage.style.backgroundImage = Background.FromSprite(moduleSprite);

                modulesContainer.Add(moduleImage);
                moduleImages.Add(moduleImage);

                moduleImage.RegisterCallback<ClickEvent>(clickModule);
                moduleImage.RegisterCallback<MouseOverEvent>(hoverModuleOn);
                moduleImage.RegisterCallback<MouseOutEvent>(hoverModuleOff);
            }
        }

        private void createSpellsUI(Hardpoint hardpoint, GroupBox spellsContainer)
        {
            spellsContainer.AddToClassList("hardpoint-spells-container");

            int numberOfSpells = hardpoint.NumberOfSpells;
            for (int j = 0; j < numberOfSpells; j++)
            {
                var spellSprite = hardpoint.Spells[j].Sprite;
                var spellImage = new VisualElement();
                spellImage.AddToClassList("hardpoint-spell");
                spellImage.name = hardpoint.Spells[j].Id;
                spellImage.style.backgroundImage = Background.FromSprite(spellSprite);

                spellsContainer.Add(spellImage);
                spellImages.Add(spellImage);
                spellImage.RegisterCallback<ClickEvent>(clickSpell);
                spellImage.RegisterCallback<MouseOverEvent>(hoverSpellOn);
                spellImage.RegisterCallback<MouseOutEvent>(hoverSpellOff);
            }
        }

        // Clicks.
        private void clickHardpoint(ClickEvent clickEvent)
        {
            var hardpointImage = clickEvent.currentTarget as VisualElement;

            Debug.Log($"{hardpointImage.name} (hardpoint image) was clicked!");
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

        // Tooltips ON.
        private void hoverHardpointOn(MouseOverEvent mouseOverEvent)
        {
            var hardpointImage = mouseOverEvent.currentTarget as VisualElement;
            string hardpointName = hardpointImage.name;

            Hardpoint hardpoint = null;
            int numberOfHardpoints = playerShip.HardpointNames.Count;
            for (int i = 0; i < numberOfHardpoints; i++)
            {
                if (playerShip.HardpointNames[i] == hardpointName)
                {
                    hardpoint = playerShip.Hardpoints[i];
                    break;
                }
            }

            if (!hardpoint)
                return;

            activeToolTipLabel.text = hardpoint.Description;
            activeTooltip.style.visibility = Visibility.Visible;
        }

        private void hoverModuleOn(MouseOverEvent mouseOverEvent)
        {
            var moduleImage = mouseOverEvent.currentTarget as VisualElement;

            Debug.Log($"{moduleImage.name} (module image) is hovered!");
        }

        private void hoverSpellOn(MouseOverEvent mouseOverEvent)
        {
            var spellImage = mouseOverEvent.currentTarget as VisualElement;

            Debug.Log($"{spellImage.name} (spell image) is hovered!");
        }

        // Tooltips OFF.
        private void hoverHardpointOff(MouseOutEvent mouseOverEvent)
        {
            //var hardpointImage = mouseOverEvent.currentTarget as VisualElement;
            activeTooltip.style.visibility = Visibility.Hidden;

            //Debug.Log($"{hardpointImage.name} (hardpoint image) is not hovered!");
        }

        private void hoverModuleOff(MouseOutEvent mouseOverEvent)
        {
            var moduleImage = mouseOverEvent.currentTarget as VisualElement;

            Debug.Log($"{moduleImage.name} (module image) is not hovered!");
        }

        private void hoverSpellOff(MouseOutEvent mouseOverEvent)
        {
            var spellImage = mouseOverEvent.currentTarget as VisualElement;

            Debug.Log($"{spellImage.name} (spell image) is not hovered!");
        }

    }
}