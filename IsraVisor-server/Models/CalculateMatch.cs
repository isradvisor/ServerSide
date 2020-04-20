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
        double GuideRank = 20;  //מקסימום אחוזים שמקבל הניקוד הכללי של המדריך מתוך 100%
        double AgeMax = 5; //מקסימום אחוזים שמקבל הפרש הגילאים מתוך 100%
        double LanguagesMax = 30; //מקסימום אחוזים שמקבל השוואת השפות מתוך 100%
        double HobbiesMax = 30; //מקסימום אחוזים שמקבל השוואת התחביבים מתוך 100%
        double ExpertisesMax = 35;//מקסימום אחוזים שמקבל השוואת ההתמחויות מתוך 100%
        double RankByTourist = 50; //האחוזים בהשוואה של ניקוד ספציפי למדריך של תייר דומה
        double maxRank = 5;//מקסימום ניקוד אפשרי שניתן לתת למדריך ע"י תייר
        double MaxExpertiseHobby = 2;

        //משווה בין תייר לכל המדריכים
        public List<CalculateMatch> CalculateMatchBetweenTouristToAllGuides(int id)
        {
            Match m = new Match();
            m = m.GetTouristMatchDetailsByID(id);      //מקבלת תייר ספציפי ע"פ פורמט הנרמול

            List<Match> AllGuides = m.GetGuidesDetails();                //מביא את כל המדריכים ע"פ פורמט הנרמול

            List<Guide_Tourist> listRanks = new List<Guide_Tourist>();   //רשימת ניקודים שהתיירים נתנו למדריכים
            List<CalculateMatch> TouristsList = CalculateMatchBetweenTouristToAllTourists(id); //מביא את ההשוואות בין תייר לכל שאר התיירים
            for (int i = 0; i < TouristsList.Count; i++)                 //רץ על כל רשימת השוואות התיירים
            {
                if (TouristsList[i].Percents>60) //בודק אם יש תייר שדומה לתייר במעל 60 אחוזים
                {
                    Guide_Tourist g = new Guide_Tourist();
                    g = g.GetRankByID(TouristsList[i].Id2); //מקבל את הניקודים שתייר ספציפי נתן למדריכים
                    if (g.Rank > 0)
                    {
                        listRanks.Add(g); //מכניס לרשימת הניקודים שהתייר נתן למדריכים
                    }
                }
              
            }
            List<CalculateMatch> tempCal = Calculate(m, AllGuides, listRanks);
            return tempCal;
        }

        //משווה בין מדריך ספציפי לכל המדריכים
        public List<CalculateMatch> CalculateMatchBetweenGuideToAllGuides(int id)
        {
            Match m = new Match();  
            m = m.GetGuideMatchDetailsByID(id);  //מקבלת מדריך ספציפי ע"פ פורמט הנרמול
            List<Match> AllGuides = m.GetGuidesDetails();  //מקבלת את כל המדריכים ע"פ פורמט הנרמול
            List<CalculateMatch> tempCal = Calculate(m, AllGuides,null); 
            return tempCal;
        }

        //משווה בין תייר ספציפי לכל התיירים
        public List<CalculateMatch> CalculateMatchBetweenTouristToAllTourists(int id)
        {
            Match m = new Match();
            List<Match> AllTourists = m.GetTouristDetails(); //מקבלת את כל התיירים ע"פ פורמט הנרמול
            m = m.GetTouristMatchDetailsByID(id); //מקבלת תייר ספציפי ע"פ פורמט הנרמול
            List<CalculateMatch> tempCal = Calculate(m, AllTourists,null);
            return tempCal;
        }

        //מקבלת את ה-3 הכי גבוהים 
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
      
      //מחשבת השוואה
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
               Match match2 = mList[i];   //    המדריך/התייר אליו משווים
                
               //משווה גילאים
                double num = Math.Abs(match.Age - match2.Age) + 1; //חישוב הפרש גילאים בערך מוחלט
                rankAge = 10 / num;
                //גילאים


                //רק אם הכניסו שפות לתייר/מדריך
                if (match2.Language.Count > 0 || match.Language.Count>0)
                {
                    //לולאה שרצה על כל שפות התייר
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
                    //לולאה שרצה על כל תחביבי התייר
                    for (int j = 0; j < match.Hobbies.Count; j++)
                    {
                        //בודק אם התחביב של המדריך/תייר נמצא ברשימה של תחביבי התייר
                        if (match2.Hobbies.Contains(match.Hobbies[j]))
                        {
                            rankHobby += (HobbiesMax / MaxExpertiseHobby) + HobbiesMax/2;
                        }
                    }
                }

                //בודק אם יש למדריך/תייר רשימת התמחויות
                if (match2.Expertises.Count > 0 || match.Expertises.Count > 0)
                {
                    //לולאה שרצה על רשימת ההתמחויות שהתייר בחר
                    for (int j = 0; j < match.Expertises.Count; j++)
                    {
                        //בודק אם יש למדריך/תייר התמחות ברשימת ההתמחויות של התייר
                        if (match2.Expertises.Contains(match.Expertises[j]))
                        {
                            rankExpertise += (ExpertisesMax / MaxExpertiseHobby) + ExpertisesMax/2;
                        }
                    }
                }

                //בודק שלא נותנים ניקוד יותר ממקסימום הניקוד האפשרי
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
                //מקסימום ניקוד אפשרי


                CalculateMatch mCal = new CalculateMatch();
                mCal.Id1 = match.Id;
                mCal.Id2 = match2.Id;

                //בודק אם match2 הוא תייר או מדריך
                //אם rank<=0 אז הוא תייר
                if (match2.Rank > 0)
                {
                    //חישוב אחוזי מדריך
                    double tempPer = rankAge + rankLang + rankHobby + rankExpertise;
                    mCal.Percents = (tempPer * (100 - GuideRank)/100) + (match2.Rank/5)*GuideRank;
                }
                else
                {
                    //חישוב אחוזי תייר
                    mCal.Percents = rankLang + rankHobby + rankExpertise + Math.Ceiling(rankAge); //ceiling מעגל למעלה
                }

                //אם קיים תייר דומה, מתווספת תוספת הניקוד שאותו תייר נתן למדריך
                if (ranksList != null && ranksList.Count>0)
                {
                    for (int j = 0; j < ranksList.Count; j++) //רץ על רשימת הניקודים שהתיירים נתנו
                    {
                        if (mCal.Id2 == ranksList[j].guidegCode)//בודקת אם המדריך נמצא ברשימת הניקוד שהתייר נתן למדריכים
                        {
                            //חישוב מחדש של המדריך עם תוספת הניקוד שהתייר הדומה נתן לו
                            double tempPercents = mCal.Percents* (100-RankByTourist) / 100; //החישוב שנעשה לפני מחושב מחדש על ידי החלוקה עם הניקוד הספציפי
                            double tempCalRank = ranksList[j].Rank / maxRank; //חישוב החלק של הניקוד הספציפי מתוך 5
                            double tempRank = tempCalRank * RankByTourist; //האחוזים של הניקוד הספציפי
                            mCal.Percents = tempPercents + tempRank; //חיבור בין האחוזים של הניקוד הספציפי לחישוב הקודם הכולל
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