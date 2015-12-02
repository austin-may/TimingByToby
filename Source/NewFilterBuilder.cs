using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace TimingForToby
{
    public partial class NewFilterBuilder : Form
    {
        //Default min for ages
        private int ageMinimum = 1;
        //Default max for ages
        private int ageMaximum = 100;
        //name of Filter
        public string filterName; 
        //list of running selecteed ages (min, max)
        private List<Tuple<int, int>> ages = new List<Tuple<int, int>>();
        //have the buttons been set
        private bool minSet = false;
        private bool maxSet = false;
        public NewFilterBuilder()
        {
            InitializeComponent();
            labelValue.Text = trackBar1.Value.ToString();
        }
        //on "Set Min" button click.  set the min for age rance
        private void BtnSetMin_Click(object sender, EventArgs e)
        {
            //Sets the potential min to the value of the trackbar if there is no value in the textbox          
            int minAge=ageMinimum;
            if (txtMinAge.Text.Trim() == "")
            {
                minAge = trackBar1.Value;
                txtMinAge.Text = minAge.ToString();
            }
            else
            {
                minAge = Int16.Parse(txtMinAge.Text.Trim());
                trackBar1.Value=minAge;
            }

            //Checks that the potential new min is less than the max
            if (minAge > ageMaximum)
            {
                MessageBox.Show("The min age must be less than the max age!");
                txtMinAge.Text = ageMinimum.ToString();
            }    
                //On a pass, set the potential min to the age minimum
            else
            {
                ageMinimum = minAge;
                txtMinAge.BackColor = Color.LimeGreen;
            }
        }

        //on "Set Max" button click.  set the max for age rance
        private void BtnSetMax_Click(object sender, EventArgs e)
        {
            int maxAge = ageMaximum;
            //Sets the potential max to the value of the trackbar if there is no value in the textbox            
            if (txtMaxAge.Text.Trim() == "")
            {
                maxAge = trackBar1.Value;
                txtMaxAge.Text = maxAge.ToString();
            }
            else
            {
                maxAge = Int16.Parse(txtMaxAge.Text.Trim());
                txtMaxAge.Text = maxAge.ToString();
            }
            //Checks that the potential new max is more than the min
            if (maxAge < ageMinimum)
            {
                MessageBox.Show("The max age must be greater than the min age!");
                txtMaxAge.Text = ageMaximum.ToString();
            }
            //On a pass, set the potential max to the age maximum
            else
            {
                ageMaximum = maxAge;
                txtMaxAge.BackColor = Color.LimeGreen;
            }
            
        }
        //creates filter (xml) if possible
        private void BtnCreateFilter_Click(object sender, EventArgs e)
        {
             /*Need to check this logic.  If filter name is entered but no check boxes are checked
              *filter name is added to the group check box in the MainWindow.
              *This should not happen! -DC 11-1-2015
              */
             if (this.filterName == null)
             {
                  //Make sure name is entered
                  MessageBox.Show("Please create a name for this filter.");
             }
             else if (!checkBox1.Checked && !checkBox2.Checked)
             {
                  MessageBox.Show("Please select age and/or gender check boxes for your filter.");
             }
             else if (checkBox1.Checked || checkBox2.Checked)
             {
                 bool success=createXMLFilter(this.filterName);
                 //Close the form if file was saved
                 if(success)
                    this.Close();
             }
             else
             {
                  MessageBox.Show("ERROR:  Please try again.");
             }
        }
        //called when the name for the filter changes
        private void TbNameBox_TextChanged(object sender, EventArgs e)
        {
            //Set the filter name 
            filterName = textBox1.Text;
        }

        //My feable attempts at creating custom filter xml's using Xml.Linq
        //Creates an xml document inside of Source/bin/Debug/Filters
        /*****NOTE:  May need to save in dofferent location to avoid problems with privileges on user's PC*****/
         //DC - 11-1-2015
        private bool createXMLFilter(string name)
        {
            XDocument doc = null;
             //get the current selected ages (if the user doesnt select the age checkbox it will be ignored anyways)
             ages.Add(new Tuple<int, int>(ageMinimum, ageMaximum));
             string ageString = "(select (strftime('%Y', 'now') - strftime('%Y', run.DOB)) - (strftime('%m-%d', 'now') < strftime('%m-%d', run.DOB))) as Age ";
            //Sex but not age is selected 
            if (this.checkBox1.Checked && !this.checkBox2.Checked)
             {
                  doc = new XDocument(new XElement("FilterSet", 
                 new XElement("Filter",
                       new XElement("Name", name + "-Male"),
                                   new XElement("SQL", "select  (select count(*) from RaceResults r2 join RaceRunner rn2 on r2.BibID = rn2.BibID join Runners run2 on rn2.RunnerID = run2.RunnerID where r2.RaceID=@RaceID AND run2.Gender=77 AND r2.Time<=r.Time order by Time) as Position," 
                                                       + "(run.FirstName || ' ' || run.LastName) as Name, CAST(Time as varchar(10)) as Time " 
                                                       + "from RaceResults r " 
                                                       + "join RaceRunner rn on r.BibID = rn.BibID "
                                                       + "join Runners run on rn.RunnerID = run.RunnerID " 
                                                       + "where r.RaceID=@RaceID AND run.Gender=77 order by Time")
                                                       ),
                 new XElement("Filter",
                       new XElement("Name", name + "-Female"),
                                   new XElement("SQL", "select  (select count(*) from RaceResults r2 join RaceRunner rn2 on r2.BibID = rn2.BibID join Runners run2 on rn2.RunnerID = run2.RunnerID where r2.RaceID=@RaceID AND run2.Gender=70 AND r2.Time<=r.Time order by Time) as Position,"
                                                       +  "(run.FirstName || ' ' || run.LastName) as Name, CAST(Time as varchar(10)) as Time "
                                                       + "from RaceResults r "
                                                       + "join RaceRunner rn on r.BibID = rn.BibID "
                                                       + "join Runners run on rn.RunnerID = run.RunnerID "
                                                       + "where r.RaceID=@RaceID AND run.Gender=70 order by Time")
                                        )
                  ));                  
             }
            //age but not sex is selected
             else if (!this.checkBox1.Checked && this.checkBox2.Checked)
             {
                 var elementList = new List<XElement>();
                 foreach(Tuple<int, int> ageRange in ages)
                 {
                     //secondary check that the min age is lower than max... if not, skip
                     if (ageRange.Item1 < ageRange.Item2)
                     {
                         elementList.Add(
                             new XElement("Filter",
                                        new XElement("Name", name + "-Ages:" + ageRange.Item1 + "-" + ageRange.Item2),
                                       new XElement("SQL", "select  (select count(*) from RaceResults r2 join RaceRunner rn2 on r2.BibID = rn2.BibID join Runners run2 on rn2.RunnerID = run2.RunnerID where r2.RaceID=@RaceID AND "
                                                           + "(select (strftime('%Y', 'now') - strftime('%Y', run2.DOB)) - (strftime('%m-%d', 'now') < strftime('%m-%d', run2.DOB)))>=" + ageRange.Item1 + " AND "
                                                           + "(select (strftime('%Y', 'now') - strftime('%Y', run2.DOB)) - (strftime('%m-%d', 'now') < strftime('%m-%d', run2.DOB)))<=" + ageRange.Item2 + " AND r2.Time<=r.Time order by Time) as Position, "
                                                           + "(run.FirstName || ' ' || run.LastName) as Name, CAST(Time as varchar(10)) as Time, "
                                                           + ageString
                                                           + "from RaceResults r "
                                                           + "join RaceRunner rn on r.BibID = rn.BibID "
                                                           + "join Runners run on rn.RunnerID = run.RunnerID "
                                                           + "where r.RaceID=@RaceID AND Age>=" + ageRange.Item1 + " AND Age<=" + ageRange.Item2 + " order by Time")

                             ));
                     }
                 }
                  doc = new XDocument(new XElement("FilterSet", elementList));
             }
            //both age and sex are selected
             else if (this.checkBox1.Checked && this.checkBox2.Checked)
             {
                 var elementList = new List<XElement>();
                 foreach(Tuple<int, int> ageRange in ages)
                 {
                     //secondary check that the min age is lower than max... if not, skip
                     if (ageRange.Item1 < ageRange.Item2)
                     {
                         elementList.Add(
                         new XElement("Filter",
                              new XElement("Name", name + "-Ages:" + ageRange.Item1 + "-" + ageRange.Item2 + "-Male"),
                                          new XElement("SQL", "select  (select count(*) from RaceResults r2 join RaceRunner rn2 on r2.BibID = rn2.BibID join Runners run2 on rn2.RunnerID = run2.RunnerID where r2.RaceID=@RaceID AND "
                                                              + "(select (strftime('%Y', 'now') - strftime('%Y', run2.DOB)) - (strftime('%m-%d', 'now') < strftime('%m-%d', run2.DOB)))>=" + ageRange.Item1 + " AND "
                                                              + "(select (strftime('%Y', 'now') - strftime('%Y', run2.DOB)) - (strftime('%m-%d', 'now') < strftime('%m-%d', run2.DOB)))<=" + ageRange.Item2 + " AND run2.Gender=77 AND r2.Time<=r.Time order by Time) as Position, "
                                                              + "(run.FirstName || ' ' || run.LastName) as Name, CAST(Time as varchar(10)) as Time, "
                                                              + ageString
                                                              + "from RaceResults r "
                                                              + "join RaceRunner rn on r.BibID = rn.BibID "
                                                              + "join Runners run on rn.RunnerID = run.RunnerID "
                                                              + "where r.RaceID=@RaceID AND run.Gender=77 AND Age>=" + ageRange.Item1 + " AND Age<=" + ageRange.Item2 + " order by Time")

                                                              ));
                         elementList.Add(new XElement("Filter",
                         new XElement("Name", name + "-Ages: " + ageRange.Item1 + "-" + ageRange.Item2 + "-Female"),
                                     new XElement("SQL", "select  (select count(*) from RaceResults r2 join RaceRunner rn2 on r2.BibID = rn2.BibID join Runners run2 on rn2.RunnerID = run2.RunnerID where r2.RaceID=@RaceID AND "
                                                         + "(select (strftime('%Y', 'now') - strftime('%Y', run2.DOB)) - (strftime('%m-%d', 'now') < strftime('%m-%d', run2.DOB)))>=" + ageRange.Item1 + " AND "
                                                         + "(select (strftime('%Y', 'now') - strftime('%Y', run2.DOB)) - (strftime('%m-%d', 'now') < strftime('%m-%d', run2.DOB)))<=" + ageRange.Item2 + " AND run2.Gender=70 AND r2.Time<=r.Time order by Time) as Position, "
                                                         + "(run.FirstName || ' ' || run.LastName) as Name, CAST(Time as varchar(10)) as Time, "
                                                         + ageString
                                                         + "from RaceResults r "
                                                         + "join RaceRunner rn on r.BibID = rn.BibID "
                                                         + "join Runners run on rn.RunnerID = run.RunnerID "
                                                         + "where r.RaceID=@RaceID AND run.Gender=70 AND Age>=" + ageRange.Item1 + " AND Age<=" + ageRange.Item2 + " order by Time")
                                          ));
                     }
                 }
                 doc = new XDocument(new XElement("FilterSet", elementList));
             }
             else
             {
                 MessageBox.Show("Please select age and/or gender check boxes for your filter.");
             }
            /*The idea is to add all of the ranges for the duration of age ranges at the end of the create filter button
             * When you press the Add Age Filter button that filter will be added to the listbox and the filter itself
             * (This part still needs to be done) - After adding a filter to add another the age range must be outside of previous existing age ranges 
             * The textboxes will also be cleared upon pressing "Add Age Filter"
             * */


             String path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Filters\" + name + ".xml";
             if (File.Exists(path))
             {
                 MessageBox.Show("File Already Exist with that name!");
                 return false;
             }
             else if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
             {
                 string error = "Name can not contain: ";
                 foreach (char e in Path.GetInvalidFileNameChars())
                 {
                     error += "\"," + e + "\"";
                 }
                 MessageBox.Show(error);
                 return false;
             }
             else if(doc!=null)
             {
                 doc.Save(path);
                 return true;
             }
             return false;
        }
        //called when the slider for the track bar moves
        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            labelValue.Text = trackBar1.Value.ToString();
        }
        //takes current age range, stores it, and allows user to add new range with min set to +1 old max
        private void BtnAdditionalAge_Click(object sender, EventArgs e)
        {
            //Add the ranges to listbox
            listBox1.Items.Add(ageMinimum + "," + ageMaximum);
            ages.Add(new Tuple<int, int>(ageMinimum, ageMaximum));
            //Add a filter for the new age range
            //createXMLFilter(this.FilterName +"_Ages"+ AgeMinimum + "-" + AgeMaximum);
            //Clear txtboxes
            txtMinAge.Clear();
            txtMaxAge.Clear();
            //Reset age range to + 1 of the previous age range.
            ageMinimum = ageMaximum +1;
            ageMaximum = 100;
            // Re enable the age ranges
            txtMaxAge.Enabled = true;
            txtMinAge.Text = (ageMinimum).ToString();
            trackBar1.Value = ageMinimum>100? 100: ageMinimum;
            //disable the ability to change the min value
            txtMinAge.Enabled = false;
            btnSetMin.Enabled = false;

            //The idea is to add all of the ranges for the duration of age ranges at the end of the create filter button
        }
        //called when max is changed, highlights cell green to let user know not set
        private void MaxChanged(object sender, EventArgs e)
        {
            txtMaxAge.BackColor = Color.White;
        }

        //called when min is changed, highlights cell green to let user know not set
        private void MinChanged(object sender, EventArgs e)
        {
            txtMinAge.BackColor = Color.White;
        }
    }
}
