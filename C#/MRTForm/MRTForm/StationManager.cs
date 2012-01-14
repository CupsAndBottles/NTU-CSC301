using System;
using System.Collections;
using System.Collections.Generic;

namespace MRTForm
{
    public class StationManager
    {
		//Contains The Entire Map as a undirected graph
       private Graph<Station> g;

        //List of all the Stations
      private List<Station> StationList;

        //Location of file holding all the stations to be read
      private const string StationListFilename = "ALL.txt";

        //List used to store the different paths found
      private  ArrayList aryPath;

        public StationManager()
        {
            g = new Graph<Station>();
            InitAllStation(StationListFilename);
        }//end constructor

        public int GetIDFromStationName(string Name)
        {
            for (int i = 0; i < StationList.Count; i++)
            {
                if (StationList[i].StationName.Equals(Name))
                {
                    return i;
                }
            }
            return -1;
        }//end GetIDFromStationName
        
        private void InitAllStation(string stationList)
        {
            FileReader fr = new FileReader(stationList);
            StationList = fr.GetAllStations();
            for (int i = 0; i < StationList.Count; i++)
            {
                Node<Station> node = new Node<Station>(i, StationList[i]);
                g.AddNode(node);
            }
            
            List<int>[] LineLayout = fr.GetLineLayout();
            
            foreach(List<int> lstLine in LineLayout){
            	for(int i =0;i< lstLine.Count-1;i++){
            		g.AddUndirectedEdge(lstLine[i],lstLine[i+1]);
            	}
            }
            
        }//end InitAllStation
        
        public Station GetStationByID(int id)
        {
            return g.GetNodeByID(id).Value;
        }//end GetStationByID

        public Path<Station>[] SearchForPath(int startStationID, int endStationID, int numOfPathsToReturn)
        {
        	//Return null if the start and end station are the same
            if (startStationID == endStationID)
            {
                return null;
            }

            //Execute Search
            DepthFirstSearch(startStationID, endStationID);

            //Return Path Arry
            Path<Station>[] returnPath;

            //If the number of possible Paths is less than the number of paths to return
            if (aryPath.Count < numOfPathsToReturn)
            {
            	//Return the number of paths of found
                return aryPath.ToArray(typeof(Path<Station>)) as Path<Station>[];
            }else{ //Number of paths found more than paths to return

            	//Initialize Path array to the number of paths to return
                returnPath = new Path<Station>[numOfPathsToReturn];

                for (int i = 0; i < numOfPathsToReturn; i++)
                {
                    returnPath[i] = aryPath[i] as Path<Station>;
                }
            }
            
            //Return Path Array
            return returnPath;
        }//end SearchForPath

        private void DepthFirstSearch(int startNodeID, int endNodeID)
        {
            //Initialize Arraylist to store all results
            aryPath = new ArrayList();

            //Initialize Path variable to store the path
            Path<Station> currPath = new Path<Station>();

            //Execute DFS
            DepthFirst2(g.GetNodeByID(startNodeID), endNodeID, currPath);

            //Sort the Paths in List based on weight variable
            aryPath.Sort();

        }//end DepthFirstSearch

        private void DepthFirst2(Node<Station> currNode, int endNodeID, Path<Station> visited)
        {

            //Add Current node to Path
            visited.Add(currNode);

            //Get All the neighbouring nodes of the Current Node
            NodeList<Station> neighbour = currNode.Neighbour;

            //Iterate through all the neighbours
            foreach (Node<Station> nextNode in neighbour)
            {

                //Continue if Node is already on list
                if (visited.Contains(nextNode))
                {
                    continue;
                }

                //Neighbour node found to be destination node
                if (nextNode.ID == endNodeID)
                {

                    //Add Node to Path
                    visited.Add(nextNode);

                    //Save Path to a List
                    aryPath.Add(new Path<Station>(visited.PathList));

                    //Remove Destination Node From Path
                    visited.RemoveLast();

                    //Break out of for-each
                    continue;
                }

                //Recursion of DFS with NextNode as current Node
                DepthFirst2(nextNode, endNodeID, visited);

                //Remove last visited node
                visited.RemoveLast();

            }//end foreach


        }//end DepthFirst2

