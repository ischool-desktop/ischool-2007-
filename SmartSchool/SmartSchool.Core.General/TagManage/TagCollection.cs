using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SmartSchool.TagManage
{
    public class TagCollection : Dictionary<int, TagInfo>
    {
        /// <summary>
        /// 掃描整個集合，群組出相同 Prefix 名稱的集合。
        /// </summary>
        /// <returns></returns>
        internal PrefixCollection GroupPrefix()
        {
            PrefixCollection prefixes = new PrefixCollection();
            foreach (TagInfo each in this.Values)
            {
                Prefix pf;
                if (!prefixes.ContainsKey(each.Prefix))
                {
                    pf = new Prefix(each.Prefix);
                    prefixes.Add(pf.Name, pf);

                    pf.Tags.Add(each.Identity, each);
                }
                else
                {
                    pf = prefixes[each.Prefix];
                    pf.Tags.Add(each.Identity, each);
                }
            }

            return prefixes;
        }

        internal Dictionary<Color, TagColor> GetTagColors()
        {
            Dictionary<Color, TagColor> _colors = new Dictionary<Color, TagColor>();

            foreach (TagInfo each in Values)
            {
                Color c = each.Color;
                if (!_colors.ContainsKey(c))
                    _colors.Add(c, new TagColor(c));
            }

            return _colors;
        }
    }
}
