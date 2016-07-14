using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using VisionInterfaces;
using System.Windows.Forms;
using System.Threading;
using HalconDotNet;
namespace NET_Project
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    class VisionService:IVisionService
    {
        public static AutoResetEvent _messageReceived = new AutoResetEvent(false);
        public void GrabImageAsync()
        {
            ((Form1)(Application.OpenForms[0])).liveImageToolStripMenuItem_Click(this, EventArgs.Empty);
            
        }

        public async Task<VisionInterfaces.Point> GetUserPointInput()
        {
            var MainForm = (Form1)(Application.OpenForms[0]);
            _messageReceived.Reset();
            MainForm.GetUserPointInput();
            var result = await Task<Point>.Factory.StartNew(() =>
            {
                _messageReceived.WaitOne();
                var X = MainForm.LastMouseClickX - MainForm.hWindowControl.ImagePart.Width/2.0;
                var Y = -(MainForm.LastMouseClickY - MainForm.hWindowControl.ImagePart.Height / 2.0);
                return new Point(X,Y);
            });
            
            return result;
        }



        public MatchingResult FindModel(string fileName)
        {
            var MainForm = (Form1)(Application.OpenForms[0]);
            HTuple row, col, angle, score;
            MainForm.FindModel(fileName, out row, out col, out angle, out score);
            return new MatchingResult() { Row=row,Column=col,Angle=angle,Score=score};
            
        }
    }
}