       public string PrintResult(Path<Station> result){
		
        //string that will be returned
       	string returnStr= "";
		
		// From station to station
		const string marker = " -> ";
		
		//String for new line
		const string newLine = "\r\n";
		
		//Counter for line change
		int numOfChanges = 0;
		
		//Lists holding the current line the user is on 
		List<string> currLine = new List<string>();
		
		//First Station
		Station src = result.PathList.GetFirst().Value;
		returnStr += src.ToString() + marker;
		
		//Add all lines of source station
		foreach(string stationNum in src.StationNumber){
			currLine.Add(GetLine(stationNum));
		}
		
		//Iterate through the rest of the stations
		for(int i = 1;i < result.PathList.Count;i++){
			
			//Get Previous Station
			Station prev = result.PathList[i-1].Value;
			
			//Get Next station
			Station curr = result.PathList[i].Value;
			
			//Find common line with previous station ( 1 or many)
			List<string> commonLines = GetCommonLines(curr,prev);
			
			//Compare common vs current line
			if(CompareLines(currLine,commonLines)){//Common Lines
				
				currLine = GetCommonLines(currLine,commonLines);
							   
			   }else{ //No Lines in common
			   	
			   	//Print current Line
			   	returnStr += currLine[0] + marker;
			   	
			   	//Print Previous Station
			   	returnStr += prev.ToString() + marker;
			   	
			   	//Set Currentline to common line
			   	currLine = commonLines;
			   	
			   	//Increment The Number of Change of Lines
			   	numOfChanges++;
			   }
		}

		return returnStr +  currLine[0] + marker +  result.GetLast().Value.ToString() +  
			newLine + "Number of Stations : " + (result.Weight-1) +
			newLine +"Number Of Interchange(s) : " + numOfChanges;
		
		}
		
        public string PrintFullResult(Path<Station> result){
        	string returnResult = "";
        	const string marker = " -> ";
        	
        	for(int i =0; i< result.PathList.Count-1;i++){
        		returnResult += result.PathList[i].Value.ToString() + marker;
        	}
        	
        	return returnResult +  result.PathList.GetLast().Value.ToString();
        }
        
        /// <summary>
        /// Returns a list of Lines which is present in both stations
        /// </summary>
        /// <param name="curr">Current Station</param>
        /// <param name="prev">Previous Station</param>
        /// <returns>List of string which contains the lines in both stations</returns>
		private List<string> GetCommonLines(Station curr, Station prev){
        	//Instantiate list to return
			List<string> newList = new List<string>();
			
			//Iterate through the Current Station's Station Number(s)
			foreach(string lineCurr in curr.StationNumber){
				//Iterate through the previous Station's Station Number(s)
				foreach(string linePrev in prev.StationNumber){
					
					if(GetLine(lineCurr).Equals(GetLine(linePrev))){
						//Add the string to the return list if it is found in both lists
						newList.Add(GetLine(lineCurr));
					   }
				}
			}
			//Return the list
			return newList;
		}// end GetCommonLines
		
		private List<string> GetCommonLines(List<string> list1, List<string> list2){
			List<string> newList = new List<string>();
			
			foreach(string lineCurr in list1){
				foreach(string linePrev in list2){
					if(lineCurr.Equals(linePrev)){
						newList.Add(lineCurr);
				}
			}
		
		}
				return newList;
		}
		
		private bool CompareLines(List<string> a, List<string> b){
			
			foreach(string str in a){
				if(b.Contains(str)){
				   	return true;
				   }
			}
			return false;
		}

		private string GetLine(string str){
			return str.Substring(0,2).ToUpper();
		}
    
        public List<Station> GetListOfStation(){
        	List<Station> newList = new List<Station>();
        	
        	foreach(Station node in StationList){
        		newList.Add(node);
        	}
        	
        	return newList;
        }
	    }

}
