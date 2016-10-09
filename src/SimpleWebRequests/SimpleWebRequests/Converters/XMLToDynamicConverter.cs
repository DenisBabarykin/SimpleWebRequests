using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleWebRequests.Converters
{
    /// <summary>Class for converting XML document to dynamic object</summary>
    public class XMLToDynamicConverter
    {
        /// <summary>Converts XML document to the dynamic object</summary>
        /// <param name="parent">Parent (root) element of XML document</param>
        /// <returns>Dynamic object</returns>
        public static dynamic Convert(XElement parent)
        {
            dynamic output = new ExpandoObject();

            output.Name = parent.Name.LocalName;
            output.Value = parent.Value;

            output.HasAttributes = parent.HasAttributes;
            if (parent.HasAttributes)
            {
                output.Attributes = new List<KeyValuePair<string, string>>();
                foreach (XAttribute attr in parent.Attributes())
                {
                    KeyValuePair<string, string> temp = new KeyValuePair<string, string>(attr.Name.LocalName, attr.Value);
                    output.Attributes.Add(temp);
                }
            }

            output.HasElements = parent.HasElements;
            if (parent.HasElements)
            {
                output.Elements = new List<dynamic>();
                foreach (XElement element in parent.Elements())
                {
                    dynamic temp = Convert(element);
                    output.Elements.Add(temp);
                }
            }

            return output;
        }
    }
}
