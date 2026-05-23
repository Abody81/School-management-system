//using DataHelper;
//using SMS.Core;
//using System.Data;

//namespace SMS.DataAccess
//{
//    public class CountryDate
//    {
//        public async Task<List<string>> GetAllCountries()
//        {
//            try
//            {
//                return await ADO_Helper.List_ExecuteReaderAsync<string>("sp_Countries_GetAllCountryNames",
//                                                CommandType.StoredProcedure);
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<Country> GetCountry(int CountryID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteReaderAsync("sp_Countries_GetCountry",
//                                       cmd => cmd.Parameters.AddWithValue(@"CountryID",CountryID),
//                                       Reader => 
//                                       {
//                                           return new Country
//                                           (
//                                                countryID: (int)Reader["CountryID"],
//                                                countryName: (string)Reader["CountryName"]
//                                           );
//                                       },
//                                       CommandType.StoredProcedure);
                    
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }
//    }
//}
