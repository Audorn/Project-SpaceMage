using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class MissionList : MonoBehaviour
    {
        [SerializeField] private List<Mission> missions;
        [SerializeField] private Mission currentMission;

        public List<Mission> Missions { get { return missions; } }
        public Mission CurrentMission { get { return currentMission; } }

        public void RegisterCurrentMission(Mission mission) 
        {
            missions.Add(mission);
            currentMission = mission; 
        }
    }
}