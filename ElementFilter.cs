using Autodesk.DesignScript.Runtime;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using System;
using Dynamo;
namespace Revit
{
    public class ElementFilter
    {
        private ElementFilter()
        {
        }
        /// <summary>
        /// This node will filter the input elements by given category.
        /// </summary>
        /// <param name="category">The category to filter by.</param>
        /// <param name="element">Elements to filter.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByCategory
        [MultiReturn(new[] {"elements"})]
        public static Dictionary<string, object> ByCategory(List<Revit.Elements.Element> element,
            Revit.Elements.Category category)
        {

            List<Revit.Elements.Element> newList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
            
            Revit.Elements.Category mask = (Revit.Elements.Category) item.GetCategory;
            if (mask.Name == category.Name)
                newList.Add(item);
        }        
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", newList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given phase.
        /// </summary>
        /// <param name="phase">The phase to filter by.</param>
        /// <param name="element">Elements to filter.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByPhase
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByPhaseCreated(List<Revit.Elements.Element> element,
            Revit.Elements.Element phase)
        {

            List<Revit.Elements.Element> newList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {

                ElementId mask = (ElementId)item.InternalElement.CreatedPhaseId;
                if (mask == phase.InternalElement.Id)
                    newList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", newList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given phase.
        /// </summary>
        /// <param name="phase">The phase to filter by.</param>
        /// <param name="element">Elements to filter.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByPhaseDemolished
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByPhaseDemolished(List<Revit.Elements.Element> element,
            Revit.Elements.Element phase)
        {

            List<Revit.Elements.Element> newList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {

                ElementId mask = (ElementId)item.InternalElement.DemolishedPhaseId;
                if (mask == phase.InternalElement.Id)
                    newList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", newList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given category.
        /// </summary>
        /// <param name="name">The name to filter by.</param>
        /// <param name="element">Elements to filter.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByName
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByName(List<Revit.Elements.Element> element, string name)
        {
            List<Revit.Elements.Element> newList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                if (item.Name == name)
                    newList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", newList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="value">The string value to filter by.</param>
        /// <param name="element">Elements to filter by name begins with.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByNameBeginsWith
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByNameBeginsWith(List<Revit.Elements.Element> element, string value)
        {
            List<Revit.Elements.Element> newList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                if (item.Name.StartsWith(value))
                    newList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", newList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="value">The string value to filter by.</param>
        /// <param name="element">Elements to filter by name does not begin with.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByNameDoesNotBeginWith
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByNameDoesNotBeginWith(List<Revit.Elements.Element> element, string value)
        {
            List<Revit.Elements.Element> newList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                if (!item.Name.StartsWith(value))
                    newList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", newList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="value">The string value to filter by.</param>
        /// <param name="element">Elements to filter by name ends with.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter,beaker, 
        /// </search>
        //this is the node ElementFilter.ByNameEndsWith
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByNameEndsWith(List<Revit.Elements.Element> element, string value)
        {
            List<Revit.Elements.Element> newList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                if (item.Name.EndsWith(value))
                    newList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", newList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="value">The string value to filter by.</param>
        /// <param name="element">Elements to filter by name does not end with.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByNameDoesNotEndWith
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByNameDoesNotEndWith(List<Revit.Elements.Element> element, string value)
        {
            List<Revit.Elements.Element> newList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                if (!item.Name.EndsWith(value))
                    newList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", newList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="value">The string value to filter by.</param>
        /// <param name="element">Elements to filter by name contains.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByNameContains
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByNameContains(List<Revit.Elements.Element> element, string value)
        {
            List<Revit.Elements.Element> newList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                if (item.Name.Contains(value))
                    newList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", newList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="value">The string value to filter by.</param>
        /// <param name="element">Elements to filter by name does not contain.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByNameDoesNotContain
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByNameDoesNotContain(List<Revit.Elements.Element> element, string value)
        {
            List<Revit.Elements.Element> newList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                if (!item.Name.Contains(value))
                    newList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", newList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter.</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter starting with.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterStartsWith
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterValueBeginsWith(List<Revit.Elements.Element> element,
            string parameterName, string value)
        {

            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterStrings = item.GetParameterValueByName(parameterName).ToString();
                if (parameterStrings.StartsWith(value))
                valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter.</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter not starting with.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterValueDoesNotBeginWith
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterValueDoesNotBeginWith(List<Revit.Elements.Element> element,
            string parameterName, string value)
        {
            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterStrings = item.GetParameterValueByName(parameterName).ToString();
                if (!parameterStrings.StartsWith(value))
                    valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter.</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter ending with.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterValueEndsWith
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterValueEndsWith(List<Revit.Elements.Element> element,
            string parameterName, string value)
        {

            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterStrings = item.GetParameterValueByName(parameterName).ToString();
                if (parameterStrings.EndsWith(value))
                    valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter.</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter not ending with.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterValueDoesNotEndWith
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterValueDoesNotEndWith(List<Revit.Elements.Element> element,
            string parameterName, string value)
        {
            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterStrings = item.GetParameterValueByName(parameterName).ToString();
                if (!parameterStrings.EndsWith(value))
                    valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter.</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter contains.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterValueContains
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterValueContains(List<Revit.Elements.Element> element,
            string parameterName, string value)
        {

            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterStrings = item.GetParameterValueByName(parameterName).ToString();
                if (parameterStrings.Contains(value))
                    valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter.</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter does not contain.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterValueDoesNotContain
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterValueDoesNotContain(List<Revit.Elements.Element> element,
            string parameterName, string value)
        {

            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterStrings = item.GetParameterValueByName(parameterName).ToString();
                if (!parameterStrings.Contains(value))
                    valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter.</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter equals.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterEquals
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterEquals(List<Revit.Elements.Element> element,
            string parameterName, string value)
        {

            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterStrings = item.GetParameterValueByName(parameterName).ToString();
                if (parameterStrings == value)
                    valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }

        /// <summary>
        /// This node will filter the input elements by given value.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter.</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter does not equal.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterDoesNotEqual
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterDoesNotEqual(List<Revit.Elements.Element> element,
            string parameterName, string value)
        {

            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterStrings = item.GetParameterValueByName(parameterName).ToString();
                if (parameterStrings != value)
                    valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }
        /// <summary>
        /// This node will filter the input elements by the given numerical parameter and value. Will only work on numerical parameters.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter. (Numerical parameter)</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter is greater than.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterIsGreaterThan
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterIsGreaterThan(List<Revit.Elements.Element> element,
            string parameterName, double value)
        {

            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterAsNums = Convert.ToDouble(item.GetParameterValueByName(parameterName).ToString());
                if (parameterAsNums > value)
                    valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }
        /// <summary>
        /// This node will filter the input elements by the given numerical parameter and value. Will only work on numerical parameters.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter. (Numerical parameter)</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter is greater than or equal to.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterIsGreaterThanOrEqualTo
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterIsGreaterThanOrEqualTo(List<Revit.Elements.Element> element,
            string parameterName, double value)
        {

            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterAsNums = Convert.ToDouble(item.GetParameterValueByName(parameterName).ToString());
                if (parameterAsNums >= value)
                    valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }
        /// <summary>
        /// This node will filter the input elements by the given numerical parameter and value. Will only work on numerical parameters.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter. (Numerical parameter)</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter is less than.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterIsLessThan
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterIsLessThan(List<Revit.Elements.Element> element,
            string parameterName, double value)
        {

            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterAsNums = Convert.ToDouble(item.GetParameterValueByName(parameterName).ToString());
                if (parameterAsNums < value)
                    valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }
        /// <summary>
        /// This node will filter the input elements by the given numerical parameter and value. Will only work on numerical parameters.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter. (Numerical parameter)</param>
        /// <param name="value">The value to filter by.</param>
        /// <param name="element">Elements to filter by parameter is less than or equal to.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterIsBetweenTwoValues
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterIsLessThanOrEqualTo(List<Revit.Elements.Element> element,
            string parameterName, double value)
        {

            List<Revit.Elements.Element> valueList = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterAsNums = Convert.ToDouble(item.GetParameterValueByName(parameterName).ToString());
                if (parameterAsNums <= value)
                    valueList.Add(item);
            }
        ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList}
                };
            return outInfo;
        }
        /// <summary>
        /// This node will filter the input elements by the given numerical parameter and values. Will only work on numerical parameters.
        /// </summary>
        /// <param name="parameterName">The parameter name to use for filter. (Numerical parameter)</param>
        /// <param name="value1">The first value to filter by.</param>
        /// <param name="value2">The first value to filter by.</param>
        /// <param name="element">Elements to filter by parameter is between two values.</param>
        /// <returns name="elements">filtered elements</returns>
        /// <search>
        /// elementfilter, beaker, 
        /// </search>
        //this is the node ElementFilter.ByParameterIsLessThanOrEqualTo
        [MultiReturn(new[] { "elements" })]
        public static Dictionary<string, object> ByParameterIsBetweenTwoValues(List<Revit.Elements.Element> element,
            string parameterName, double value1, double value2)
        {

            List<Revit.Elements.Element> valueList1 = new List<Revit.Elements.Element>();
            foreach (var item in element)
            {
                var parameterAsNums = Convert.ToDouble(item.GetParameterValueByName(parameterName).ToString());
                if (parameterAsNums > value1)
                    valueList1.Add(item);
            }
            ;
            List<Revit.Elements.Element> valueList2 = new List<Revit.Elements.Element>();
            foreach (var item in valueList1)
            {
                var parameterAsNums = Convert.ToDouble(item.GetParameterValueByName(parameterName).ToString());
                if (parameterAsNums < value2)
                    valueList2.Add(item);
            }
            ;
            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "elements", valueList2}
                };
            return outInfo;
        }


    }
}
