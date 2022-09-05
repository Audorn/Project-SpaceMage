using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using SpaceMage.Ships;

namespace SpaceMage.UI {
    public class ShipEditor : MonoBehaviour
    {
        private Ship playerShip;
        private ListView list;

        [MenuItem("Window/UI Toolkit/ShipEditor")]

        private void OnEnable()
        {
            Debug.Log("HERE");
            var uiDocument = GetComponent<UIDocument>();

            list = uiDocument.rootVisualElement.Q<ListView>();

            list.RegisterCallback<ClickEvent>(PrintClickMessage);
        }

        private void OnDisable()
        {
            list.UnregisterCallback<ClickEvent>(PrintClickMessage);
        }

        private void PrintClickMessage(ClickEvent clickEvent)
        {
            var list = clickEvent.currentTarget as ListView;

            Debug.Log($"{list.name} was clicked!");
        }


        //public void CreateGUI()
        //{
        //    // Get the player ship.
        //    if (!playerShip)
        //        playerShip = Player.Ship;

        //    // Each editor window contains a root VisualElement object
        //    VisualElement root = rootVisualElement;

        //    // Import UXML
        //    var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/ShipEditor.uxml");
        //    VisualElement visualTreeFromUXML = visualTree.Instantiate();
        //    root.Add(visualTreeFromUXML);

        //    // A stylesheet can be added to a VisualElement.
        //    // The style will be applied to the VisualElement and all of its children.
        //    var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/ShipEditor.uss");

        //    root.style.maxWidth = 350;
        //    root.style.maxHeight = 400;
        //    Debug.LogError(root.style);

        //    //VisualElement labelWithStyle = new Label("Hello World! With Style");
        //    //labelWithStyle.styleSheets.Add(styleSheet);
        //    //root.Add(labelWithStyle);
        //}
    }
}