using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCR_Script_Generator
{
    class Study1
    {
        public int studyID;
        public string databaseName;
        public string server;

        public Study1(int theStudyID)
        {
            studyID = theStudyID;
            databaseName = getDatabaseName(theStudyID);
            server = getServer(theStudyID);
        }
      


        public int studyIdentifier
        {
            get { return studyID; }
            set { studyID = value; }
        }

        private string getDatabaseName(int studyID)
        {
            return "DATABASE001";
        }

        private string getServer(int studyID)
        {
            return "BER-FLXSQL-002C";
        }
    }



}
