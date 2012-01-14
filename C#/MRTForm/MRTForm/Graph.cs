using System;
using System.Collections.Generic;

namespace MRTForm
{
	/// <summary>
	/// Description of Graph.
	/// </summary>
	public class Graph<T>
	{
		private NodeList<T> nodeList;
		
		public Graph()
		{
			nodeList = new NodeList<T>();
		}
		
		public void AddNode(Node<T> newNode){
			
			int id = newNode.ID;
			
			//Check for replication of nodes
			foreach(Node<T> currNode in nodeList){
				if(id == currNode.ID){
					return;
				}
			}
			nodeList.Add(newNode);
		}
		
		public Node<T> GetNodeByID(int ID){
			return nodeList.FindByID(ID);
		}
			
		public void AddUndirectedEdge(Node<T> fromNode, Node<T> toNode){
			fromNode.AddNeighbour(toNode);
			toNode.AddNeighbour(fromNode);
		}
		
		public void AddUndirectedEdge(int fromNodeID, int toNodeID){
			
			if(!NodeExists(fromNodeID) || !NodeExists(toNodeID) || fromNodeID == toNodeID){
			   	return;
			  }
			
			AddUndirectedEdge(nodeList.FindByID(fromNodeID),nodeList.FindByID(toNodeID));
		}
		
		public bool NodeExists(int NodeID){
			return nodeList.Contains(NodeID);
		}
		
		public bool NodeExists(Node<T> Node){
			return nodeList.Contains(Node);
			
		}
		
		public int NumOfNodes{
			get{
			return nodeList.Count;	
			}
		}
	
	}
}
