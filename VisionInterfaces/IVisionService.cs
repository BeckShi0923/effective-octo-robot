using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace VisionInterfaces
{
    [ServiceContract]
    public interface IVisionService
    {
        [OperationContract]
        void GrabImageAsync();
        [OperationContract]
        Task<Point> GetUserPointInput();
        [OperationContract]
        MatchingResult FindModel(string fileName);
    }
}
