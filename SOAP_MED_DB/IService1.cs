using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SOAP_MED_DB
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        List<Game> GetGames();

        [OperationContract]
        Game GetOneGame(string id);

        [OperationContract]
        void DeleteGame(int id);

        [OperationContract]
        void UpdateGame(int id, Game spil);

        [OperationContract]
        int AddGame(string titel, double rating);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.

}
