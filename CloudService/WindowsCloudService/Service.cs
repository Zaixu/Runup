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
        private ServiceHost sHost;
        public Service()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start Service
        /// </summary>
        /// <param name="args">String array arguments</param>
        protected override void OnStart(string[] args)
        {
            //Start sevice host of CloudService Service
            sHost = new ServiceHost(typeof(CloudService.Service));
            sHost.Open();
        }

        protected override void OnStop()
        {
            //Stop service host
            sHost.Close();
        }
    }
}
