using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public static class JMath
{


    public static void sayHi()
    {
        Debug.Log("JMath checking in");
    }

    public static int increaseWithLimitAndStick(float index, float amount, float limit)
    {
        index += amount;


        if (Mathf.Abs(index) > Mathf.Abs(limit))
        {
            index = limit;
        }


        return (int)index;

    }

    public static string getRandPhrase(List<string> inList)
    {
        string retString = "";

        retString = inList[Random.Range(0, inList.Count)];

        return retString;
    }

    public static string wrapText(string input, float lineLength)
    {
        // Split string by char " "         
        string[] words = input.Split(" "[0]);

        // Prepare result
        string result = "";

        // Temp line string
        string line = "";

        // for each all words        
        foreach (string s in words)
        {
            // Append current word into line
            string temp = line + " " + s;

            // If line length is bigger than lineLength
            if (temp.Length > lineLength)
            {

                // Append current line into result
                result += line + "\n";
                // Remain word append into new line
                line = s;
            }
            // Append current word into current line
            else
            {
                line = temp;
            }
        }

        // Append last line into result        
        result += line;

        // Remove first " " char
        return result.Substring(1, result.Length - 1);
    }

    public static double getAngle(float x1, float y1, float x2, float y2)
    {

        double hyp = Mathf.Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)));

        var angle = (180 / System.Math.PI) * System.Math.Acos((y1 - y2) / (hyp));

        if (x1 > x2)
        {
            angle = 360 - (180 / System.Math.PI) * System.Math.Acos((y1 - y2) / (hyp));

        }

        return angle;

    }

    public static double getHyp(float x1, float y1, float x2, float y2)
    {

        double hyp = Mathf.Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)));

        return hyp;

    }

    public static double getX(float rot)
    {
        double rotRad = rot * (System.Math.PI / 180);
        return System.Math.Sin(rotRad);
    }

    public static double getY(float rot)
    {
        double rotRad = rot * (System.Math.PI / 180);
        return -System.Math.Cos(rotRad);
    }
   
}
