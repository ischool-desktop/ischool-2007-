using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Score;
using SmartSchool.StudentRelated.Placing.Sorter;

namespace SmartSchool.StudentRelated.Placing.Rule
{
    public class UnduplicatePlacingRule : IPlacingRule
    {
        #region IPlacingRule жин√

        public IList<PlacingInfo> GetPlacingInfo(StudentSemesterScoreRecordCollection collection, IScoreDependance dependance)
        {
            ScoreComparer comparer = new SimpleComparer(dependance);
            collection.Sort(comparer);
            List<PlacingInfo> list = new List<PlacingInfo>();
            PlacingInfo lastInfo = null;
            int emptyPlace = 0;
            int place = 1;
            foreach (StudentSemesterScoreRecord record in collection)
            {
                PlacingInfo info = new PlacingInfo();
                info.Record = record;
                info.Score = record.PlacedScore;

                if (lastInfo == null)
                {
                    info.Place = place;
                }
                else
                {
                    if (info.Score.CompareTo(lastInfo.Score) == 0)
                    {
                        info.Place = place;                        
                    }
                    else
                    {
                        place += emptyPlace + 1;
                        info.Place = place;
                        emptyPlace = 0;
                    }
                }
                lastInfo = info;
                list.Add(info);
            }
            return list;
        }
        #endregion
    }
}
