using System;
using System.Collections.Generic;
using ScubyNet.obj;
using ScubyNet.net;

namespace ScubyNet.inp
{
	public class InpScriptEvent
	{
		private InpScript moParent;
		private string msEventName;
		private string msEventCondition = "";
		private string[] msCommands;
		
		public InpScriptEvent(InpScript voParent, string vsEventName, List<string> vcsCommands)
		{
			moParent = voParent;
			int lPos = vsEventName.IndexOf("/");
			if (lPos > 0 ) {
				msEventName = vsEventName.Substring(0, lPos).Trim();
				msEventCondition = vsEventName.Substring(lPos+1).Trim();
				ParseCondition (msEventCondition );
			} else {
				msEventName = vsEventName;
			}
			msCommands = vcsCommands.ToArray();
		}
		
		public string Name { get { return msEventName; } } 
		
		public enum EPool { PLAYER = 0, SHOT, HITPOINT, ENTITY };
		public enum EMatch { MY = 0, OUR, OTHER, ALL };
		public enum EOrder { NONE = 0, DISTANCE, ANGLE, TIME };
		private EPool EntityPool = EPool.PLAYER;
		private EMatch EntityMatch = EMatch.MY;
		private EOrder EntityOrder = EOrder.NONE;
		bool EMin = true;
		
		private void ParseCondition(string vsCondition) {
			string[] parts = vsCondition.Split(' ');
			foreach (string sPart in parts) {
				string s = sPart.ToUpper().Trim();
				if (sPart.Trim().Length == 0)
					continue;
				foreach (var e in Enum.GetValues(typeof(EPool)))
					if (s.Equals(((EPool)e).ToString("F")))
						EntityPool = (EPool)e;
				foreach (var e in Enum.GetValues(typeof(EMatch)))
					if (s.Equals(((EMatch)e).ToString("F")))
						EntityMatch = (EMatch)e;
				foreach (var e in Enum.GetValues(typeof(EOrder)))
					if (s.Equals(((EOrder)e).ToString("F")))
						EntityOrder = (EOrder)e;
				if (s.Equals("MAX"))
					EMin = false;
			}
		}
		
		private string Eval(string sLine, bool vbFkt) { 
			int count = 0;
			string ssub = "";
			string sret = "";
			foreach (char c in sLine.Trim()) {
				if (c == '[') 
					count++;
				else if (c == ']') {
					count--;
					if (count == 0)
						sret += Eval(ssub, true);
				}
				else {
					if (count == 0)
						sret += c; 
					else 
						ssub += c;
				}
			}
			if (count != 0) {
				Console.WriteLine ( "cannot parse expression" );
				return "{ERR}";
			}
			
			if (vbFkt) {
				string[] asParts = sret.Trim().Split(':');
				string sfkt = asParts[0].Trim();
				string[] asParams = null;
				if (asParts.Length > 0) {
					asParams = asParts[1].Replace(',',' ').Replace("  "," ").Trim().Split(' ');
				}
				sret = InpFunction.RunFunction(sfkt, asParams);
			}
			
			return sret;
		}
		
		private string GetStarValue(Connection voConn) {
			Player me = World.TheWorld.GetPlayer(voConn.ID);
			if (EntityPool == EPool.HITPOINT) {
				if (EntityMatch != EMatch.OTHER) {
					throw new NotImplementedException();			
				}
				List<Vector2D> lP = new List<Vector2D>();
				foreach (Player p in World.TheWorld.Players.Values) {
					if (!p.IsFriend) {
						Vector2D pnt = me.GetHitpoint(p, true);
						lP.Add(pnt);
					}
				}
				if (lP.Count == 0) return "(500.0|500.0)";
				if (lP.Count == 1) return "(" + lP[0].X.ToString() + "|" + lP[0].Y.ToString() + ")";
				Vector2D pRet = lP[0];
				double dist = me.Position.VirtualDistanceTo(pRet);
				for (int i=1; i<lP.Count; i++) {
					double dist2 = me.Position.VirtualDistanceTo(lP[i]);
					if ((EMin && dist2 < dist) || (!EMin && dist2 > dist)) {
						dist = dist2;
						pRet = lP[i];
					}
				}
				return "(" + pRet.X.ToString() + "|" + pRet.Y.ToString() + ")";
			} else {
				List<Entity> lEnt = new List<Entity>();
				if (EntityPool == EPool.ENTITY || EntityPool == EPool.PLAYER) {
					foreach (Player e in World.TheWorld.Players.Values) {
						switch (EntityMatch) {
						case EMatch.ALL: lEnt.Add(e); break;
						case EMatch.MY:	if (e.ID == voConn.ID) lEnt.Add(e);	break;
						case EMatch.OUR: if (e.IsFriend) lEnt.Add(e); break;
						case EMatch.OTHER: if (!e.IsFriend) lEnt.Add(e); break;
						}
					}
				} else { 
					foreach (Shot e in World.TheWorld.Shots.Values) {
						switch (EntityMatch) {
						case EMatch.ALL: lEnt.Add(e); break;
						case EMatch.MY:	if (e.ParentId == voConn.ID) lEnt.Add(e); break;
						case EMatch.OUR: if (e.Parent.IsFriend) lEnt.Add(e); break;
						case EMatch.OTHER: if (!e.Parent.IsFriend) lEnt.Add(e); break;
						}
					}
				}
				if (lEnt.Count == 0)
					return "(500.0|500.0)";
				if (lEnt.Count == 1) 
					return "{" + lEnt[0].ID + "}";
				if (EntityOrder == EOrder.NONE)
					return "{" + lEnt[0].ID + "}";
				Entity eRet = lEnt[0];
				double diff = GetDiffToEntity(me, eRet);
				for (int i = 1; i < lEnt.Count; i++) {
					double diff2 = GetDiffToEntity(me, lEnt[i]);
					if ((EMin && diff2 < diff) || !EMin && diff2 > diff) {
						diff = diff2;
						eRet = lEnt[i];
					}						
				}
				return "{" + eRet.ID + "}";
			}
			
		}
		
