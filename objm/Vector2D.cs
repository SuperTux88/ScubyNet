using System;

namespace ScubyNet.obj
{
	public class Vector2D
	{
		public static double MAX_X = 1000.0;
		public static double MAX_Y = 1000.0;
		private double x = 0.0;
		private double y = 0.0;
		
		public Vector2D () { }
		public Vector2D (double length) { x=length; y=0.0; }
		public Vector2D (double vfX, double vfY) { x=vfX; y=vfY; }
		
		public double X { get { return x; } set { x = value; } }
		public double Y { get { return y; } set { y = value; } }
		public double Length { 
			get { return Math.Sqrt(x*x+y*y); } 
			set { 
				double l = Length;
				if (l != 0) {
					x = x / l * value;
					y = y / l * value;
				}
			}
		}
		public double Angle {
			get { return Math.Atan2(y, x); }
			set { 
				double l = Length;
				x = Math.Cos(value) * l;
           		y = Math.Sin(value) * l;
			}
		}
		
		public void Mult(double vfF) { x *= vfF; y *= vfF; }
		
		public Vector2D Add(Vector2D vV) { return Add(vV.x, vV.y); }
		public Vector2D Add(double vX, double vY) {	return new Vector2D(x + vX,	y + vY); }
		
		public Vector2D Inv() { return new Vector2D(-x,-y); }
		public Vector2D Norm() { double l = Length;	return new Vector2D(x / l, y / l);}
		
		public double DistanceTo(Vector2D vV) { return this.Add(vV.Inv()).Length; }
		
		public double VirtualDistanceTo(Vector2D v1) {
			double dx = (v1.x < (MAX_X/2.0)) ? MAX_X : -MAX_X;
			double dy = (v1.y < (MAX_Y/2.0)) ? MAX_Y : -MAX_Y;
			Vector2D v2 = v1.Add( dx, 0.0);
			Vector2D v3 = v1.Add(0.0,  dy); 
			Vector2D v4 = v1.Add( dx,  dy); 
			double dist =                DistanceTo(v1) ;
			       dist = Math.Min(dist, DistanceTo(v2));
			       dist = Math.Min(dist, DistanceTo(v3));
			       dist = Math.Min(dist, DistanceTo(v4));
			return dist;
		}
		
		public double AngleTo(Vector2D vV) { return this.Add(vV.Inv()).Angle; }
		
		public double VirtualAngleTo(Vector2D v1) {
			double dx = (v1.x < (MAX_X/2.0)) ? MAX_X : -MAX_X;
			double dy = (v1.y < (MAX_Y/2.0)) ? MAX_Y : -MAX_Y;
			Vector2D v2 = v1.Add( dx, 0.0);
			Vector2D v3 = v1.Add(0.0,  dy); 
			Vector2D v4 = v1.Add( dx,  dy); 
			double angle = AngleTo(v1);
			double dist  = DistanceTo(v1);
			double dist2 = DistanceTo(v2); if (dist2 < dist) { angle = AngleTo(v2); dist = dist2; }
			       dist2 = DistanceTo(v3); if (dist2 < dist) { angle = AngleTo(v3); dist = dist2; }
			       dist2 = DistanceTo(v4); if (dist2 < dist) { angle = AngleTo(v4); dist = dist2; }
			return angle;
		}
		
		//public Point P { get { return new Point(x,y); } }
		
		public override string ToString ()
		{
			 return "(" + x.ToString() + "|" + y.ToString() + ")";
		}
	}
}

