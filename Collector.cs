using Autodesk.DesignScript.Runtime;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using RevitServices.Persistence;
using Revit.Elements;
using System;
using Element = Revit.Elements.Element;
using Wall = Autodesk.Revit.DB.Wall;

namespace Revit
{
    public class Collector
    {
        private Collector()
        {
        }
        /// <summary>
        /// This node will collect all elements of given category while supplying extra outputs.
        /// </summary>
        /// <param name="category">The category to collect elements from.</param>
        /// <returns name="elements">Elements of given category.</returns>
        /// <returns name="names">Names of elements of given category.</returns>
        /// <returns name="count">Counts of elements of given category.</returns>
        /// <search>
        /// category, collector, beaker
        /// </search>

        //this is the node Collector.ByCategory with multiple outputs
        [MultiReturn(new[] { "elements","names", "count" })]
        public static Dictionary<string, object> ByCategory(Revit.Elements.Category category)
        {
            var categoryId = category.Id;
            Document doc = DocumentManager.Instance.CurrentDBDocument;
            BuiltInCategory myCatEnum = (BuiltInCategory)Enum.Parse(typeof(BuiltInCategory), categoryId.ToString());
            FilteredElementCollector elements = new FilteredElementCollector(doc).OfCategory(myCatEnum).WhereElementIsNotElementType();
            //grabs the elements from the collection
            var elementCollection = elements.ToElements();
            //generates a list to convert to a usable type in Dynamo
            List<Revit.Elements.Element> revitElements = new List<Revit.Elements.Element>();
            foreach (var element in elementCollection)
            {
                revitElements.Add(element.ToDSType(true));
            };
            List<string> stringList = new List<string>();
            foreach (Autodesk.Revit.DB.Element element in elementCollection)
            {
                stringList.Add(element.Name);
            };
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", revitElements},
                    { "names",stringList },
                    {"count", elements.GetElementCount()}                   
                };
            return outInfo;
        }

        /// <summary>
        /// This node will collect all curtain walls in the active project.
        /// </summary>
        /// <returns name="curtainWalls">Returns all curtain walls in the current project.</returns>
        /// <search>
        /// curtainwall, collector, beaker, 
        /// </search>
        //this is the node Collector.OfCurtainWall with output of just curtain wall types
        [MultiReturn(new[] {"curtainWalls"})]
        public static Dictionary<string, object> OfCurtainWalls()
        {
            Document doc = DocumentManager.Instance.CurrentDBDocument;
            FilteredElementCollector wallsCollector =
                new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType();

            var wallCollection = wallsCollector.ToElements();

            List<Revit.Elements.Element> curtainWallList = new List<Revit.Elements.Element>();
            foreach (Autodesk.Revit.DB.Element element in wallCollection)
            {
                if (element.ToDSType(true).GetParameterValueByName("Unconnected Height").ToString() != "")
                {
                    var wall = (Wall) element;
                    if (wall.CurtainGrid != null)
                        curtainWallList.Add(wall.ToDSType(true));
                }

            }
            ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "curtainWalls", curtainWallList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will collect all stacked walls in project along with individual members.
        /// </summary>
        /// <returns name="stackedWalls">The stacked walls in the document.</returns>
        /// <returns name="stackedWallMembers">The walls that make up the stacked walls.</returns>
        /// <search>
        /// stackedwalls, stacked, collector, beaker, 
        /// </search>
        //this is the node Collector.OfStackedWalls with output of just stacked wall types
        [MultiReturn(new[] { "stackedWalls","stackedWallMembers" })]
        public static Dictionary<string, object> OfStackedWalls()
        {
            var doc = DocumentManager.Instance.CurrentDBDocument;
            FilteredElementCollector wallsCollector =
                new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StackedWalls).WhereElementIsNotElementType();

            var wallCollection = wallsCollector.ToElements();

            List<Revit.Elements.Element> stackedWallList = new List<Revit.Elements.Element>();
            foreach (var element in wallCollection)
            {
                    stackedWallList.Add(element.ToDSType(true));
            }
            List<List<Revit.Elements.Element>> stackedWallMembers = new List<List<Revit.Elements.Element>>();
            foreach (var element in stackedWallList)
            {
                var stackedWall = (Wall)element.InternalElement;
                var stackedwallIds = new List<ElementId>(stackedWall.GetStackedWallMemberIds());
                List<Element> individualWalls = new List<Element>();
                foreach (var id in stackedwallIds)
                    
                {
                    individualWalls.Add(doc.GetElement(id).ToDSType(true));                               
                }
                stackedWallMembers.Add(individualWalls);
            }         
            ;

            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                {"stackedWalls", stackedWallList},
                {"stackedWallMembers", stackedWallMembers}
                };
            return outInfo;
        }

    }
}
