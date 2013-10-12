using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.CloudService;

namespace Domain.Implementations
{
    public class SyncService : ISyncService
    {
        // Functions
        // :ISyncService
        public void SaveExercise(IRoute route, Users user)
        {
            IDBFactory factory = new DBFactory();
            Routes dbRoute = factory.CreateRoute(route);

            CloudService.ServiceClient client = new ServiceClient();
            client.SaveExerciseAsync(user, dbRoute);
        }
    }
}
