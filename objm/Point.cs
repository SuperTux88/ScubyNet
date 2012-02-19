﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScubyNet.obj
{
    public class Point
    {
        private double mPosX;
        private double mPosY;        

        public Point()
        {
        }

        public Point(double vdPosX, double vdPosY)
        {
            mPosX      = vdPosX;
            mPosY      = vdPosY;
        }

        public double getDistanceTo(Point voPoint){
            return Math.Sqrt(
                       Math.Pow((voPoint.PosX - this.PosX), 2)
                     + Math.Pow((voPoint.PosY - this.PosY), 2)
                   );
        }

        public double getAngle(Point voPoint)
        {
            return Math.Atan2(
                    (voPoint.PosY - this.PosY), 
                    (voPoint.PosX - this.PosX)
                   );
        }

        public double PosX { get { return mPosX; } set { mPosX = value; } }
        public double PosY { get { return mPosY; } set { mPosY = value; } }
    }
}
