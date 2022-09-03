using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class MissionContentSelectorCog
    {
        protected static MissionPossibleContent _possibleContent;
        protected static MissionContent _content;

        public MissionContent MissionContent { get { return _content; } }

        protected int calculateQuantityToSelect(List<float> percents, List<int> quantities)
        {
            // Early out - Percents or quantities not provided.
            if (percents.Count == 0 || quantities.Count == 0)
                return 1;

            float percent = Random.Range(0f, 1f);
            int index = 0;
            for (int i = 0; i < percents.Count; i++)
            {
                // Early out - this percent is too high for this row.
                if (percent > percents[i])
                    break;

                // Early out - quantities doesn't have a corresponding row.
                if (quantities.Count <= index)
                    break;

                index = i;
            }

            return quantities[index];
        }

        protected List<int> selectOptionsFromEnumAsInts(int quantity, List<int> guaranteed, List<int> likely, List<int> blocked, List<int> all)
        {
            var selected = new List<int>();

            if (guaranteed.Count > 0)
                selected.AddRange(guaranteed);

            // Early out - Only enough room for the guaranteed options.
            if (selected.Count >= quantity)
                return selected;

            selected = selectOptionsFromLimitedList(selected, likely, quantity);

            // Early out - Only enough room for guaranteed and likely options.
            if (selected.Count >= quantity)
                return selected;

            all = all.Except(selected).Except(guaranteed).Except(likely).Except(blocked).ToList();
            selected = selectOptionsFromLimitedList(selected, all, quantity);

            return selected;
        }

        private List<GameObject> selectOptionsFromPrefabs(int quantity, List<GameObject> guaranteed, List<GameObject> likely, List<GameObject> blocked, List<GameObject> all)
        {
            var selected = new List<GameObject>();

            if (guaranteed.Count > 0)
                selected.AddRange(guaranteed);

            // Early out - Only enough room for the guaranteed options.
            if (selected.Count >= quantity)
                return selected;

            selected = selectOptionsFromLimitedList(selected, likely, quantity);

            // Early out - Only enough room for guaranteed and likely options.
            if (selected.Count >= quantity)
                return selected;

            all = all.Except(selected).Except(guaranteed).Except(likely).Except(blocked).ToList();
            selected = selectOptionsFromLimitedList(selected, all, quantity);

            return selected;
        }

        private List<int> selectOptionsFromLimitedList(List<int> selected, List<int> list, int quantity)
        {
            if (list.Count > 0)
            {
                while (selected.Count < quantity && list.Count > 0)
                {
                    int index = Random.Range(0, list.Count);
                    selected.Add(list[index]);
                    list.RemoveAt(index);
                }
            }

            return selected;
        }
        private List<GameObject> selectOptionsFromLimitedList(List<GameObject> selected, List<GameObject> list, int quantity)
        {
            if (list.Count > 0)
            {
                while (selected.Count < quantity && list.Count > 0)
                {
                    int index = Random.Range(0, list.Count);
                    selected.Add(list[index]);
                    list.RemoveAt(index);
                }
            }

            return selected;
        }
    }
}