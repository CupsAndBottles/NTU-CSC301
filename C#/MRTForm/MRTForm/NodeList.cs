using System;
using System.Collections.Generic;

namespace MRTForm
{
	/// <summary>
	/// Description of NodeList.
	/// </summary>
	public class NodeList<T> : List<Node<T>>
	{
		public NodeList(): base()
		{
			
		}
		
		public bool Contains(int ID){
			return FindByID(ID) != null;
		}
		
		public bool Contains(T Value){
			return FindByValue(Value) != null;
		}
		
		public Node<T> GetFirst(){
			if(this.Count > 0){
			return this[0];
			}
			return null;
		}
		
		public Node<T> GetLast(){
			if(this.Count == 0){
				return null;
			}else{
				return this[this.Count-1];
			}
		}
		
		public void RemoveFirst(){
			if(this.Count > 0){
				this.RemoveAt(0);
			}else{
				return;
			}
		}
		
		public void RemoveLast(){
			if(this.Count == 0){
				return;
			}else{
				RemoveAt(this.Count-1);
			}
		}
		
		public Node<T> FindByID(int ID){
			foreach(Node<T> node in this){
				if(node.ID == ID){
					return node;
				}
			}
			return null;
		}
		
		public Node<T> FindByValue(T value){
			foreach(Node<T> node in this){
				if(node.Value.Equals(value)){
					return node;
				}
			}
			return null;
			
		}
	}
}
