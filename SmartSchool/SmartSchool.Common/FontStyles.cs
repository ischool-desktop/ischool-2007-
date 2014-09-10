using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace SmartSchool.Common
{
    /// <summary>
    /// 系統字型
    /// </summary>
    public static class FontStyles
    {
        private static PrivateFontCollection _systemFonts = new PrivateFontCollection();
        private static Font _protoType;
        
        static FontStyles()
        {
            const int DefaultFontSize = 10;
            const string FontFileName = "SystemFont.ttf";

            //_systemFonts = new PrivateFontCollection();

            if (File.Exists(Path.Combine(Application.StartupPath, FontFileName)))
            {
                _systemFonts.AddFontFile(FontFileName);
                _protoType = new Font(_systemFonts.Families[_systemFonts.Families.Length - 1], DefaultFontSize, FontStyle.Regular);
            }
            else
            {
                try
                {
                    _protoType = new Font(new FontFamily("微軟正黑體"), 9.75f);
                }
                catch
                {
                    _protoType = new Font(FontFamily.GenericSansSerif, DefaultFontSize);
                }
            }

            InitialFonts();
        }

        private static void InitialFonts()
        {
            _general = new Font(_protoType, FontStyle.Regular);
        }

        private static Font _general;
        /// <summary>
        /// 系統字型
        /// </summary>
        public static Font General
        {
            get
            {
                return _general;
            }
        }
        /// <summary>
        /// 系統字型的FontFamily
        /// </summary>
        public static FontFamily GeneralFontFamily
        {
            get
            {
                if (_systemFonts != null && _systemFonts.Families.Length > 0)
                    return _systemFonts.Families[_systemFonts.Families.Length - 1];
                else
                {
                    try
                    {
                        return new FontFamily("微軟正黑體");
                    }
                    catch
                    {
                        return FontFamily.GenericSansSerif;
                    }
                }
                //return (new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)))).FontFamily;
            }
        }
    }
}
