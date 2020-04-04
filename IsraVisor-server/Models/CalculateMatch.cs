using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class CalculateMatch
    {
        public int Id1 { get; set; }
        public int Id2 { get; set; }
        public double Percents { get; set; }
        double AgeMax = 5;
        double LanguagesMax = 30;
        double HobbiesMax = 25;
        double ExpertisesMax = 40;

        public List<CalculateMatch> CalculateMatchBetweenTouristToAllGuides(int id)
        {
            Match m = new Match();
            m = m.GetTouristMatchDetailsByID(id);
            List<Match> AllGuides= m.GetGuidesDetails();
            return Calculate(m, AllGuides);
        }

        public List<CalculateMatch> CalculateMatchBetweenGuideToAllGuides(int id)
        {
            Match m = new Match();
            List<Match> AllGuides = m.GetGuidesDetails();
            m = m.GetGuideMatchDetailsByID(id);
            return Calculate(m, AllGuides);
        }

        public List<CalculateMatch> CalculateMatchBetweenTouristToAllTourists(int id)
        {
            Match m = new Match();
            List<Match> AllTourists = m.GetTouristDetails();
            m = m.GetTouristMatchDetailsByID(id);
            return Calculate(m, AllTourists);
        }
      
        public List<CalculateMatch> Calculate(Match match, List<Match> Others)
        {
            List<CalculateMatch> CalList = new List<CalculateMatch>();
            DBservices db = new DBservices();
            List<Match> mList = Others;
            for (int i = 0; i < mList.Count; i++)
            {
                double rankLang = 0;
                double rankHobby = 0;
                double rankExpertise = 0;
                double rankAge = 0.0;
               Match match2 = mList[i];
                double num = Math.Abs(match.Age - match2.Age) + 1;
                rankAge = 10 / num;
                if (match2.Language.Count > 0 || match.Language.Count>0)
                {
                    for (int j = 0; j < match.Language.Count; j++)
                    {
                        if (match2.Language.Contains(match.Language[j]))
                        {
                            rankLang += LanguagesMax;
                            break;
                        }
                    }
                }
                if (match2.Hobbies.Count > 0 || match.Hobbies.Count > 0)
                {
                    for (int j = 0; j < match.Hobbies.Count; j++)
                    {
                        if (match2.Hobbies.Contains(match.Hobbies[j]))
                        {
                            rankHobby += (HobbiesMax / match.Hobbies.Count);
                        }
                    }
                }

                if (match2.Expertises.Count > 0 || match.Expertises.Count > 0)
                {
                    for (int j = 0; j < match.Expertises.Count; j++)
                    {
                        if (match2.Expertises.Contains(match.Expertises[j]))
                        {
                            rankExpertise += (ExpertisesMax / match.Expertises.Count);
                        }
                    }
                }

                if (rankExpertise > ExpertisesMax)
                {
                    rankExpertise = ExpertisesMax;
                }
                if (rankHobby > HobbiesMax)
                {
                    rankHobby = HobbiesMax;
                }
                if (rankAge > AgeMax)
                {
                    rankAge = AgeMax;
                }
                CalculateMatch mCal = new CalculateMatch();
                mCal.Id1 = match.Id;
                mCal.Id2 = match2.Id;
                mCal.Percents = rankLang + rankHobby + rankExpertise + Math.Ceiling(rankAge);
                CalList.Add(mCal);
            }
            return CalList;
        }



    }
    
}