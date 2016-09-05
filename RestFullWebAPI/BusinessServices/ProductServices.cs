using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public class ProductServices
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductServices()
        {
            _unitOfWork = new UnitOfWork();
        }

    }
}
