using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CloudService.Database;


namespace CloudService
{
 
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        bool Login(Users user);

        [OperationContract]
        bool Register(Users user);

        [OperationContract]
        bool SaveData(Users user);

        [OperationContract]
        bool LoadData(Users user);
    }
}
