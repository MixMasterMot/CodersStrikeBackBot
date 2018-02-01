using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{

    static void Main(string[] args)
    {
        string[] inputs;
        bool boosted = false;
        bool raceStart = false;

        int lastX = 0;
        int lastY = 0;

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]);
            int y = int.Parse(inputs[1]);
            // x position of the next check point
            int nextCheckpointX = int.Parse(inputs[2]);
            // y position of the next check point
            int nextCheckpointY = int.Parse(inputs[3]);
            // distance to the next checkpoint
            int nextCheckpointDist = int.Parse(inputs[4]);
            // angle between your pod orientation and the direction of the next checkpoint
            int nextCheckpointAngle = int.Parse(inputs[5]);
            inputs = Console.ReadLine().Split(' ');
            int opponentX = int.Parse(inputs[0]);
            int opponentY = int.Parse(inputs[1]);

            string thrust = "0";
            // if angle to next point is to great slow speed to increase turn speed
            // add more angles
            // change speed according to angles
            // greater angle == less thrust
            if (nextCheckpointAngle > 90 | nextCheckpointAngle < -90)
            {
                thrust = "0";
            }
            else
            {
                thrust = "100";
            }
            if (nextCheckpointAngle == 0 && nextCheckpointDist >= 500 && boosted == false)
            {
                thrust = "BOOST";
            }
            Tuple<int, int> trgt = null;
            if (raceStart == false)
            {
                raceStart = true;
                trgt = new Tuple<int, int>(nextCheckpointX, nextCheckpointY);
            }
            else
            {
                trgt = getStopingpoint(x, y, nextCheckpointX, nextCheckpointY, lastX, lastY);
            }
            

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // You have to output the target position
            // followed by the power (0 <= thrust <= 100)
            // i.e.: "x y thrust"
            lastX = x;
            lastY = y;
            string activeThrust = " " + thrust;
            Console.WriteLine(trgt.Item1 + " " + trgt.Item2 + activeThrust);
        }
    }

    public static Tuple<int,int> getTarget(int curX, int curY,int nextCheckpointX, int nextCheckpointY,int lastX,int lastY)
    {
        //double speedLosPerTurn = -0.15;
        double speed = Math.Sqrt(Math.Abs(Math.Pow((curX - lastX), 2) + Math.Pow((curY - lastY), 2)));

        double distanceToPoint= Math.Sqrt(Math.Abs(Math.Pow((curX - nextCheckpointX), 2) + Math.Pow((curY - nextCheckpointY), 2)));

        double turnsToStop = Math.Log(speed) / 0.163;
        double avrgSpeedLoss = (0 - speed) / turnsToStop;
        double stopDistance = Math.Pow(speed, 2) / (2 * avrgSpeedLoss);

        double stopPerc = (distanceToPoint - stopDistance) / distanceToPoint;
        double angle = Math.Atan2((curY - nextCheckpointY), (curX - nextCheckpointX)) * 360 / Math.PI;

        int x = Convert.ToInt32(curX + Math.Cos(angle) * distanceToPoint - stopDistance);
        int y = Convert.ToInt32(curX + Math.Sin(angle) * distanceToPoint - stopDistance);
        //int x = Convert.ToInt32(nextCheckpointX * stopPerc);
        //int y = Convert.ToInt32(nextCheckpointY * stopPerc);
        Console.Error.WriteLine("Debug trgt " +x + " " +y);

        return new Tuple<int, int>(x, y);
    }

    public static Tuple<int,int> getStopingpoint(int curX, int curY, int nextCheckpointX, int nextCheckpointY, int lastX, int lastY)
    {
        //get angle from current pos to next point
        double angle = Math.Atan2((curY - nextCheckpointY), (curX - nextCheckpointX)) * 360 / Math.PI;
        //get current speed
        double currentSpeed = calculateDistance(curX, curY, lastX, lastY);
        double stopDistance = currentSpeed / 100;
        double distanceToCheckPoint = calculateDistance(curX, curY, nextCheckpointX, nextCheckpointY);

        //get point to break at
        int x = Convert.ToInt32(curX + Math.Cos(angle) * distanceToCheckPoint - stopDistance);
        int y = Convert.ToInt32(curX + Math.Sin(angle) * distanceToCheckPoint - stopDistance);
        //return break point
        return new Tuple<int, int>(x, y);
    }
    public static double calculateDistance(int x1,int y1,int x2, int y2)
    {
        double distanceToPoint = Math.Sqrt(Math.Abs(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2)));
        return distanceToPoint;
    }
}