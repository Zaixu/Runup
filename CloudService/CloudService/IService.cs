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
        string Login(Users user);

        [OperationContract]
        string Register(Users user);

        [OperationContract]
        bool SaveData(Users user);

        [OperationContract]
        bool LoadData(Users user);

        [OperationContract]
        bool SaveExercise(Users user, Routes route);
    }
}
