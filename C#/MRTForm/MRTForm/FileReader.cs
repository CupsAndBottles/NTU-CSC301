using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace MRTForm
{
	/// <summary>
	/// Description of FileReader.
	/// </summary>
	public class FileReader
	{
		private StreamReader sr;
		private FileStream fs;
		private List<string> FileRead;
		private char DELIMITER =',';
		
		public FileReader()
		{
			FileRead = new List<string>();
		}
		
		public FileReader(string filename)
		{
			FileRead = new List<string>();
			readFile(filename);
		}
		
		public void readFile(string filename){
			
			fs = new FileStream(filename,FileMode.Open);
			sr = new StreamReader(fs);
			
			string str = String.Empty; 
			
			while((str = sr.ReadLine()) != null){
				Debug.WriteLine(str);
					FileRead.Add(str);
			}
			
			sr.Close();
			fs.Close();
		}
			
		public List<Station> GetAllStations(string filename){

			readFile(filename);
			return GetAllStations();
		}
		
		public List<Station> GetAllStations(){
			
			List<Station> aryList = new List<Station>();
			int currCount = 0;			
			
			for(int i=0;i< FileRead.Count;i++){
			string str = FileRead[i];
			
			if(str.StartsWith("[") || str.Length == 0){
				   	continue;
				}else{
				
				string[] temp = str.Split(DELIMITER);
				int index = Convert.ToInt32(temp[0]);
				
				if(currCount == index){
				Station t = new Station();
				string[] stationNumber = temp[1].Trim().Split('/');
				t.StationNumber = stationNumber;
				t.StationName = temp[2].Trim();
				aryList.Add(t);	
				currCount++;
				}
				}
			
			}
			return aryList;
		
		}	
			
		public List<int>[] GetLineLayout(string filename){
			readFile(filename);
			return GetLineLayout();
		}
			
		public List<int>[] GetLineLayout(){
			
			List<List<int>> masterList = new List<List<int>>();
			List<int> temp = new List<int>();
			
			for(int i =0;i<FileRead.Count;i++){
				string str = FileRead[i];
				
				if(str.StartsWith("[") || str.Length == 0){
					
					if(str.Equals("[End]")){//End of line
						
						//Add list to masterList
					   	masterList.Add(temp);
					   	
					}else if(str.Length == 0){//Blank Line
						continue;
					}else{
						//Re init Temp list
						temp = new List<int>();
					}
				}else{ //Normal Line
					temp.Add(Convert.ToInt32(str.Split(DELIMITER)[0]));
				}
			}
			return masterList.ToArray();
		
		}
		
	}
	
}
