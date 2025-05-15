using LINQQueries.ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQQueries.ConsoleApp
{
    public class LINQQuerySyntax
    {
        private List<PersonalInfo> listPersonalInfo = JSONReader<PersonalInfo>.GetAll("dataPersonalInfo");
        private List<DailyCost> listDailyCost = JSONReader<DailyCost>.GetAll("dataDailyCost");

        public List<PersonalInfo> SelectAll()
        {
            var result = (from listObj in listPersonalInfo
                          select listObj).ToList();
            return result;
        }
        public List<PersonalInfo> WhereCon()
        {
            var result = (from listObj in listPersonalInfo
                          where listObj.Savings > 0 && listObj.FirstName.Contains("first")
                          select listObj).ToList();
            return result;
        }

        public List<PersonalInfo> OrderBy()
        {
            //by default because ascending
            var result1 = (from listObj in listPersonalInfo
                           select listObj).ToList();

            var result2 = (from listObj in listPersonalInfo
                           orderby listObj.FirstName descending
                           select listObj).ToList();

            //Using extension method
            var InAscOrder = listPersonalInfo.OrderBy(s => s.LastName);
            var InDescOrder = listPersonalInfo.OrderByDescending(s => s.LastName);

            //Multiple sorting
            var orderByResult = from listObj in listPersonalInfo
                                orderby listObj.FirstName, listObj.LastName
                                select new { listObj.FirstName, listObj.LastName };


            return result1;
        }

        public List<PersonalInfo> ThenBy()
        {
            var thenByResult = listPersonalInfo.OrderBy(s => s.FirstName).ThenBy(s => s.Savings);
            var thenByDescResult = listPersonalInfo.OrderBy(s => s.FirstName).ThenByDescending(s => s.Savings).ToList();
            return thenByDescResult;
        }

        public void GroupBy()
        {
            var groupedResult = (from listObj in listPersonalInfo
                                 group listObj by listObj.Savings).ToList();
        }

        public void Join()
        {
            var innerJoin = listPersonalInfo.Join(// outer sequence 
                      listDailyCost,  // inner sequence 
                      personalInfo => personalInfo.Id,    // outerKeySelector
                      dailyCost => dailyCost.Id,  // innerKeySelector
                      (personalInfo, dailyCost) => new  // result selector
                      {
                          personalInfo.FirstName,
                          dailyCost.Amount
                      });
        }

        //Checks if all the elements in a sequence satisfies the specified condition
        public bool All()
        {
            var result = listPersonalInfo.All(x => x.Savings > 0 && x.Savings > 10000);
            return result;
        }

        //Checks if any of the elements in a sequence satisfies the specified condition
        public bool Any()
        {
            var result = listPersonalInfo.Any(x => x.Savings > 0 && x.Savings > 10000);
            return result;
        }

        public long Count()
        {
            var result = listPersonalInfo.Count();
            var result2 = listPersonalInfo.Count(x => x.Savings >= 18);
            var result3 = (from s in listPersonalInfo select s.Id).Count();
            return result;
        }

        //The Max operator returns the largest numeric element from a collection.
        public long Max()
        {
            var result = listPersonalInfo.Max(x => x.Savings);
            return result;
        }

        public long Sum()
        {
            var result = listPersonalInfo.Sum(x => x.Savings);
            return result;
        }




    }
}
