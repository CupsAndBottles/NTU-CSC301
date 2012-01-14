using System;

namespace MRTForm
{
	/// <summary>
	/// Description of Station.
	/// </summary>
	public class Station : IComparable
	{
		private bool isInterChange = false;
		private string stationName;
		private string[] stationNumber;
		
		public Station()
		{
			
		}
		
		public bool IsInterChange {
			get { return isInterChange; }
			set { isInterChange = value; }
		}
		
		public string StationName {
			get { return stationName; }
			set { stationName = value;}
		}
		
		public string[] StationNumber {
			get { return stationNumber; }
			set { stationNumber = value;
				isInterChange = (stationNumber.Length > 1);}
		}
		
		public override string ToString()
		{
		return StationName;
		}

        public int CompareTo(Object obj)
        {
            Station temp = (Station)obj;
            return this.StationName.CompareTo(temp.stationName);
        }//end compareto

	}//end class
}//end namespace
