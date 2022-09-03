using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Data;

namespace SpaceMage.Missions
{
    public class MissionGeneratorInitializer : MissionContentGeneratorCog
    {
        public MissionGeneratorInitializer(MissionContent content, Mission mission)
        {
            _content = content;
            _mission = mission;
        }

        public void Execute()
        {
            float aspect = (float)Screen.width / Screen.height;
            float screenHeightInUnits = Camera.main.orthographicSize * 2;
            float screenWidthInUnits = screenHeightInUnits * aspect;
            Vector2 baseMapSize = new Vector2(Mathf.Min(screenWidthInUnits, screenHeightInUnits), Mathf.Min(screenWidthInUnits, screenHeightInUnits));
            float x = baseMapSize.x, y = baseMapSize.y;

            // Flags that modify map x,y size and basic form.
            bool hasBigMapModifier = _content.SecondaryModifiers.Contains(SecondaryModifier.BIG_MAP);         // Full-screen.
            bool hasSmallMapModifier = _content.SecondaryModifiers.Contains(SecondaryModifier.SMALL_MAP);     // Mini-screen.
            bool hasSplitMapModifier = _content.SecondaryModifiers.Contains(SecondaryModifier.SPLIT_MAP);     // Two half-screen maps.
            bool hasCorridorModifier = _content.SecondaryModifiers.Contains(SecondaryModifier.CORRIDOR);      // Full width, but narrow.


            if (hasBigMapModifier) { x = screenWidthInUnits; y = screenHeightInUnits; }
            else if (hasSmallMapModifier) { x = baseMapSize.x * 0.65f; y = baseMapSize.y * 0.65f; }
            else if (hasSplitMapModifier) { x = baseMapSize.x * 0.5f; y = baseMapSize.y * 0.7f; } // Two maps.
            else if (hasCorridorModifier) { x = screenWidthInUnits; y = baseMapSize.y * 0.5f; }

            _mission.SetMapSize(new Vector2(x, y));
        }
    }
}