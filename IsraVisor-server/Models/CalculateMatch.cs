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
        double GuideRank = 20;
        double AgeMax = 5;
        double LanguagesMax = 30;
        double HobbiesMax = 30;
        double ExpertisesMax = 35;
        double Rank = 50;
        double maxRank = 5;

        public List<CalculateMatch> CalculateMatchBetweenTouristToAllGuides(int id)
        {
            Match m = new Match();
            m = m.GetTouristMatchDetailsByID(id);
            List<Match> AllTourists = m.GetTouristDetails();
            List<Match> AllGuides = m.GetGuidesDetails();
            List<Guide_Tourist> listRanks = new List<Guide_Tourist>();
            for (int i = 0; i < AllTourists.Count; i++)
            {
                Guide_Tourist g = new Guide_Tourist();
               g = g.GetRankByID(AllTourists[i].Id);
                if (g.Rank>0)
                {
                    listRanks.Add(g);
                }
            }
            List<CalculateMatch> tempCal = Calculate(m, AllGuides, listRanks);
            return tempCal;
        }

        public List<CalculateMatch> CalculateMatchBetweenGuideToAllGuides(int id)
        {
            Match m = new Match();
            m = m.GetGuideMatchDetailsByID(id);
            List<Match> AllGuides = m.GetGuidesDetails();
            List<CalculateMatch> tempCal = Calculate(m, AllGuides,null);
            return tempCal;
        }

        public List<CalculateMatch> CalculateMatchBetweenTouristToAllTourists(int id)
        {
            Match m = new Match();
            List<Match> AllTourists = m.GetTouristDetails();
            m = m.GetTouristMatchDetailsByID(id);
            List<CalculateMatch> tempCal = Calculate(m, AllTourists,null);
            return tempCal;
        }
        public List<CalculateMatch> GetTop3(List<CalculateMatch> cal)
        {
            List<CalculateMatch> tempCal = new List<CalculateMatch>();
          cal = cal.OrderBy(mCal => mCal.Percents).ToList();

            for (int i = cal.Count-1; i >= cal.Count-4; i--)
            {
                tempCal.Add(cal[i]);
            }
            return tempCal;
        }
      
      
        public List<CalculateMatch> Calculate(Match match, List<Match> Others, List<Guide_Tourist> ranksList)
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
                //רק אם הכניסו שפות לתייר/מדריך
                if (match2.Language.Count > 0 || match.Language.Count>0)
                {
                    for (int j = 0; j < match.Language.Count; j++)
                    {
                        //בודק אם השפה של המדריך נמצאת במערך שפות של התייר
                        if (match2.Language.Contains(match.Language[j]))
                        {
                            rankLang += LanguagesMax;
                            break;
                        }
                    }
                }

                //בודק אם יש תחביב למדריך/תייר 
                if (match2.Hobbies.Count > 0 || match.Hobbies.Count > 0)
                {
                    for (int j = 0; j < match.Hobbies.Count; j++)
                    {
                        //
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
                if (match2.Rank > 0)
                {
                    double tempPer = rankAge + rankLang + rankHobby + rankExpertise;
                    mCal.Percents = (tempPer * (100 - GuideRank)/100) + (match2.Rank/5)*GuideRank;
                }
                else
                {
                    mCal.Percents = rankLang + rankHobby + rankExpertise + Math.Ceiling(rankAge);
                }
                if (ranksList != null && ranksList.Count>0)
                {
                    for (int j = 0; j < ranksList.Count; j++)
                    {
                        if (mCal.Id2 == ranksList[j].guidegCode)
                        {
                            double tempPercents = mCal.Percents*Rank/100;
                            double tempCalRank = ranksList[j].Rank / maxRank;
                            double tempRank = tempCalRank * Rank;
                            mCal.Percents = tempPercents + tempRank;
                            break;
                        }
                    }
                }
                CalList.Add(mCal);
            }
            return CalList;
        }



    }
    
}