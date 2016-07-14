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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace NET_Project
{

    public partial class MatchingParameters : Form
    {
        public HTuple ModelCenterRow, ModelCenterCol, CentroidRow = 0, CentroidCol = 0, AngleStart, AngleExtent, NumMatches, NumLevels;
        public Display HGraphics;
        public MatchingParameters()
        {
            InitializeComponent();
            HGraphics = new Display(hWindowControl1);
            AngleStart = -0.39;
            AngleStartText.Text = AngleStart.ToString();
            AngleExtent = 0.79;
            AngleExtentText.Text = AngleExtent.ToString();
            NumMatches = 0;
            NumMatchTextBox.Text = NumMatches.ToString();
            NumLevels = 0;
            NumLevelsTextBox.Text = NumLevels.ToString();
            OptimizationComboBox.SelectedIndex = 0;
            SubPixComboBox.SelectedIndex = 1;
        }
        public MatchingParameters(MatchingModelParam param)
        {
            ModelCenterRow = param.ModelCenterRow;
            ModelCenterCol = param.ModelCenterCol;
            InitializeComponent();
            HGraphics = new Display(hWindowControl1);
            HGraphics.allObj = param.allObj;
            AngleStart = param.AngleStart;
            AngleStartText.Text = AngleStart.ToString();
            AngleExtent = param.AngleExtent;
            AngleExtentText.Text = AngleExtent.ToString();
            NumMatches = param.NumMatches;
            NumMatchTextBox.Text = NumMatches.ToString();
            NumLevels = param.NumLevels;
            NumLevelsTextBox.Text = NumLevels.ToString();
            OptimizationComboBox.SelectedItem = param.Optimization.S;
            SubPixComboBox.SelectedItem = param.SubPix.S;
            minScoreScroll.Value = Convert.ToInt16(param.MinScore.D*100);
            contrastBar.Value = param.Contrast;
            OverlapScroll.Value = Convert.ToInt16(param.Overlap.D*100);
            GreedinessScroll.Value = Convert.ToInt16(param.Greediness.D*100);
            GreedinessValue.Text = (param.Greediness*100.0).ToString() + "%";
            CreateModel.Visible = false;
            groupBox1.Enabled = false;
            FindButton.Visible = true;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CreateModel.Enabled = true;
                AngleStart = Convert.ToDouble(AngleStartText.Text);
                AngleStartText.BackColor = System.Drawing.Color.White;
                ErrorMessage.Text = "";
            }
            catch(FormatException Ex)
            {
                CreateModel.Enabled = false;
                ErrorMessage.Text = Ex.Message;
                AngleStartText.BackColor = System.Drawing.Color.Pink;
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CreateModel.Enabled = true;
                AngleExtent = Convert.ToDouble(AngleExtentText.Text);
                AngleExtentText.BackColor = System.Drawing.Color.White;
                ErrorMessage.Text = "";

                if (AngleExtent < 0)
                {
                    CreateModel.Enabled = false;
                    AngleExtentText.BackColor = System.Drawing.Color.Pink;
                    ErrorMessage.Text = "Angle Extent must be greater than 0.";
                }
            }
            catch (FormatException Ex)
            {
                CreateModel.Enabled = false;
                ErrorMessage.Text = Ex.Message;
                AngleExtentText.BackColor = System.Drawing.Color.Pink;
            }
        }

        private void NumMatchTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CreateModel.Enabled = true;
                NumMatches = Convert.ToInt16(NumMatchTextBox.Text);
                NumMatchTextBox.BackColor = System.Drawing.Color.White;
                ErrorMessage.Text = "";

                if (NumMatches < 0)
                {
                    CreateModel.Enabled = false;
                    NumMatchTextBox.BackColor = System.Drawing.Color.Pink;
                    ErrorMessage.Text = "Num Matches must be greater than 0.";
                }
            }
            catch (FormatException Ex)
            {
                CreateModel.Enabled = false;
                ErrorMessage.Text = Ex.Message;
                NumMatchTextBox.BackColor = System.Drawing.Color.Pink;
            }
        }


        private void GreedinessScroll_Scroll(object sender, ScrollEventArgs e)
        {
            GreedinessValue.Text = e.NewValue.ToString() +"%";
        }

        private void NumLevelsTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CreateModel.Enabled = true;
                NumLevels = Convert.ToInt16(NumLevelsTextBox.Text);
                NumLevelsTextBox.BackColor = System.Drawing.Color.White;
                ErrorMessage.Text = "";

                if (NumLevels < 0 || NumLevels >10)
                {
                    CreateModel.Enabled = false;
                    NumLevelsTextBox.BackColor = System.Drawing.Color.Pink;
                    ErrorMessage.Text = "Num Matches must be greater than 0 and less than 10.";
                }
            }
            catch (FormatException Ex)
            {
                CreateModel.Enabled = false;
                ErrorMessage.Text = Ex.Message;
                NumLevelsTextBox.BackColor = System.Drawing.Color.Pink;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void serialize(string FileName)
        {
            MatchingModelParam param = new MatchingModelParam(ModelCenterRow, ModelCenterCol, AngleStart, AngleExtent,
                minScoreScroll.Value / 100.0, OptimizationComboBox.SelectedItem.ToString().Replace(' ', '_'), contrastBar.Value, HGraphics.allObj, NumMatches,
                OverlapScroll.Value / 100.0, SubPixComboBox.SelectedItem.ToString(), GreedinessScroll.Value / 100.0, NumLevels);
            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FileName, FileMode.Create);
            formatter.Serialize(stream, param);
            stream.Close();
        }

        public static MatchingModelParam deserialize(string FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FileName, FileMode.Open);
            MatchingModelParam param = (MatchingModelParam)formatter.Deserialize(stream);
            stream.Close();
            return param;
        }

        public void findModel(HObject Image, out HTuple Row, out HTuple Column, out HTuple Angle, out HTuple Score)
        {
            Row = new HTuple();
            Column = new HTuple();
            Angle = new HTuple();
            Score = new HTuple();

            try
            {
                MatchingModelParam param = new MatchingModelParam(ModelCenterRow, ModelCenterCol, AngleStart, AngleExtent,
                    minScoreScroll.Value / 100.0, OptimizationComboBox.SelectedItem.ToString().Replace(' ', '_'), contrastBar.Value, HGraphics.allObj, NumMatches,
                    OverlapScroll.Value / 100.0, SubPixComboBox.SelectedItem.ToString(), GreedinessScroll.Value / 100.0, NumLevels);
                param.findModel(Image, out Row, out Column, out Angle, out Score);
            }
            catch(Exception ex)
            {
                this.ErrorMessage.Text = ex.Message;
            }
        }

    }
    /* this class stores all the parameters needed to create the matching model. 
     * This class is needed to serialize the parameters into a file, which can be read later on*/
    [Serializable]
    public class MatchingModelParam
    {
        public HTuple ModelCenterRow, ModelCenterCol, AngleStart, AngleExtent, MinScore, Optimization, Contrast, NumMatches, Overlap, SubPix, Greediness, NumLevels;
        public SortedDictionary<string, HObject> allObj = new SortedDictionary<string, HObject>();

        public MatchingModelParam(HTuple CenterRow, HTuple CenterCol, HTuple angleStart, HTuple angleExtent, HTuple score, HTuple optimization,
            HTuple contrast, SortedDictionary<string, HObject> Obj, HTuple numMatches, HTuple overlap, HTuple subPix, HTuple greediness, HTuple numLevels)
        {
            //create model parameters
            ModelCenterRow = CenterRow;
            ModelCenterCol = CenterCol;
            AngleStart = angleStart;
            AngleExtent = angleExtent;
            Optimization = optimization;
            Contrast = contrast;

            //find model parameters
            MinScore = score;
            NumMatches = numMatches;
            Overlap = overlap;
            SubPix = subPix;
            Greediness = greediness;
            NumLevels = numLevels;
            //graphics
            allObj = Obj;
        }

        /*findModel takes a form object MatchingParameters as input, which contains matching model information, and uses this 
         information to find the matches in the image HGraphics.allObj["pic"]. The position, angle and score of the matches returned
         Tip: Use try-catch block to enclose findModel when calling it in case of error*/
        public void findModel(HObject Image, out HTuple Row, out HTuple Column, out HTuple Angle, out HTuple Score)
        {
            HObject ImageReduced, ReducedDomain;
            HTuple OriginRow, OriginCol, area;
            HTuple ModelID;
            HOperatorSet.FillUp(allObj["colored_region"], out ReducedDomain);
            HOperatorSet.Union1(ReducedDomain, out ReducedDomain);
            HOperatorSet.AreaCenter(ReducedDomain, out area, out OriginRow, out OriginCol);
            HOperatorSet.ReduceDomain(allObj["pic"], ReducedDomain, out ImageReduced);
            

                HOperatorSet.CreateShapeModel(ImageReduced, "auto", AngleStart, AngleExtent, "auto",
                    Optimization, "ignore_local_polarity",
                    Contrast, "auto", out ModelID);
                HOperatorSet.SetShapeModelOrigin(ModelID, ModelCenterRow - OriginRow, ModelCenterCol - OriginCol);
                HOperatorSet.FindShapeModel(Image, ModelID, AngleStart, AngleExtent, MinScore,
                    NumMatches, Overlap, SubPix, NumLevels,
                    Greediness, out Row, out Column, out Angle, out Score);

            
        }
    }
}
