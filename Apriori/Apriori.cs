using Apriori.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Apriori.App
{
    public class Apriori : Controller
    {
        #region Props
        private readonly IAprioriProcessor _aprioriCoreProcessor;
        private readonly IAprioriItemsParser _aprioriItemsParser;
        #endregion

        #region Ctor
        public Apriori(IAprioriProcessor aprioriCoreProcessor,
                       IAprioriItemsParser aprioriItemsParser)
        {
            _aprioriCoreProcessor = aprioriCoreProcessor;
            _aprioriItemsParser = aprioriItemsParser;
        }
        #endregion

        #region Actions

        /// <summary>
        /// todo: Add the skeleton structure for the solution, and implement unit tests (in the unit-test project)!
        /// </summary>
        /// <param name="setD">Database of transactions, formatted with dash and underscore separators. 
        /// For example, the database setD with three transactions {1,2,3}, {2,3,4} and {1,3,4} would be passed in as "setD=1-2-3_2-3-4_1-3-4"</param>
        /// <param name="minsup">Minimum support parameter (as defined in the paper)</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [HttpGet()]
        public ContentResult Index(string setD = "A-F_A-B-F_A-C-F_A-B-C-F_S-F-D-A-S_A-C_A-S-D", ushort minsup = 2)
        {
            //Prepare Item Sets
            ItemSetList itemSets = _aprioriItemsParser.Process(setD);

            //Get Association Rules
            List<AssociationRule> rules = _aprioriCoreProcessor.GetRules(itemSets, minsup);

            return Content(rules.ToLines());
        }
        #endregion

    }
}