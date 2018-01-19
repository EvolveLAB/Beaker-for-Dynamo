using Autodesk.DesignScript.Runtime;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using RevitServices.Persistence;
using RevitServices;
using Revit.Elements;
using System;
using DimensionType = Autodesk.Revit.DB.DimensionType;
using Element = Revit.Elements.Element;
using Wall = Autodesk.Revit.DB.Wall;
using CoreNodeModels;
using Dynamo.Search;
using RevitServices.Transactions;

namespace Revit
{
    public class FamilyType
    {
        private FamilyType()
    { }

        /// <summary>
        /// This node will obtain the hosting criteria for a given family type.
        /// </summary>
        /// <param name="familyType">The family type to figure out host for.</param>
        /// <returns name="hostValue">The type of host, blank is none.</returns>
        /// <search>
        /// document, beaker, 
        /// </search>
        //this is the node DimensionUnits
        [MultiReturn(new[] { "hostValue" })]
        public static Dictionary<string, object> HostType(List<Revit.Elements.FamilyType> familyType)
        {
            Document doc = DocumentManager.Instance.CurrentDBDocument;
            string hostValue = null;
            foreach (var famType in familyType)
            {
                var internalElement = (Autodesk.Revit.DB.Family)famType.Family.InternalElement;
                TransactionManager.Instance.ForceCloseTransaction();
                Document famDoc = doc.EditFamily(internalElement);
                hostValue = famDoc.OwnerFamily.get_Parameter(BuiltInParameter.FAMILY_HOSTING_BEHAVIOR).AsValueString();
                famDoc.Close(false);
            }

            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "hostValue", hostValue}
                };
            return outInfo;
        }




    }
    
}
