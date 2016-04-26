using UnityEngine;
using System.Collections.Generic;

public class MGraphNode
{
	public Position position { get; private set; }
	public int moveCost;

	public List<MGraphNode> neighbors { get; private set; }

	public MGraphNode(Position p, int moveCost)
	{
		position = p;
		neighbors = new List<MGraphNode> ();
		this.moveCost = moveCost;
	}

	public void addNeighbor(MGraphNode n)
	{
		if (n == null)
			return;
		
		neighbors.Add (n);
	}
}

public class MovementGraph {

	public bool wasBuilt { get; private set; }
	public MGraphNode origin { get; private set; }
	public MGraphNode goal { get; private set; }
	public MGraphNode[,] nodeArray { get; private set; }

	public MovementGraph()
	{
		wasBuilt = false;
		origin = null;
		goal = null;
		nodeArray = null;
	}

	public void build(Position origin, int[,] movementArray)
	{
		int width, height; //map size

		width = movementArray.GetLength (0);
		height = movementArray.GetLength (1);

		nodeArray = new MGraphNode[width,height];

		//Populating nodeArray
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {

				//if movement cost is 5, we can't walk there, so we don't add it to the graph
				if (movementArray [x, y] == 5) 
					continue;

				nodeArray [x, y] = new MGraphNode (new Position(x, y), movementArray [x, y]);
			}
		}

		MGraphNode curr;

		//Now we add the edges
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {

				curr = nodeArray [x, y];

				if (curr == null)
					continue;

				//Maybe I could create methods for each of these?
				//Maybe I don't even need a graph?
				//top
				if ((y + 1) < height) curr.addNeighbor(nodeArray[x,y+1]);

				//right
				if((x+1) < width) curr.addNeighbor(nodeArray[x+1,y]);

				//bottom
				if(y > 0) curr.addNeighbor(nodeArray[x, y-1]);

				//left
				if(x > 0) curr.addNeighbor(nodeArray[x-1, y]);
			}
		}


		//Last step: get the origin node
		this.origin = nodeArray [origin.x, origin.y];
	}


	public void setGoal(Position p)
	{
		if (nodeArray == null) {
			Debug.LogError ("Trying to set MovementGraph goal before building the graph");	
			return;
		}

		goal = nodeArray [p.x, p.y];
	}

	public void unsetGoal()
	{
		goal = null;
	}

	public bool hasGoal() { return goal != null; }
}
