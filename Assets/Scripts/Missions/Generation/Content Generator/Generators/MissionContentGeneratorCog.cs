using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class MissionContentGeneratorCog
    {
        protected static MissionContent _content;
        protected static Mission _mission;

        public Mission Mission { get { return _mission; } }
    }
}