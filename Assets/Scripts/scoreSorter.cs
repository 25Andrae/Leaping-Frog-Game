using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class scoreSorter
{
    static public string[] highScoreData;
    static public string[] SaveIntoStringArray(string data)
    {
        List<string> listOfHighScore;
        if (highScoreData == null) {
           listOfHighScore = new List<string>();
           listOfHighScore.Add(data);
        }
        else {
            listOfHighScore = highScoreData.ToList();
            if (listOfHighScore.Count < 5)
            {
                listOfHighScore.Add(data);
            }
            else
            {
                listOfHighScore.RemoveAt(0);
                listOfHighScore.Add(data);
            }
        }
        string[] returnMe = listOfHighScore.ToArray();
        return returnMe;
    }
}
