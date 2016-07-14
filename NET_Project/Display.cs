using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Windows.Forms;

namespace NET_Project
{
    public sealed class Display
        /*This class allows the user to store the objects that is to be displayed on the HALCON window.
         * hWindowControl: The control of the HALCON window
         * allObj: All the objects are stored in this dictionary. The key is a string type representing the name of the object
         *         The Value is a class that stores the object(Image, region, xld etc.) and the corresponding parameters
         */
    {
        private HWindowControl hWindowControl;
        public SortedDictionary<string, HObject> allObj = new SortedDictionary<string, HObject>();

        // Class construction
        public Display(HWindowControl win)
        {
            hWindowControl = win;
        }

        public void display()
        {
            HOperatorSet.SetSystem("flush_graphic", "false");
            hWindowControl.HalconWindow.ClearWindow();
            allObj.Count();
            foreach (KeyValuePair<string, HObject> SelectedObject in allObj.Reverse())
            {
                string[] Attributes;
                string[] Colors = {"green", "red", "yellow", "colored", "blue"};
                string[] Draws = { "fill","margin"};
                string[] Thickness = { "thick","thin"};
                Attributes = SelectedObject.Key.Split('_');
                Colors = Attributes.Intersect(Colors).ToArray();
                Draws = Attributes.Intersect(Draws).ToArray();
                Thickness = Attributes.Intersect(Thickness).ToArray();

                if (Thickness.Length != 0)
                {
                    if (Thickness[0] == "thick")
                    {
                        HOperatorSet.SetLineWidth(hWindowControl.HalconWindow, 3);
                    }
                    else if (Thickness[0] == "thin")
                    {
                        HOperatorSet.SetLineWidth(hWindowControl.HalconWindow, 1);
                    }
                }
                else
                {
                    HOperatorSet.SetLineWidth(hWindowControl.HalconWindow, 1);
                }
                if (Draws.Length != 0)
                {
                    HOperatorSet.SetDraw(hWindowControl.HalconWindow, Draws[0]);
                } 
                if (Colors.Length != 0)
                {
                    if (Colors[0] != "colored")
                    {
                        HOperatorSet.SetColor(hWindowControl.HalconWindow, Colors[0]);
                    }
                    else
                    {
                        HOperatorSet.SetColored(hWindowControl.HalconWindow, 6);
                    }
                }
                HOperatorSet.DispObj(SelectedObject.Value, hWindowControl.HalconWindow);
            }
            HOperatorSet.SetSystem("flush_graphic", "true");
            HOperatorSet.DispLine(hWindowControl.HalconWindow, -4, -4, -4, -4);
        }
        //keepOnly function deletes all other keys not specified in the keepKeys string array
        public void keepOnly(string[] keepKeys)
        {
            string[] deleteKeys = allObj.Keys.Except(keepKeys).ToArray();
            foreach(string del in deleteKeys)
            {
                allObj[del].Dispose();
                allObj.Remove(del);
            }
        }

        public void SetFullImagePart(HObject Image)
        {
            hWindowControl.SetFullImagePart(new HImage(Image));
        }

        public string ListKeys()
        {
            string keys = "";
            foreach (string key in allObj.Keys)
            {
                keys += key + "\n";
            }
            keys = keys.TrimEnd('\n');
            return keys;
        }
    }
}
