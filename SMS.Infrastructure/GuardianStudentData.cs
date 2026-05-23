//using Microsoft.Data.SqlClient;
//using SMS.DTOs;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Text;

//namespace SMS.DataAccess
//{
//    public class GuardianStudentData
//    {
//        private static GuardianStudent FromDataReader(IDataReader reader)
//        {
//            return new GuardianStudent
//            (
//                guardianID: reader.GetInt32(reader.GetOrdinal("GuardianID")),
//                studentID: reader.GetInt32(reader.GetOrdinal("StudentID")),
//                relationshipType: reader.GetString(reader.GetOrdinal("RelationshipType"))
//            );
//        }

//        private static void _AddParameters(SqlCommand command, GuardianStudent link)
//        {
//            command.Parameters.Add("@GuardianID", SqlDbType.Int).Value = link.GuardianID;
//            command.Parameters.Add("@StudentID", SqlDbType.Int).Value = link.StudentID;
//            command.Parameters.Add("@RelationshipType", SqlDbType.NVarChar, 50).Value = link.RelationshipType;
//        }

            
//    }
//}
