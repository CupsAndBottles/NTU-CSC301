using System;

namespace MRTForm
{
	/// <summary>
	/// Description of Path.
	/// </summary>
	public class Path<T> : IComparable
	{
		NodeList<T> pathList;
		int weight;
		
			public Path()
		{
				pathList = new NodeList<T>();
				weight = 0;
		}
			
			public Path(NodeList<T> newList){
				pathList = new NodeList<T>();
				weight = 0;
				
				foreach(Node<T> node in newList){
					pathList.Add(node);
					weight++;
				}
			}
			
			public void Add(Node<T> newNode){
				pathList.Add(newNode);
				weight++;
			}
			
			public Node<T> GetFirst(){
				return pathList.GetFirst();
			}
			
			public Node<T> GetLast(){
				return pathList.GetLast();
			}
			
			public void RemoveLast(){
				pathList.RemoveLast();
				weight--;
			}
			
			public bool Contains(int value){
				return pathList.Contains(value);
			}
			
			public bool Contains(Node<T> node){
				return pathList.Contains(node.Value);
			}
			
			public NodeList<T> PathList{
				get{
					return pathList;
				}
			}
			
			public int Weight{
				get
				{
					return weight;
				}
			}
			
			public int CompareTo(Object obj){
				Path<T> newPath = (Path<T>)obj;
				return this.weight.CompareTo(newPath.weight);
			}

				
	}
}
