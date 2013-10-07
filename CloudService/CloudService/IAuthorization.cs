using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CloudService
{

    [ServiceContract]
    public interface IAuthorization
    {
        [OperationContract]
        bool Login(User user);
        
    }

    [DataContract]
    public class User
    {
        string usernameValue;
        string passwordValue;

        [DataMember]
        public string Username
        {
            get { return usernameValue; }
            set { usernameValue = value; }
        }

        [DataMember]
        public string Password
        {
            get { return passwordValue; }
            set { passwordValue = value; }
        }
    }
}
