using Autodesk.DesignScript.Runtime;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using RevitServices.Persistence;
using RevitServices;
using Revit.Elements;
using System;
using System.Linq;
using Element = Revit.Elements.Element;
using Wall = Autodesk.Revit.DB.Wall;
namespace Revit
{
    public class Selection
    {
        private Selection()
        { }


        /// <summary>
        /// This node will collect all walls of given type in the active project.
        /// </summary>
        /// <returns name="walls">Returns all walls of type in the current project.</returns>
        /// <search>
        /// walltype, selection, beaker, 
        /// </search>
        //this is the node AllWallsOfType with output of that wall type
        [MultiReturn(new[] { "walls" })]
        public static Dictionary<string, object> AllWallsOfType (Revit.Elements.WallType wallType)
        {
            Document doc = DocumentManager.Instance.CurrentDBDocument;
            FilteredElementCollector wallsCollector =
                new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType();

            var wallCollection = wallsCollector.ToElements();

            List<Revit.Elements.Element> wallTypes = new List<Revit.Elements.Element>();
            foreach (var element in wallCollection)
            {
                var wall = (Wall)element;
                if (wall.WallType.Id == wallType.InternalElement.Id)
                    wallTypes.Add(wall.ToDSType(true));
            }
            ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "walls", wallTypes}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will collect all floors of given type in the active project.
        /// </summary>
        /// <returns name="floors">Returns all floors of given type in the current project.</returns>
        /// <search>
        /// floortype, selection, beaker, 
        /// </search>
        //this is the node AllFloorsOfType with output of that wall type
        [MultiReturn(new[] { "floors" })]
        public static Dictionary<string, object> AllFloorsOfType(Revit.Elements.FloorType floorType)
        {
            Document doc = DocumentManager.Instance.CurrentDBDocument;
            FilteredElementCollector floorCollector =
                new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Floors).WhereElementIsNotElementType();

            var floorCollection = floorCollector.ToElements();

            List<Revit.Elements.Element> floorTypes = new List<Revit.Elements.Element>();
            foreach (var element in floorCollection)
            {
                var floor = (Autodesk.Revit.DB.Floor)element;
                if (floor.FloorType.Id == floorType.InternalElement.Id)
                    floorTypes.Add(floor.ToDSType(true));
            }
            ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "floors", floorTypes}
                };
            return outInfo;
        }
      




    }
}
