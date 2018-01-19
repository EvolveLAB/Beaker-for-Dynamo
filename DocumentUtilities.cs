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
using RevitServices.Transactions;

namespace Revit
{
    public class DocumentUtilities
    {
        private DocumentUtilities()
    { }

        /// <summary>
        /// This node will get the document from a given family type
        /// </summary>
        /// <param name="familyType">The family type to get document from.</param>
        /// <returns name="familyDocument">The family document.</returns>
        /// <search>
        /// document, beaker, 
        /// </search>
        //this is the node GetFamilyDocument
        [MultiReturn(new[] { "document" })]
        public static Dictionary<string, object> GetFamilyDocument(List<Revit.Elements.FamilyType> familyType)
        {
            Document doc = DocumentManager.Instance.CurrentDBDocument;

            Autodesk.Revit.DB.Document familyDocument = null;

            foreach (var fam in familyType)
            {
                         var internalElement = (Autodesk.Revit.DB.Family)fam.Family.InternalElement;
            TransactionManager.Instance.ForceCloseTransaction();
            //TransactionManager.Instance.EnsureInTransaction(doc);
            familyDocument = doc.EditFamily(internalElement);
            //TransactionManager.Instance.TransactionTaskDone();   
            }


            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "document", familyDocument}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will close a given document.
        /// </summary>
        /// <param name="document">The document to close</param>
        /// <param name="save">If you want to save.</param>
        /// <returns name="closeDocument">Result.</returns>
        /// <search>
        /// document, beaker, 
        /// </search>
        //this is the node DimensionUnits
        [MultiReturn(new[] { "closed" })]
        public static Dictionary<string, object> CloseDocument(Autodesk.Revit.DB.Document document, bool save)
        {
            var closeDocument = document.Close(save);

            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "closed", closeDocument}
                };
            return outInfo;
        }








    }
    
}
