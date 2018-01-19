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
    public class Properties
    {
        private Properties()
        {
        }
        //this is the node Properties.SheetProperties with a few outputs
        [MultiReturn(new[] { "Sheet", "Sheet Name", "Sheet Number", "Titleblock", "Sheet Views" })]
        public static Dictionary<string, object> SheetProperties(List<Revit.Elements.Views.Sheet> viewSheet)
        {
            Document doc = DocumentManager.Instance.CurrentDBDocument;
            List<ElementId> viewIds = new List<ElementId>();
            foreach (var sheet in viewSheet)
            {
                viewIds.Add(sheet.InternalElement.Id);
            }
            List<Autodesk.Revit.DB.Element> internalSheet = new List<Autodesk.Revit.DB.Element>();
            foreach (var sheet in viewSheet)
            {
                internalSheet.Add(sheet.InternalElement);
            }
            List<Revit.Elements.Element> ttblList = new List<Revit.Elements.Element>();
            foreach (ElementId i in viewIds)
            {
                FilteredElementCollector elements = new FilteredElementCollector(doc, i).WhereElementIsNotElementType();
                var elementCollection = elements.ToElements();
                foreach (var item in elementCollection)
                {
                    var catStrings = item.Category.Name.ToString();
                    if (catStrings == "Title Blocks")
                        ttblList.Add(item.ToDSType(true));
                }
            }
            //grabs the elements from the collection

            //generates a list to convert to a usable type in Dynamo           
            List<string> sheetNames = new List<string>();
            foreach (Revit.Elements.Views.Sheet item in viewSheet)
            {
                sheetNames.Add(item.SheetName);
            }
            List<string> sheetNums = new List<string>();
            foreach (Revit.Elements.Views.Sheet item in viewSheet)
            {
                sheetNums.Add(item.SheetNumber);
            }
            ICollection<ElementId> coll = new List<ElementId>();
            List<List<Revit.Elements.Element>> viewList = new List<List<Revit.Elements.Element>>();
            List<Element> individualViews = new List<Element>();
            foreach (Autodesk.Revit.DB.ViewSheet item in internalSheet)
            {
                coll = item.GetAllPlacedViews();
                foreach (ElementId i in coll)
                {
                    individualViews.Add(doc.GetElement(i).ToDSType(true));
                }
            }
            viewList.Add(individualViews);

            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                { "Sheet", viewSheet},
                { "Sheet Name", sheetNames},
                { "Sheet Number", sheetNums},
                { "Titleblock", ttblList},
                { "Sheet Views", viewList}
                };
            return outInfo;
        }



    }
}
