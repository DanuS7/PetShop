using PetShop.BusinessLogic.AppBL;
using PetShop.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.BusinessLogic
{
    public class BussinessLogic
    {
        public ISesion GetSessionBL()
        {
            return new SessionBL();
        }
    }
}
