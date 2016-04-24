using UnityEngine;
using System.Collections.Generic;

public class MGraphNode
{
	public Position position { get; private set; }
	public List<MGraphNode> neighbors { get; private set; }
	public int moveSpeed;

	public MGraphNode(Position p, int moveSpeed)
	{
		position = p;
		neighbors = new List<MGraphNode> ();
		this.moveSpeed = moveSpeed;
	}

	public void addNeighbor(MGraphNode n)
	{
		if (n == null)
			return;
		
		neighbors.Add (n);
	}
}

public class MovementGraph {

	public MGraphNode origin { get; private set; }

	public void build(Position origin, int[,] movementArray)
	{
		int x, y; //map size
		MGraphNode[,] nodeArray;

		x = movementArray.GetLength (0);
		y = movementArray.GetLength (1);

		nodeArray = new MGraphNode[x,y];

		//Populating nodeArray
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {

				//if movement is 0, we don't want to add it to the graph
				if (movementArray [i, j] == 0) 
					continue;

				nodeArray [i, j] = new MGraphNode (new Position(i, j), movementArray [i, j]);
			}
		}

		MGraphNode curr;

		//Now we add the edges
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {

				curr = nodeArray [i, j];

				if (curr == null)
					continue;

				//Maybe I could create methods for each of these?
				//Maybe I don't even need a graph?
				//top
				if ((j + 1) < y) curr.addNeighbor(nodeArray[i,j+1]);

				//right
				if((i+1) < x) curr.addNeighbor(nodeArray[i+1,j]);

				//bottom
				if(j > 0) curr.addNeighbor(nodeArray[i, j-1]);

				//left
				if(i > 0) curr.addNeighbor(nodeArray[i-1, j]);
			}
		}


		//Last step: get the origin node
		this.origin = nodeArray [origin.x, origin.y];
	}
}
