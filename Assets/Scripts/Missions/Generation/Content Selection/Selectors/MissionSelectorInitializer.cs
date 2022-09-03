using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Data;

namespace SpaceMage.Missions
{
    public class MissionSelectorInitializer : MissionContentSelectorCog
    {
        private Transform transform;

        public MissionSelectorInitializer(Transform transform, MissionPossibleContent possibleContent) 
        { 
            this.transform = transform;
            _possibleContent = possibleContent; 
        }

        public void Execute()
        {
            GameObject go = GameObject.Instantiate(PrefabManager.MissionContentPrefab, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(transform);

            MissionContent content = go.GetComponent<MissionContent>();
            MissionManager.SetCurrentMissionContent(content);
            MissionManager.CurrentMission.SetMissionContent(content);

            content.SetMissionType(_possibleContent.MissionType);
            content.SetMapSize(_possibleContent.MapSize);
            content.SetMaxThreatLevel(_possibleContent.MaxThreatLevel);
            content.SetExpectedThreatLevel(_possibleContent.ExpectedThreatLevel);
            content.SetMinThreatLevel(_possibleContent.MinThreatLevel);
            content.SetMaxRarity(_possibleContent.MaxRarity);
            content.SetExpectedRarity(_possibleContent.ExpectedRarity);
            content.SetMinRarity(_possibleContent.MinRarity);

            _content = content;
        }
    }
}