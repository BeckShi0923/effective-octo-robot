using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Threading;

namespace NET_Project
{

    enum myCallbackTypes
    {
        OTHER = 0,
        EXPOSURE_END,
        TRANSFER_END,
        DEVICE_LOST
    };

    class AquisitionCallback
    {

    }
}
