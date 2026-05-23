//using Microsoft.Data.SqlClient;
//using SMS.Core;
//using System.Data;

//namespace SMS.DataAccess
//{
//    public class SchoolClassData
//    {
//        private static void _AddParameters(SqlCommand command, SchoolClass classInfo)
//        {
//            // We only add ClassID if it's an update (not -1)
//            if (classInfo.ClassID != -1)
//            {
//                command.Parameters.Add("@ClassID", SqlDbType.Int).Value = classInfo.ClassID;
//            }

//            command.Parameters.Add("@StageID", SqlDbType.Int).Value = classInfo.StageID;

//            // Handle the nullable TrackID
//            command.Parameters.Add("@TrackID", SqlDbType.Int).Value =
//                (object)classInfo.TrackID ?? DBNull.Value;

//            command.Parameters.Add("@GradeLevelID", SqlDbType.Int).Value = classInfo.GradeLevelID;
//            command.Parameters.Add("@SectionName", SqlDbType.NVarChar, 5).Value = classInfo.SectionName;
//        }

//        private static SchoolClass FromDataReader(SqlDataReader reader)
//        {
//            int classIDOrdinal = reader.GetOrdinal("ClassID");
//            int stageIDOrdinal = reader.GetOrdinal("StageID");
//            int trackIDOrdinal = reader.GetOrdinal("TrackID");
//            int gradeLevelIDOrdinal = reader.GetOrdinal("GradeLevelID");
//            int sectionNameOrdinal = reader.GetOrdinal("SectionName");

//            return new SchoolClass
//            {
//                ClassID = reader.GetInt32(classIDOrdinal),
//                StageID = reader.GetInt32(stageIDOrdinal),

//                // Handling the nullable TrackID column
//                TrackID = reader.IsDBNull(trackIDOrdinal) ? (int?)null : reader.GetInt32(trackIDOrdinal),

//                GradeLevelID = reader.GetInt32(gradeLevelIDOrdinal),
//                SectionName = reader.GetString(sectionNameOrdinal)
//            };
//        }

//    }
//}
