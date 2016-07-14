using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Diagnostics;
using VisionInterfaces;
using System.ServiceModel;

namespace NET_Project
{
    public partial class Form1 : Form
    {
        private static ServiceHost _server;
        private Display HGraphics;
        private HTuple AcqHandle = new HTuple();
        public double LastMouseClickX { get; set; }
        public double LastMouseClickY { get; set; }
        public Form1()
        {
            InitializeComponent();
            HGraphics = new Display(hWindowControl);
            HOperatorSet.SetSystem("clip_region", "false");
            _server = new ServiceHost(new VisionService());
            _server.Open();
            
        }


        private void fToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string FilePath;
            HImage Img = new HImage();
            
            //Open Dialog to let user select image
            if(openImg.ShowDialog() == DialogResult.OK)
            {
                HGraphics.allObj.Clear();
                Control.CheckForIllegalCrossThreadCalls = false;
                //Display the file path on the title
                this.Text = openImg.FileName;
                FilePath = openImg.FileName.Replace("\\", "/");
                Img.ReadImage(FilePath);
                hWindowControl.SetFullImagePart(Img);
                //Display the image on HALCON window
                HGraphics.allObj["pic"] = new HObject(Img);
                HGraphics.display();
                Img.Dispose();
                LogTextBox.Text += FilePath + "\n";
                //Scroll log to the bottom
                LogTextBox.SelectionStart = LogTextBox.Text.Length;
                LogTextBox.ScrollToCaret();
            }
        }
        
        private void hWindowControl_HMouseMove(object sender, HMouseEventArgs e)
        {
            HTuple X = new HTuple(), Y = new HTuple(), GrayVal = new HTuple();
            HTuple button = new HTuple();
            //Read mouse position
            this.MousePosRow.Text = "Row: " + Convert.ToInt16(e.Y).ToString();
            this.MousePosCol.Text = "Col: " + Convert.ToInt16(e.X).ToString();
        }

        private void hWindowControl_HMouseDown(object sender, HMouseEventArgs e)
        {
            //Display click button
            if (e.Button == MouseButtons.Left)
            {
                this.LastMouseClickX = e.X;
                this.LastMouseClickY = e.Y;
                LogTextBox.Text += "Left Click: ";
            }
            else if (e.Button == MouseButtons.Right)
            {
                LogTextBox.Text += "Right Click: ";
                //if needed, you can use this code to display context menu by right click on the HALCON window
                contextMenuStrip1.Show(this, 
                    new System.Drawing.Point(Form.MousePosition.X - this.Location.X - 6, Form.MousePosition.Y - this.Location.Y - 28));
                Log(HGraphics.ListKeys());
            }
            else if (e.Button == MouseButtons.Middle)
            {
                LogTextBox.Text += "Middle Click: ";
                for (int i = 0; i < HGraphics.allObj.Count; i++)
                {
                    Log(HGraphics.allObj.ElementAt(i).Key);
                }
            }
            //Display click position
            LogTextBox.Text += "(" + Math.Round(e.Y, 3).ToString() + ", " + Math.Round(e.X, 3).ToString() + ")\n";
            //Scroll the textbox automatically to the bottom
            LogTextBox.SelectionStart = LogTextBox.Text.Length;
            LogTextBox.ScrollToCaret();
            

            //XML file Test
            using (XmlWriter writer = XmlWriter.Create("Config.xml"))
            {
                writer.WriteStartElement("Configuration");
                writer.WriteStartElement("Configuration");
                writer.WriteElementString("ID", "0983");
                writer.WriteEndElement();
                writer.WriteStartElement("Configuration");
                writer.WriteElementString("ID", "0984");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                /*String directoryName = "C:\\Consolidated";
DirectoryInfo dirInfo = new DirectoryInfo(directoryName);
if (dirInfo.Exists == false)
    Directory.CreateDirectory(directoryName);

List<String> MyMusicFiles = Directory
                   .GetFiles("C:\\Music", "*.*", SearchOption.AllDirectories).ToList();

foreach (string file in MyMusicFiles)
{
    FileInfo mFile = new FileInfo(file);
    // to remove name collusion
    if (new FileInfo(dirInfo + "\\" + mFile.Name).Exists == false) 
         mFile.MoveTo(dirInfo + "\\" + mFile.Name);
}
                 * 
                 * 
                 * 
                 string source = @"C:\Music";
string[] directories = Directory.GetDirectories(source);
string consolidated = Path.Combine(source, "Consolidated")
Directory.CreateDirectory(consolidated);
foreach(var directory in directories) {
    Directory.Move(directory, consolidated);
}*/
            }
        }

