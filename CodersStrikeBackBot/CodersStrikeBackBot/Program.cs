using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace CodersStrikeBackBot
{
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

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");


                // You have to output the target position
                // followed by the power (0 <= thrust <= 100)
                // i.e.: "x y thrust"
                string activeThrust = " " + thrust;
                Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + activeThrust);
            }
        }
    }
}
