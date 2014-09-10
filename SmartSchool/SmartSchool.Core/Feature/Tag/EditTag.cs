using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Tag
{
    public class EditTag: FeatureBase
    {
        [QueryRequest()]
        public static void Delete(int tagId)
        {
            string strServiceName = "SmartSchool.Tag.Delete";
            DSRequest req = new DSRequest();
            req.SetContent("<DeleteRequest><Tag><ID>" + tagId + "</ID></Tag></DeleteRequest>");
            CallService(strServiceName, req);
        }

        [QueryRequest()]
        public static void Update(int tagId, string prefix, string name, int argbColor)
        {
            string strServiceName = "SmartSchool.Tag.Update";

            DSXmlHelper request = new DSXmlHelper("UpdateRequest");
            request.AddElement("Tag");
            request.AddElement("Tag", "Field");
            request.AddElement("Tag/Field", "Prefix", prefix);
            request.AddElement("Tag/Field", "Name", name);
            request.AddElement("Tag/Field", "Color", argbColor.ToString());
            request.AddElement("Tag", "Condition", "<ID>" + tagId.ToString() + "</ID>", true);

            CallService(strServiceName, new DSRequest(request));
        }

        public static int Insert(string prefix, string name, int argbColor, TagCategory category)
        {
            string strServiceName = "SmartSchool.Tag.Insert";

            DSXmlHelper request = new DSXmlHelper("InsertRequest");
            request.AddElement("Tag");
            request.AddElement("Tag", "Prefix", prefix);
            request.AddElement("Tag", "Name", name);
            request.AddElement("Tag", "Color", argbColor.ToString());
            request.AddElement("Tag", "Category", GetCategoryString(category));

            return int.Parse(CallService(strServiceName, new DSRequest(request)).GetContent().GetText("NewID"));
        }

        internal static string GetCategoryString(TagCategory category)
        {
            return Enum.GetName(typeof(TagCategory), category);
        }
    }
}