        #region Event for Ending Region Select
        private delegate void MatchingEvent();
        private event MatchingEvent EndRegionSelect;
        protected virtual void OnEndRegionSelect()
        {
            if (EndRegionSelect != null)
            {
                EndRegionSelect();
            }
        }
        #endregion

        private void SelectRegionButton_Click(object sender, EventArgs e)
        {
            //Execute the function DisplayCross whenever the mouse is moving
            if (SelectRegionButton.Text == "Select Region")
            {
                this.hWindowControl.HMouseMove += DisplayRegion;
                this.hWindowControl.HMouseDown += GetRegion;
                SelectRegionButton.Text = "End Region Select";
                menuStrip1.Enabled = false; //disables model menu
                LogTextBox.Text += "**Use right click to end region selection**\n";
            }
            else if (SelectRegionButton.Text == "End Region Select")
            {
                //Reset the tuples that stores the click positions, so that the user will have a new region next time
                SelectRegionRows = new HTuple();
                SelectRegionCols = new HTuple();
                this.hWindowControl.HMouseMove -= DisplayRegion;
                this.hWindowControl.HMouseDown -= GetRegion;
                SelectRegionButton.Text = "Select Region";
                HGraphics.allObj.Remove("green_cursor");
                HGraphics.display();
                menuStrip1.Enabled = true;//enables model menu
                if (HGraphics.allObj["colored_region"].CountObj() == 0)
                {
                    HGraphics.allObj.Remove("colored_region");
                }
                OnEndRegionSelect(); //Event to trigger matching algorithms after creation of region
            }
            
        }
        //This function displays the cursor and the selected region
        private void DisplayRegion(object sender, HMouseEventArgs e)
        {
            //This block draws a cross on the tip of the mouse for visualisation purpose
            HObject temp;
            HOperatorSet.GenContourPolygonXld(out temp, new HTuple(0, (double)hWindowControl.ImagePart.Height), new HTuple(e.X, e.X));
            HGraphics.allObj["green_cursor"] = temp;
            HOperatorSet.GenContourPolygonXld(out temp, new HTuple(e.Y, e.Y), new HTuple(0, hWindowControl.ImagePart.Width));
            HGraphics.allObj["green_cursor"] = HGraphics.allObj["green_cursor"].ConcatObj(temp);

            //Declare variables to store region and the contour of the region
            HObject Contours = new HObject();
            Contours.GenEmptyObj();
            HObject RegionChanged = new HObject();
            RegionChanged.GenEmptyObj();
            for (int i = 0; i < SelectRegionRows.Length; i += 2)
            {
                if (i == SelectRegionRows.Length - 1)
                {
                    HOperatorSet.GenContourPolygonXld(out temp, new HTuple((double)SelectRegionRows[i], e.Y, e.Y, (double)SelectRegionRows[i], (double)SelectRegionRows[i]),
                        new HTuple((double)SelectRegionCols[i], (double)SelectRegionCols[i], e.X, e.X, (double)SelectRegionCols[i]));
                }
                else
                {
                    HOperatorSet.GenContourPolygonXld(out temp, new HTuple((double)SelectRegionRows[i], (double)SelectRegionRows[i + 1], (double)SelectRegionRows[i+1], (double)SelectRegionRows[i], (double)SelectRegionRows[i]),
                        new HTuple((double)SelectRegionCols[i], (double)SelectRegionCols[i], (double)SelectRegionCols[i + 1], (double)SelectRegionCols[i+1], (double)SelectRegionCols[i]));
                }
                HOperatorSet.GenRegionContourXld(temp, out Contours, "margin");
                HOperatorSet.ConcatObj(Contours, RegionChanged, out RegionChanged);
                
            }
            HGraphics.allObj["colored_region"] = RegionChanged;
            HGraphics.display();
        }
        
