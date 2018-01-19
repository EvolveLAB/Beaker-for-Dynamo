using Autodesk.DesignScript.Runtime;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using RevitServices.Persistence;
using Revit.Elements;
using System;
using System.Linq;
using Element = Revit.Elements.Element;
using RevitServices.Transactions;
using Revit.GeometryConversion;
using Dynamo;

namespace Revit
{
    public class Utilities
    {
        private Utilities()
        {
        }

        /// <summary>
        /// This node will copy given elements by given translation point.
        /// </summary>
        /// <param name="element">The elements to copy.</param>
        /// <param name="translation">Dynamo point to use as translation.</param>
        /// <returns name="element">The copied elements.</returns>
        /// <search>
        /// copy, move, translation, 
        /// </search>
        //this is the node Copy Elements
        [MultiReturn(new[] {"element"})]
        public static Dictionary<string, object> CopyElements(List<Revit.Elements.Element> element,
            Autodesk.DesignScript.Geometry.Point translation)
        {
            //declare an elementId collection and add the ids to it.
            List<Autodesk.Revit.DB.ElementId> idCollection = new List<Autodesk.Revit.DB.ElementId>();
            foreach (var item in element)
            {
                var internalElement = (Autodesk.Revit.DB.Element) item.InternalElement;
                idCollection.Add(internalElement.Id);
            }
            //start the transaction to copy elements
            Document doc = DocumentManager.Instance.CurrentDBDocument;
            var revitPoint = translation.ToRevitType();
            TransactionManager.Instance.EnsureInTransaction(doc);
            var newElementIds = ElementTransformUtils.CopyElements(doc, idCollection, revitPoint);
            TransactionManager.Instance.TransactionTaskDone();
            //declare new list to append the new elements to it
            List<Revit.Elements.Element> newElements = new List<Revit.Elements.Element>();
            foreach (var item in newElementIds)
            {
                newElements.Add(doc.GetElement(item).ToDSType(true));
            }

            //returns the outputs
            var outInfo = new Dictionary<string, object>
            {
                {"element", newElements}
            };
            return outInfo;
        }

        /// <summary>
        /// This node will grab tagged and untagged elements of the given category in the given view.
        /// </summary>
        /// <param name="view">The view to use.</param>
        /// <param name="category">The category to use.</param>
        /// <returns name="notTagged">The not tagged items.</returns>
        /// <returns name="tagged">The tagged items.</returns>       
        /// <search>
        /// sheet, sheets, titleblock, collector, beaker, 
        /// </search>
        //this is the node ElementTaggedStatus
        [MultiReturn(new[] { "notTagged","tagged" })]
        public static Dictionary<string, object> ElementTaggedStatus(Revit.Elements.Views.View view,
            Revit.Elements.Category category)
        {
            var viewId = view.InternalElement.Id;
            var categoryId = category.Id;
            Document doc = DocumentManager.Instance.CurrentDBDocument;
            BuiltInCategory myCatEnum = (BuiltInCategory) Enum.Parse(typeof(BuiltInCategory), categoryId.ToString());

            List<Revit.Elements.Element> notTaggedList = new List<Revit.Elements.Element>();
            foreach (Autodesk.Revit.DB.Element e in new FilteredElementCollector(doc, viewId)
                .OfCategory(myCatEnum).WhereElementIsNotElementType())
            {
                if (new FilteredElementCollector(doc, viewId)
                        .OfClass(typeof(IndependentTag))
                        .Cast<IndependentTag>()
                        .FirstOrDefault(q => q.TaggedLocalElementId == e.Id) == null)
                    notTaggedList.Add(e.ToDSType(true));
            }

            List<Revit.Elements.Element> allTagged = new List<Revit.Elements.Element>();
            FilteredElementCollector allInView =
                new FilteredElementCollector(doc, viewId)
                    .OfClass(typeof(IndependentTag)).WhereElementIsNotElementType();
            var elementCollection = allInView.ToElements();

            foreach (Autodesk.Revit.DB.Element t in elementCollection)
            {
                var tag = (Tag)t.ToDSType(true);
                allTagged.Add(tag.TaggedElement);
            }
            List<Revit.Elements.Element> taggedList = new List<Revit.Elements.Element>();
            foreach (Element element in allTagged)
            {
                if (element.InternalElement.Category.Id.ToString() == category.Id.ToString())
                    taggedList.Add(element.InternalElement.ToDSType(true));
            }

            //returns the outputs
                var outInfo = new Dictionary<string, object>
            {
                {"notTagged", notTaggedList},
                {"tagged", taggedList}
            };
            return outInfo;
        }

        /// <summary>
        /// This node will "bake" the given elements. Developed to break association of Dynamo created Revit elements with Dynamo.
        /// </summary>
        /// <param name="element">The elements to bake.</param>
        /// <param name="bakeElementsToggle">Toggle to enable it to run.</param>
        /// <param name="pinElementsToggle">Toggle to allow user to pin element upon baking.</param>
        /// <returns name="element">The baked elements.</returns>
        /// <returns name="wasRan?">Did the workflow run?</returns>
        /// <search>
        /// bake
        /// </search>
        //this is the node Bake Elements
        [MultiReturn(new[] {"element","wasRan?"})]
        public static Dictionary<string, object> BakeElements(List<Revit.Elements.Element> element,
            Boolean bakeElementsToggle = false, Boolean pinElementsToggle = false)
        {
            //declare an elementId collection and add the ids to it.
            List<Autodesk.Revit.DB.ElementId> idCollection = new List<Autodesk.Revit.DB.ElementId>();
            //get the element ids
            foreach (var item in element)
            {
                var internalElement = (Autodesk.Revit.DB.Element) item.InternalElement;
                idCollection.Add(internalElement.Id);
            }
                //declare new list to append the new elements to it
                List<Revit.Elements.Element> newElements = new List<Revit.Elements.Element>();
            var wasRan = (string)"Set Toggle to true";

            if (bakeElementsToggle == true)
            {
                //start the transaction to copy elements
                Document doc = DocumentManager.Instance.CurrentDBDocument;
                var revitPoint = new XYZ(0, 0, 0);
                TransactionManager.Instance.EnsureInTransaction(doc);
                var newElementIds = ElementTransformUtils.CopyElements(doc, idCollection, revitPoint);
                TransactionManager.Instance.TransactionTaskDone();
                foreach (var id in idCollection)
                {
                    TransactionManager.Instance.EnsureInTransaction(doc);
                    var deletedElements = doc.Delete(id);
                    TransactionManager.Instance.TransactionTaskDone();
                }
                foreach (var item in newElementIds)
                {
                    newElements.Add(doc.GetElement(item).ToDSType(true));
                }
                foreach (Revit.Elements.Element ele in newElements)
                {
                    ele.InternalElement.Pinned = pinElementsToggle;
                }
                wasRan = (string)"Success!";
            }
            else
            {
                wasRan = (string) "Set Toggle to true";
            }
        
            //returns the outputs
            var outInfo = new Dictionary<string, object>
            {
                {"element", newElements},
                {"wasRan?", wasRan}
            };
            return outInfo;
        }



    }
}


