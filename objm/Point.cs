using System;
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
		
		public Point Add(Point voP) {
			
		}

        public double getDistanceTo(Point voPoint) {
            return Math.Sqrt(
                       Math.Pow((voPoint.PosX - this.PosX), 2)
                     + Math.Pow((voPoint.PosY - this.PosY), 2)
                   );
        }
		
		public double getShortestDistanceTo(Point voPoint) {
			double x = (this.PosX < 500)? 1000:-1000;
			double y = (this.PosY < 500)? 1000:-1000;
			Point p2 = new Point(voPoint.PosX + x, voPoint.PosY); 
			Point p3 = new Point(voPoint.PosX, voPoint.PosY + y); 
			Point p4 = new Point(voPoint.PosX + x, voPoint.PosY + y); 			
			double dist = getDistanceTo(voPoint);
			dist = Math.Min(dist, getDistanceTo(p2));
			dist = Math.Min(dist, getDistanceTo(p3));
			dist = Math.Min(dist, getDistanceTo(p4));
			return dist;
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
