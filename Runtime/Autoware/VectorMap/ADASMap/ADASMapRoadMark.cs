﻿#region License
/******************************************************************************
* Copyright 2018-2020 The AutoCore Authors. All Rights Reserved.
* 
* Licensed under the GNU Lesser General Public License, Version 3.0 (the "License"); 
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
* 
* https://www.gnu.org/licenses/lgpl-3.0.html
* 
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*****************************************************************************/
#endregion


using System.Linq;

namespace AutoCore.MapToolbox.Autoware
{
    class ADASMapRoadMark : ADASMapElement<ADASMapRoadMark>
    {
        public int AID { get; set; }
        public enum Type : int
        {
            MARK = 1,
            ARROW = 2,
            CHARACTER = 3,
            SIGN = 4
        }
        public Type RoadMarkType { get; set; }
        public int LinkID { get; set; }
        ADASMapArea area;
        public ADASMapArea Area
        {
            set => area = value;
            get
            {
                if (area == null && ADASMapArea.Dic.TryGetValue(AID, out ADASMapArea value))
                {
                    area = value;
                }
                return area;
            }
        }
        ADASMapLane linkLane;
        public ADASMapLane LinkLane
        {
            set => linkLane = value;
            get
            {
                if (linkLane == null && ADASMapLane.Dic.TryGetValue(LinkID, out ADASMapLane value))
                {
                    linkLane = value;
                }
                return linkLane;
            }
        }
        public override string ToString() => $"{ID},{(Area != null ? Area.ID : 0)},{(int)RoadMarkType},{(LinkLane != null ? LinkLane.ID : 0)}";
        const string file = "road_surface_mark.csv";
        const string header = "ID,AID,Type,LinkID";
        public static void ReadCsv(string path)
        {
            Reset();
            var data = Utils.ReadLinesExcludeFirstLine(path, file);
            if (data != null && data.Count() > 0)
            {
                foreach (var item in data.Split(','))
                {
                    new ADASMapRoadMark
                    {
                        ID = int.Parse(item[0]),
                        AID = int.Parse(item[1]),
                        RoadMarkType = (Type)int.Parse(item[2]),
                        LinkID = int.Parse(item[3])
                    };
                }
            }
        }
        public static void WriteCsv(string path)
        {
            ReIndex();
            UpdateLinkLane();
            Utils.CleanOrCreateNew(path, file, header);
            Utils.AppendData(path, file, List.Select(_ => _.ToString()));
            Utils.RemoveEmpty(path, file);
        }

        private static void UpdateLinkLane()
        {
            if (ADASMapLane.List.Count > 0)
            {
                List.ForEach(_ => _.LinkLane = ADASMapLane.NearestLane(_.Area.SLine.BPoint.Position));
            }
        }
    }
}