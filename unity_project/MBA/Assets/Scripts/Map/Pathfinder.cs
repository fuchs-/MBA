using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public class Pathfinder 
{
	//Dijkstra stuff
	public static Dictionary<MGraphNode, int> dist;
	public static Dictionary<MGraphNode, MGraphNode> prev;

	public static void Dijkstra(MovementGraph graph)
	{
		Stopwatch s = new Stopwatch();

		s.Start ();

		List<MGraphNode> Q = new List<MGraphNode>();		//Set of unvisited nodes
		dist = new Dictionary<MGraphNode, int>();			//Distances to each node, from the origin
		prev = new Dictionary<MGraphNode, MGraphNode>();	//Previous node to each node on the path
		
		foreach(MGraphNode v in graph.nodeArray)			//initializing sets
		{
			if (v == null)
				continue;
			dist [v] = int.MaxValue;
			prev [v] = null;
			Q.Add (v);							
		}

		dist [graph.origin] = 0;							//distance cost from the origin to the origin

		MGraphNode u;
		int alt;

		while (Q.Count > 0) {
			u = getLowestDistance (Q);						//origin will be selected first
			Q.Remove (u);									//equivalent to visiting the node u

			foreach (MGraphNode v in u.neighbors) {
				if (!Q.Contains (v)) continue; 				//We dont want neighbors that have already been visited

				alt = dist [u] + v.moveCost;

				if(alt < dist[v])							//New shortest path to v was found
				{
					dist [v] = alt;
					prev [v] = u;
				}
			}
		}

		s.Stop ();

		UnityEngine.Debug.Log ("Dijkstra's algorithm just finished taking " + s.ElapsedMilliseconds + " ms");
	}

	private static MGraphNode getLowestDistance(List<MGraphNode> l)
	{
		MGraphNode ret;
		int currValue;

		ret = null;
		currValue = int.MaxValue;

		foreach (MGraphNode n in l) {
			if (dist [n] <= currValue) {
				ret = n;
				currValue = dist [n];
			}
		}

		return ret;
	}

}
