using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScubyNet.obj
{
    public class Point__OLD
    {
        private double mPosX;
        private double mPosY;
        
        public Point__OLD()
        {
        }	
		/*
        public Point__OLD(double vdPosX, double vdPosY)
        {
            mPosX = clacValidCoordinate(vdPosX);
            mPosY = clacValidCoordinate(vdPosY);
        }

        public Point__OLD(double vdDirection)
        {
            mPosX = clacValidCoordinate(Math.Cos(vdDirection));
            mPosY = clacValidCoordinate(Math.Sin(vdDirection));
        }
		
		public Point__OLD Add(Point voP) {
			return new Point(this.PosX + voP.PosX, this.PosY + voP.PosY);
		}

        public double getDistanceTo(Point__OLD voPoint) {
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

		
		public double getAngleToShortest(Point voPoint) {
			double x = (this.PosX < 500)? 1000:-1000;
			double y = (this.PosY < 500)? 1000:-1000;
			Point p2 = new Point(voPoint.PosX + x, voPoint.PosY); 
			Point p3 = new Point(voPoint.PosX, voPoint.PosY + y); 
			Point p4 = new Point(voPoint.PosX + x, voPoint.PosY + y);
			double dist = getDistanceTo(voPoint);
			double angle = getAngle(voPoint);
			double dist2 = getDistanceTo(p2); if (dist2 < dist) {angle=getAngle(p2); dist = dist2; }
			       dist2 = getDistanceTo(p3); if (dist2 < dist) {angle=getAngle(p3); dist = dist2; }
			       dist2 = getDistanceTo(p4); if (dist2 < dist) {angle=getAngle(p4); dist = dist2; }
			return angle;
		}
		
        public double getAngle(Point voPoint)
        {
            return Math.Atan2((voPoint.PosY - this.PosY) , (voPoint.PosX - this.PosX) );
        }
		
		public double tkatan() {
			return 0.0;
		}
			
			
        private double clacValidCoordinate(double dCoordinate)
        {
            return (dCoordinate + 1000.0) % 1000.0;
        }
		 *
        public double PosX { get { return mPosX; } set { mPosX = clacValidCoordinate(value); } }
        public double PosY { get { return mPosY; } set { mPosY = clacValidCoordinate(value); } }
        */
    }
}