        //This function records all the click positions and stores them in a Tuple
        HTuple SelectRegionRows = new HTuple();
        HTuple SelectRegionCols = new HTuple();
        private void GetRegion(object sender, HMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SelectRegionRows.Append(e.Y);
                SelectRegionCols.Append(e.X);
            }
        }
        
        #region Matching
        //This function allows the user to create a matching model using an input image and region. If no image
        //or region exists when clicking the button, it will ask the user to input them first
        private void createModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HGraphics.keepOnly(new string[] { "pic" , "colored_region"});
            HGraphics.display();
            if (HGraphics.allObj.ContainsKey("pic") == false) //check if an image already exists
            {
                fToolStripMenuItem.PerformClick(); //if not, open file dialog and select an image file
            }

            if (HGraphics.allObj.ContainsKey("colored_region") && HGraphics.allObj.ContainsKey("pic") == true) //check if a region already exists
            {
                //if yes, ask if the user wants to create a new region
                DialogResult result = MessageBox.Show("A region has been created before. Do you want to create a new region?", "Matching Model Region", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    HGraphics.allObj.Remove("colored_region");
                    HGraphics.allObj.Remove("green_matchingRegion");
                    HGraphics.allObj.Remove("green_cross");
                    HGraphics.display();
                    SelectRegionButton.Text = "Select Region";
                    SelectRegionButton.PerformClick();
                    //If the user choose to create a new region, run MatchingInitialize after the user ends region selection
                    EndRegionSelect += MatchingInitialize;
                }
                //if no, run region creation function
                else if (result == DialogResult.No)
                {
                    //Run MatchingInitialize directly
                    MatchingInitialize();
                }
            }
            else if(HGraphics.allObj.ContainsKey("pic") == true)
            {
                SelectRegionButton.Text = "Select Region";
                SelectRegionButton.PerformClick();
                EndRegionSelect += MatchingInitialize;
            }
            
        }

        MatchingParameters Form2;//Remember to dispose this when finish creating model

        //This function initializes the matching parameter form
        private void MatchingInitialize()
        {
            if (HGraphics.allObj.ContainsKey("colored_region"))//check if region is empty
            {
                EndRegionSelect -= MatchingInitialize;// once MatchingInitialize() is executed, unsubscribe from the event EndRegionSelect
                if (Form2 == null)
                {
                    Form2 = new MatchingParameters();
                }
                else
                {
                    DisposeMatchingForm();
                    Form2 = new MatchingParameters();
                }
                try
                {
                    Form2.Show();
                }
                catch
                {
                    Form2 = new MatchingParameters();
                    Form2.Show();
                }

                HGraphics.allObj["green_matchingRegion"] = CalculateShapeModel(30);
                HGraphics.display();
                Form2.contrastBar.Scroll += InteractiveContrastSelection;
                Form2.CentroidRadio.Checked = true;//Use centroid as model center


                CalculateModelCenter(HGraphics.allObj["green_matchingRegion"], Form2);

                //Subscriptions **remember to unsubscribe**
                hWindowControl.HMouseDown += ManualCenterSelection;
                Form2.hWindowControl1.HMouseDown += ManualCenterSelection;
                Form2.CentroidRadio.CheckedChanged += AutoCenterSelection;
                Form2.CreateModel.Click += CreateModelOK;
                Form2.Cancel.Click += CreateModelCancel;
            }
        }

        /*ManualCenterSelection allows the user to select the center of the model manually by clicking the window control.
         * It stores the position in ModelCenterRow and ModelCenterCol in the Matching parameter form*/
        private void ManualCenterSelection(object sender, HMouseEventArgs e)
        {
            if (!Form2.IsDisposed)
            {
                if (Form2.ManualRadio.Checked)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (sender == hWindowControl)
                        {
                            Form2.ModelCenterRow = e.Y;
                            Form2.ModelCenterCol = e.X;

                            int x, y;
                            x = Convert.ToInt16((double)e.Y);
                            y = Convert.ToInt16((double)e.X);

                            Form2.hWindowControl1.HalconWindow.SetPart(x - 30, y - 30, x + 30, y + 30);

                            DisplayCross(HGraphics, e.Y, e.X);
                            DisplayCross(Form2.HGraphics, e.Y, e.X);
                        }

                        if (sender == Form2.hWindowControl1)
                        {
                            double r,c;
                            int b;
                            Form2.hWindowControl1.HalconWindow.GetMpositionSubPix(out r, out c, out b);
                            if (b == 1)
                            {
                                Form2.ModelCenterRow = r;
                                Form2.ModelCenterCol = c;
                                DisplayCross(HGraphics, r, c);
                                DisplayCross(Form2.HGraphics, r, c);
                            }
                        }
                    }
                }
            }
            else
            {
                hWindowControl.HMouseDown -= ManualCenterSelection;
                Form2.hWindowControl1.HMouseDown -= ManualCenterSelection;
            }
        }
        /*AutoCenterSelection caluclates the centroid of the model and force the model center to be the centroid*/
        private void AutoCenterSelection(object sender, EventArgs e)
        {
            if (Form2.CentroidRadio.Checked)
            {
                CalculateModelCenter(CalculateShapeModel(Form2.contrastBar.Value), Form2);
            }
        }

        private void InteractiveContrastSelection(object sender, ScrollEventArgs e)
        {
            CalculateModelCenter(CalculateShapeModel(e.NewValue), Form2);
        }

        //The following function draws a green cross object stored in HGraphics.allObj["green_cross"] with center given in the input row and col
        private void DisplayCross(Display HG, double row, double col)
        {
            HObject temp;
            HOperatorSet.GenContourPolygonXld(out temp, new HTuple(0, (double)hWindowControl.ImagePart.Height), new HTuple(col, col));
            HG.allObj["green_cross"] = temp;
            HOperatorSet.GenContourPolygonXld(out temp, new HTuple(row, row), new HTuple(0, hWindowControl.ImagePart.Width));
            HG.allObj["green_cross"] = HGraphics.allObj["green_cross"].ConcatObj(temp);
            HG.display();
        }

        private HObject CalculateShapeModel(int Contrast)
        {
            HObject ImageReduced, Domain, ResultRegion, ResultImage;

            HOperatorSet.FillUp(HGraphics.allObj["colored_region"], out Domain);
            HOperatorSet.Union1(Domain, out Domain);
            HOperatorSet.ReduceDomain(HGraphics.allObj["pic"], Domain, out ImageReduced);
            HOperatorSet.InspectShapeModel(ImageReduced, out ResultImage, out ResultRegion, 1, Contrast);
            HGraphics.allObj["green_matchingRegion"] = ResultRegion;
            HGraphics.display();

            return ResultRegion;
        }

        private void CalculateModelCenter(HObject Region, MatchingParameters Form2)
        {
            HTuple area, row, col;
            HOperatorSet.AreaCenter(Region, out area, out row, out col);
            Form2.CentroidRow = row;
            Form2.CentroidCol = col;
            Form2.ModelCenterRow = row;
            Form2.ModelCenterCol = col;

            DisplayCross(HGraphics, row, col);

            int x, y;
            x = Convert.ToInt16((double)row);
            y = Convert.ToInt16((double)col);
            Form2.hWindowControl1.HalconWindow.SetPart(x - 30, y - 30, x + 30, y + 30);

            //allows the window control in Form2 to display the same picture and other graphics
            Form2.HGraphics.allObj["pic"] = HGraphics.allObj["pic"];
            Form2.HGraphics.allObj["green_cross"] = HGraphics.allObj["green_cross"];
            Form2.HGraphics.allObj["green_matchingRegion"] = HGraphics.allObj["green_matchingRegion"];
            Form2.HGraphics.allObj["colored_region"] = HGraphics.allObj["colored_region"];
            Form2.HGraphics.display();
        }

        private void CreateModelOK(object sender, EventArgs e)
        {
            
            HObject ImageReduced, ReducedDomain;
            HTuple ModelID;
            HOperatorSet.FillUp(HGraphics.allObj["colored_region"], out ReducedDomain);
            HOperatorSet.Union1(ReducedDomain, out ReducedDomain);
            HOperatorSet.ReduceDomain(HGraphics.allObj["pic"], ReducedDomain, out ImageReduced);

            //create shape model to check if it can be created successfully. If exception occurs, display error message
            try
            {
                HOperatorSet.CreateShapeModel(ImageReduced, "auto", Form2.AngleStart, Form2.AngleExtent, "auto",
                    Form2.OptimizationComboBox.SelectedItem.ToString().Replace(' ', '_'), "ignore_local_polarity",
                    Form2.contrastBar.Value, "auto", out ModelID);

                if (saveShapeModel.ShowDialog() == DialogResult.OK)
                {
                    Form2.serialize(saveShapeModel.FileName);
                    Log("Model saved to " + saveShapeModel.FileName);
                    DisposeMatchingForm();
                }

                
            }
            catch (Exception ex)
            {
                Form2.ErrorMessage.Text = ex.Message;
            }

        }

        private void CreateModelCancel(object sender, EventArgs e)
        {
            HGraphics.allObj.Remove("colored_region");
            HGraphics.allObj.Remove("green_matchingRegion");
            HGraphics.allObj.Remove("green_cross");
            HGraphics.display();
            DisposeMatchingForm();
        }
        //Close Matching parameter form and unsubscribe all the event delegates
        public void DisposeMatchingForm()
        {
            Form2.Close();
            hWindowControl.HMouseDown -= ManualCenterSelection;
            Form2.hWindowControl1.HMouseDown -= ManualCenterSelection;
            Form2.CentroidRadio.CheckedChanged -= AutoCenterSelection;
            Form2.CreateModel.Click -= CreateModelOK;
            Form2.Cancel.Click -= CreateModelCancel;
            Form2.FindButton.Click -= findModelClick;
            Form2.HGraphics = null;
            Form2.Dispose();
        }
        //viewModel_Click calls up open file dialog(.mod) and deserialize the file to MatchingModelParam, which contains graphics of
        //the matching model. Graphics are copied to HGraphics and displayed
        private void viewModel_Click(object sender, EventArgs e)
        {
            try
            {
                if (openShapeModel.ShowDialog() == DialogResult.OK)
                {
                    MatchingModelParam param = MatchingParameters.deserialize(openShapeModel.FileName);
                    HGraphics.allObj = param.allObj;
                    hWindowControl.SetFullImagePart(new HImage(HGraphics.allObj["pic"]));
                    HGraphics.display();
                    Log("Open model " + Path.GetFileName(openShapeModel.FileName));
                }
            }
            catch (Exception ex)
            {
                Log(ex.ToString() + "\n");
            }
        }
        

        private void Log(string logString)
        {
            LogTextBox.Text += logString + "\n";
            LogTextBox.SelectionStart = LogTextBox.Text.Length;
            LogTextBox.ScrollToCaret();
        }
        /*findModelToolStripMenuItem_Click creates a MatchingParameters form and let user change matching parameters and
         find model*/
        private void findModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openShapeModel.ShowDialog() == DialogResult.OK)
                {
                    HGraphics.keepOnly(new string[] { "pic"});
                    HGraphics.display();
                    if (Form2 != null)
                    {
                        DisposeMatchingForm();
                    }
                    Form2 = new MatchingParameters(MatchingParameters.deserialize(openShapeModel.FileName));
                    Form2.Show();
                    Form2.FindButton.Click += findModelClick;
                }
            }
            catch (Exception ex)
            {
                Log(ex.ToString() + "\n");
            }
        }
        
        private void findModelClick(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            HGraphics.display();
            HTuple Row, Column, Angle, Score;
            HOperatorSet.SetColor(hWindowControl.HalconWindow, "green");

            HObject ImageReduced = HGraphics.allObj["pic"].Clone();
            if (HGraphics.allObj.ContainsKey("colored_region"))
            {
                HObject reg;
                HOperatorSet.FillUp(HGraphics.allObj["colored_region"], out reg);
                HOperatorSet.Union1(reg, out reg);
                HOperatorSet.ReduceDomain(HGraphics.allObj["pic"], reg, out ImageReduced);
                reg.Dispose();
            }
            Form2.findModel(ImageReduced, out Row, out Column, out Angle, out Score);
            
            HOperatorSet.DispCross(hWindowControl.HalconWindow, Row, Column, 100, 0);
            stopwatch.Stop();
            string s = string.Format("\n{0,15} {1,15} {2,15} {3,15}\n", "Row", "Col", "Phi", "Score");

            for (int i = 0; i < Score.Length; i++)
            {
                s += string.Format("{0,15} {1,15} {2,15} {3,15}\n", Math.Round(Row[i].D,2).ToString(), Math.Round(Column[i].D,2).ToString(),
                    Math.Round(Angle[i].D, 2).ToString(), Math.Round(Score[i].D, 2).ToString());
                
            }
            Log(s);
            Log("Instance found: " + Score.Length.ToString());
            Log("Duration: " + stopwatch.Elapsed.TotalSeconds.ToString() + " s");
            ImageReduced.Dispose();
        }

        #endregion

        #region Image Acquisition
        public void liveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AcqHandle.Length > 0)
            {
                HObject Image;
                HOperatorSet.SetFramegrabberParam(AcqHandle, "ExposureTimeRaw", 70000);
                HOperatorSet.GrabImageStart(AcqHandle, -1);
                {
                    HOperatorSet.GrabImageAsync(out Image, AcqHandle, -1);
                    HGraphics.keepOnly(new string[] { });
                    HGraphics.allObj["pic"] = Image;
                    HGraphics.SetFullImagePart(Image);
                    HGraphics.display();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Log("Initializing Camera...");
            Log("Please wait...");
            Timer CamInitDelay = new Timer();
            CamInitDelay.Tick +=CamInitDelay_Tick;
            CamInitDelay.Interval = 1000;
            CamInitDelay.Start();
        }
        private void CamInitDelay_Tick(object sender, EventArgs e)
        {
            HTuple Information, InfoBoards, AvailableDevices, MisconfiguredDevices, DeviceName, Suggestions, Generic, TmpString, MAC;
            ((Timer)sender).Stop();
            ((Timer)sender).Dispose();
            try
            {
                HOperatorSet.InfoFramegrabber("GigEVision", "info_boards", out Information, out InfoBoards);
                Log("Device Found: "+InfoBoards.Length.ToString());
                HOperatorSet.TupleRegexpSelect(InfoBoards, "status:misconfigured", out MisconfiguredDevices);
                Log("Misconfigured Device: " + MisconfiguredDevices.Length.ToString());
                if (MisconfiguredDevices.Length > 0)
                {
                    Log("Fixing misconfigured devices. ");
                    HOperatorSet.TupleRegexpMatch(MisconfiguredDevices, "suggestion:[^ ]+", out Suggestions);
                    Log(Suggestions.ToString());
                    for(int i = 0; i<Suggestions.Length; i++)
                    {
                        HOperatorSet.TupleStrLastN(Suggestions, 11, out Generic);
                        HOperatorSet.TupleStrLastN(Generic, 11, out TmpString);
                        HOperatorSet.TupleStrFirstN(TmpString, 11, out MAC);
                        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "progressive", -1, "default", Generic, "false", "default", MAC, 0, -1, out TmpString);
                        HOperatorSet.CloseFramegrabber(TmpString);
                    }
                }

                HOperatorSet.InfoFramegrabber("GigEVision", "info_boards", out Information, out InfoBoards);
                HOperatorSet.TupleRegexpSelect(InfoBoards, "status:available", out AvailableDevices);
                if (AvailableDevices.Length > 0)
                {
                    HOperatorSet.TupleRegexpMatch(AvailableDevices, "device:[^ ]+", out DeviceName);
                    HOperatorSet.TupleStrLastN(DeviceName, 7, out DeviceName);
                    Log(DeviceName.ToString());
                    for (int i = 0; i < DeviceName.Length; i++)
                    {
                        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", DeviceName[i], 0, -1, out TmpString);
                        AcqHandle.TupleConcat(TmpString);
                        AcqHandle[i] = TmpString;
                    }
                }

                //HOperatorSet.OpenFramegrabber ("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "00305313ce47_Basler_acA250014gm", 0, -1, out AcqHandle);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        #endregion

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (HGraphics.allObj.ContainsKey("pic"))
            {
                saveImageToolStripMenuItem.Enabled = true;
            }
            else
            {
                saveImageToolStripMenuItem.Enabled = false;
            }
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if(!Directory.Exists(Directory.GetCurrentDirectory() + "\\tempPic"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\tempPic");
            }
            string fileName = Directory.GetCurrentDirectory().Replace("\\", "/") + "/tempPic/Capture" + DateTime.Now.ToString("HH-mm-ss");
            Log("Saved to " + fileName);
            try
            {
                HOperatorSet.WriteImage(HGraphics.allObj["pic"], "bmp", 0, fileName);
                Log("Saved to " + fileName);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            
        }

        private void executeCalibrationCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Timer t = new Timer();
            t.Interval = 500;
            t.Tick += FindCalibObjPose;
            t.Start();
            HOperatorSet.SetFont(hWindowControl.HalconWindow, "-Courier New-16-*-*-*-*-1-");
            HGraphics.keepOnly(new string[] { });
        }

        private void FindCalibObjPose(Object sender, EventArgs e)
        {
            HTuple ImgPath = "D:/Tool/Calvin/NET_Project/caltab/";
            HTuple StartCamPar = new HTuple(0.012, 0, 0.0000022, 0.0000022, 2592 / 2, 1944 / 2, 2592, 1944);//(Focus, Kappa, Sx, Sy, Cx, Cy, Width, Height)
            HTuple CameraType = "area_scan_division";
            HTuple PoseInfo = new HTuple("TransX", "TransY", "TransZ", "RotationX", "RotationY", "RotationZ", "Type");
            HTuple PoseUnit = new HTuple("mm", "mm", "mm", "deg", "deg", "deg", "");
            HTuple CalibDataID, objInCameraPose;
            HOperatorSet.SetDraw(hWindowControl.HalconWindow, "margin");
            //HOperatorSet.GenCaltab(7, 7, 0.005, 0.5, ImgPath + "caltab.descr", ImgPath + "caltab.ps");
            HOperatorSet.CreateCalibData("calibration_object", 1, 1, out CalibDataID);
            HOperatorSet.SetCalibDataCamParam(CalibDataID, 0, CameraType, StartCamPar);
            HOperatorSet.SetCalibDataCalibObject(CalibDataID, 0, ImgPath + "caltab.descr");

            if (AcqHandle.Length > 0)
            {
                HObject Image;
                HOperatorSet.SetFramegrabberParam(AcqHandle, "ExposureTimeRaw", 100000);
                HOperatorSet.GrabImageStart(AcqHandle, -1);
                {
                    HOperatorSet.GrabImageAsync(out Image, AcqHandle, -1);

                    HGraphics.allObj["pic"] = Image;
                    HGraphics.SetFullImagePart(Image);
                }
            }

            try
            {

                HOperatorSet.FindCalibObject(HGraphics.allObj["pic"], CalibDataID, 0, 0, 1, new HTuple(), new HTuple());
                HOperatorSet.GetCalibDataObservPose(CalibDataID, 0, 0, 1, out objInCameraPose);

                HOperatorSet.SetSystem("flush_graphic", "false");
                HOperatorSet.DispObj(HGraphics.allObj["pic"], hWindowControl.HalconWindow);
                HOperatorSet.SetColor(hWindowControl.HalconWindow, "green");
                HOperatorSet.DispCaltab(hWindowControl.HalconWindow, ImgPath + "caltab.descr", StartCamPar, objInCameraPose, 1);
                objInCameraPose[0] = objInCameraPose[0] * 1000;
                objInCameraPose[1] = objInCameraPose[1] * 1000;
                objInCameraPose[2] = objInCameraPose[2] * 1000;

                HDevelopExport.disp_message(hWindowControl.HalconWindow, (PoseInfo + ": " + objInCameraPose.TupleRound() + "." + (objInCameraPose.TupleSub(objInCameraPose.TupleRound()).TupleAbs() * 1000).TupleRound() + " " + PoseUnit).TupleSelectRange(0, 5), "window", 12, 12, "green", "false");
                HOperatorSet.SetSystem("flush_graphic", "true");
                HOperatorSet.DispLine(hWindowControl.HalconWindow, -4, -4, -4, -4);
            }

            catch (Exception ex)
            {
                HOperatorSet.SetSystem("flush_graphic", "false");
                HOperatorSet.DispObj(HGraphics.allObj["pic"], hWindowControl.HalconWindow);
                HOperatorSet.SetColor(hWindowControl.HalconWindow, "green");
                HDevelopExport.disp_message(hWindowControl.HalconWindow, PoseInfo.TupleSelectRange(0, 5) + ": ???", "window", 12, 12, "green", "false");
                HOperatorSet.SetSystem("flush_graphic", "true");
                HOperatorSet.DispLine(hWindowControl.HalconWindow, -4, -4, -4, -4);

            }
        }

        private void analyzeDefectBlobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            HObject Image;

            //Initialization
            //Grab image from file and convert to gray image, delete all other display objects in HGraphics
            fToolStripMenuItem.PerformClick();
            
            HOperatorSet.Rgb1ToGray(HGraphics.allObj["pic"], out Image);
            HGraphics.keepOnly(new string[] { });
            HGraphics.allObj["pic"] = Image.Clone();

                    }

        private void Execute_Click(object sender, EventArgs e)
        {
            HTuple Width, Height, AbsoluteHisto;
            HObject Rectangle, Image;
            Image = HGraphics.allObj["pic"].Clone();
            //Calculate absolute histogram of the image
            HOperatorSet.GetImageSize(HGraphics.allObj["pic"], out Width, out Height);
            HOperatorSet.GenRectangle1(out Rectangle, 0, 0, Height - 1, Width - 1);
            HOperatorSet.GrayHistoAbs(Rectangle, HGraphics.allObj["pic"], 1, out AbsoluteHisto);


            //find the gray value with the greatest frequency
            HTuple Indices, BackgroundGrayValue;
            HOperatorSet.TupleSortIndex(AbsoluteHisto, out Indices);
            HOperatorSet.TupleInverse(Indices, out Indices);

            BackgroundGrayValue = Indices.I;
            if (BackgroundGrayValue > 255 - 21)
            {
                BackgroundGrayValue = 255 - 20;
            }

            //Thresholding
            HTuple DilationRadius = 5;
            HObject TempRegion, TempContour;
            HOperatorSet.Threshold(HGraphics.allObj["pic"], out TempRegion, new HTuple(BackgroundGrayValue.I + 20, 0), new HTuple(255, BackgroundGrayValue.I - 20));
            HOperatorSet.Connection(TempRegion, out TempRegion);
            HOperatorSet.SelectShape(TempRegion, out TempRegion, "area", "and", 30, 999999);
            HOperatorSet.Union1(TempRegion, out TempRegion);
            HOperatorSet.DilationCircle(TempRegion, out TempRegion, DilationRadius);
            HOperatorSet.FillUp(TempRegion, out TempRegion);
            HOperatorSet.GenContourRegionXld(TempRegion, out TempContour, "border");
            HOperatorSet.SmoothContoursXld(TempContour, out TempContour, 5);
            HOperatorSet.GenRegionContourXld(TempContour, out TempRegion, "filled");
            HOperatorSet.ErosionCircle(TempRegion, out TempRegion, 5);
            HGraphics.allObj["1_defectregion_erosion_red_margin_thick"] = TempRegion.Clone();
            HOperatorSet.Union1(TempRegion, out TempRegion);

            HOperatorSet.ReduceDomain(Image, TempRegion, out Image);

            HObject DiffOfGauss, RegionCrossings;
            HOperatorSet.DiffOfGauss(Image, out DiffOfGauss, 4, 50);
            HOperatorSet.DualThreshold(DiffOfGauss, out RegionCrossings, 20, 11, 11);
            HGraphics.allObj["crossingregion_colored_margin"] = RegionCrossings;
            HGraphics.display();

            HTuple Number, Area, Row, Column, Convexity, Min, Max, Range, Mean, Deviation, Circularity;
            HTuple DefectInfo = new HTuple("XY", "Size", "Max", "Min", "Mean", "Deviation", "Circularity", "Convexity");

            HOperatorSet.CountObj(HGraphics.allObj["1_defectregion_erosion_red_margin_thick"], out Number);
            HOperatorSet.SetFont(hWindowControl.HalconWindow, "-Arial-12-*-0.5-*-*-1-ANSI_CHARSET-");
            for (int Index = 1; Index <= Number; Index++)
            {
                HOperatorSet.SelectObj(HGraphics.allObj["1_defectregion_erosion_red_margin_thick"], out TempRegion, Index);
                HOperatorSet.AreaCenter(TempRegion, out Area, out Row, out Column);
                HOperatorSet.Convexity(TempRegion, out Convexity);
                HOperatorSet.MinMaxGray(TempRegion, Image, 0, out  Min, out Max, out Range);
                HOperatorSet.Intensity(TempRegion, Image, out Mean, out Deviation);
                HOperatorSet.RegionFeatures(TempRegion, "circularity", out Circularity);

                HTuple Unit = new HTuple("(" + Row + ", " + Column + ")", Area, Max, Min, Mean, Deviation, Circularity, Convexity);

                HDevelopExport.disp_message(hWindowControl.HalconWindow, DefectInfo + ": " + Unit, "image", Row, Column, "green", "false");

            }

            HOperatorSet.SetFont(hWindowControl.HalconWindow, "-Courier New-18-*-*-*-*-1-");
            HDevelopExport.disp_message(hWindowControl.HalconWindow, "Number of Defect: " + Number, "window", 12, 12, "green", "false");

        }

        public void GetUserPointInput()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;

            this.LastMouseClickX = hWindowControl.ImagePart.Width / 2.0;
            this.LastMouseClickY = hWindowControl.ImagePart.Height / 2.0;
            DisplayCross(HGraphics, hWindowControl.ImagePart.Height/2.0, hWindowControl.ImagePart.Width/2.0);
            hWindowControl.HMouseDown += ChoosePoint;
            
            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);
            }
            hWindowControl.PreviewKeyDown += ChoosePointArrow;
        }

        void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }

        private void ChoosePointArrow(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case(Keys.Up):
                    {
                        LastMouseClickY -= 0.5;
                        break;
                    }
                case (Keys.Down):
                    {
                        LastMouseClickY += 0.5;
                        break;
                    }
                case (Keys.Left):
                    {
                        LastMouseClickX -= 0.5;
                        break;
                    }
                case (Keys.Right):
                    {
                        LastMouseClickX += 0.5;
                        break;
                    }
                case (Keys.Enter):
                    {
                        EndChoosePoint();
                        break;
                    }

            }

            DisplayCross(HGraphics, LastMouseClickY, LastMouseClickX);
        }

        private void ChoosePoint(object sender, HMouseEventArgs e)
        {
            //Display click button
            if (e.Button == MouseButtons.Left)
            {
                DisplayCross(HGraphics, e.Y, e.X);
            }
            else if (e.Button == MouseButtons.Right)
            {
                EndChoosePoint();
            }
        }

        private void EndChoosePoint()
        {
            VisionService._messageReceived.Set();
            hWindowControl.HMouseDown -= ChoosePoint;
            hWindowControl.PreviewKeyDown -= ChoosePointArrow;
            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown -= new PreviewKeyDownEventHandler(control_PreviewKeyDown);
            }
        }

        public void FindModel(string fileName, out HTuple row, out HTuple col, out HTuple angle, out HTuple score)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            HGraphics.display();
            HOperatorSet.SetColor(hWindowControl.HalconWindow, "green");

            MatchingModelParam modelParam = MatchingParameters.deserialize(fileName);
            modelParam.findModel(HGraphics.allObj["pic"], out row, out col, out angle, out score);

            HOperatorSet.DispCross(hWindowControl.HalconWindow, row, col, 100, 0);
            stopwatch.Stop();
            string s = string.Format("\n{0,15} {1,15} {2,15} {3,15}\n", "Row", "Col", "Phi", "Score");

            for (int i = 0; i < score.Length; i++)
            {
                s += string.Format("{0,15} {1,15} {2,15} {3,15}\n", Math.Round(row[i].D, 2).ToString(), Math.Round(col[i].D, 2).ToString(),
                    Math.Round(angle[i].D, 2).ToString(), Math.Round(score[i].D, 2).ToString());

            }
            Log(s);
            Log("Instance found: " + score.Length.ToString());
            Log("Duration: " + stopwatch.Elapsed.TotalSeconds.ToString() + " s");
        }
    }
}

