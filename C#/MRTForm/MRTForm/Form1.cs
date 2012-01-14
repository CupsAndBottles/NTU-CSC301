using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MRTForm
{
    public partial class Form1 : Form
    {
        StationManager stm = new StationManager();
        string startStn;
        string endStn;
        List<Station> startList,endList;
        Path<Station>[] result;
        int numOfResults = 3;

        public Form1()
        {
            InitializeComponent();
            initStationList();
        }

        private void initStationList()
        {
        	startList = stm.GetListOfStation();
        	endList = stm.GetListOfStation();
        	
        	startList.Sort();
        	endList.Sort();
        	
              this.startStation.DataSource = startList;
              this.endStation.DataSource = endList;

        }
        
        private void computeBtn_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            string newLine = "\r\n";
            startStn = ((Station)this.startStation.SelectedItem).StationName;
            endStn = ((Station)this.endStation.SelectedItem).StationName;

            if(startStn.Equals(endStn)){
                txtResult.Text = "Error: Start and End stations are the same";
            }else{

            	
                 result = stm.SearchForPath(stm.GetIDFromStationName(startStn), stm.GetIDFromStationName(endStn), numOfResults);
                
                 txtResult.Text ="Starting Station : " + result[0].GetFirst().Value.ToString() + newLine;
                 txtResult.Text +="Ending Station : " + result[0].GetLast().Value.ToString() + newLine + newLine;
                 	
                 	
                 foreach(Path<Station> pt in result)
                 {
    			 txtResult.Text += "Full Route :"+ newLine;	             
                 txtResult.Text+= stm.PrintFullResult(pt) +newLine + newLine;
                 txtResult.Text+= "Condensed Route :" + newLine;
                 txtResult.Text+= stm.PrintResult(pt)+newLine;
                 txtResult.Text += newLine;
                 }//end for
            }//end if

        }//end computeBtn_Click
        
        private void Button1Click(object sender, EventArgs e)
        {
        txtResult.Text = "";
        }
        
    }//end class
}
