using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WindowsCloudService
{
    public partial class Service : ServiceBase
    {
        ServiceHost sHost;
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            sHost = new ServiceHost(typeof(CloudService.Service));
            sHost.Open();
        }

        protected override void OnStop()
        {
            sHost.Close();
        }
    }
}