		private double GetDiffToEntity(Player me, Entity e) {
			double dRet = EMin?double.MaxValue:double.MinValue;
			switch (EntityOrder) {
			case EOrder.DISTANCE: dRet = me.Position.VirtualDistanceTo(e.Position); break;
			case EOrder.ANGLE: Console.WriteLine("IMPLEMENT ANGLE DIFF"); break;
			case EOrder.TIME: Console.WriteLine("IMPLEMENT TIME DIFF"); break;
			}
			return dRet;
		}
		
		
		private bool MatchCondition(string vsCondition) {
			string[] ops = vsCondition.Trim().Split(' ');
			bool bRet = false;
			if (ops[0].Trim().Length == 0)
				return true;
			if (ops.Length != 3) {
				Console.WriteLine("ERROR evaluating condition '" + vsCondition + "' > false");
			} else {
				string op = ops[1].Trim();
				string a  = ops[0].Trim();
				string b  = ops[2].Trim();
				if (op.Equals("==")) 
					return a.Equals(b);
				else if (op.Equals("!="))
					return !a.Equals(b);
				else if (op.Equals("<")) {
					double da, db;
					if (!double.TryParse(a, out da)) { Console.WriteLine("Comp: Cannot express " + a + " as a number"); return false; }
					if (!double.TryParse(b, out db)) { Console.WriteLine("Comp: Cannot express " + b + " as a number"); return false; }
					return da < db;
				}
				else if (op.Equals(">")) {
					double da, db;
					if (!double.TryParse(a, out da)) { Console.WriteLine("Comp: Cannot express " + a + " as a number"); return false; }
					if (!double.TryParse(b, out db)) { Console.WriteLine("Comp: Cannot express " + b + " as a number"); return false; }
					return da > db;
				}
				
			}
			return bRet;
		}
		
		public void Trigger(Connection voConn) {
			bool nextif = false;
			foreach (string sLine in msCommands) { 
				string sFullLine = sLine.Replace("_", "{" + voConn.ID + "}" ); 
				
				sFullLine = sFullLine.Replace("*", GetStarValue(voConn) ); 
				
				sFullLine = Eval(sFullLine, false).Trim();
				if (sFullLine.Contains("{ERR")) {
					Console.WriteLine("error in line: " + sLine);
					Console.WriteLine(" >> " + sFullLine);
					continue;
				}
				
				if (nextif) {
					if (sFullLine.Equals(".")) {
						nextif = false;
						continue;
					} else if (sFullLine.StartsWith("?")) {
						nextif = false;
					} else 
						continue;
				}
				if (sFullLine.Equals(".")) continue;
				
				if (sFullLine.StartsWith("?")) {
					string sCompare = sFullLine.Substring(1).Trim();
					if (!MatchCondition(sCompare)) {
						nextif = true;
					} else { 
						Console.WriteLine(sCompare + " is true!");
					}	
				} else if (InpCommand.HasCommand(sFullLine.Split(' ')[0].Trim())) {
					Console.WriteLine("process line: " + sFullLine);
					InpCommand.RunCommand(sFullLine, voConn);
				} else { 
					// unknown
					Console.WriteLine("Dunno what to do with: " + sFullLine); 
				}
				
				
			}
		}
	
	}
}

