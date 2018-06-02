using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LAF.Common.MLanguage
{
    public class LanguageConfigurationSectionHandler : IConfigurationSectionHandler
    {
        public LanguageConfigurationSectionHandler()
        { }

        #region IConfigurationSectionHandler 成员

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            return section;
        }

        #endregion
    }
}
